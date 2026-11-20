# Plan dokończenia migracji Asklepios do .NET 10

Data analizy: 14 listopada 2026 r. Zakres: kod i konfiguracja w lokalnym repozytorium. Plan zakłada zachowanie ASP.NET Core MVC/Razor, SQL Server oraz istniejących funkcji aplikacji.

## Stan potwierdzony

Migracja została już rozpoczęta i zapisana w historii: `23823c3` — migracja z .NET 6 do .NET 10, następnie `bd900e5` — aktualizacja pakietów. Obecny etap to weryfikacja i domknięcie migracji.

| Obszar | Stan lokalny | Pozostała praca |
| --- | --- | --- |
| Projekty | `Asklepios.Core` w katalogu `Asklepios.Models`, `Asklepios.Data`, `Asklepios.Web`, `TestConsole`: wszystkie `net10.0` | Zweryfikować zachowanie po aktualizacji |
| SDK/runtime | SDK `10.0.102`, ASP.NET Core i .NET runtime `10.0.2` dostępne lokalnie; brak `global.json` | Ustalić aktualne poprawki 10.0 i spójny SDK dla deweloperów/CI |
| Pakiety Microsoft | Główne pakiety ASP.NET Core/EF Core `10.0.0` | Ujednolicić poprawki i ograniczyć zbędne zależności |
| Kompilacja | `dotnet build Asklepios.sln --no-restore --configuration Release --verbosity quiet`: kod wyjścia 0, 0 błędów, 922 ostrzeżenia | Powtórzyć z czystego checkoutu i po świeżym restore |
| NuGet | Build zgłasza `NU1901`, `NU1902`, `NU1903` | Przeprowadzić świeży audyt i usunąć podatne zależności |
| EF Core | Aktywna migracja `20230926174950_init3`, model zapisany z EF `6.0.13`; brak snapshotu w aktywnym katalogu `Migrations` | Odtworzyć spójną historię i porównać model z bazą |
| Uruchamianie | Web używa `Program.cs`; `TestConsole` uruchamia host przez `Startup.cs` | Sprawdzić obie konfiguracje, zwłaszcza Identity |
| CI/CD | Trzy workflow instalują SDK `6.0.x`; wdrożenia używają starych akcji i publikują bez wskazania projektu | Zaktualizować CI i publikację |
| Profile publikacji | Część `.pubxml` ma `net6.0` i ścieżki `net6.0`; występują różne tryby publikacji | Uzgodnić rzeczywisty cel i sposób wdrażania |
| Testy | Brak osobnego projektu testów automatycznych; `TestConsole` jest aplikacją uruchamiającą host | Dodać testy krytycznych zachowań |

Kompilacja korzystała z istniejących assets NuGet; jej wynik nie potwierdza świeżego restore, działania bazy, aplikacji ani hostingu. Nie uruchamiano aplikacji, migracji bazy ani wdrożeń. Przed analizą katalog roboczy zawierał już zmodyfikowane, śledzone pliki `obj`.

## 1. Powtarzalne środowisko i punkt odniesienia

- [ ] Pracować na osobnej gałęzi, zachować istniejące zmiany użytkownika i oznaczyć ostatni zweryfikowany artefakt wdrożeniowy.
- [ ] Dodać `global.json` wskazujący wybraną, wspieraną wersję SDK 10.0; uzgodnić identyczny wybór z CI. Numery poprawek ustalić podczas realizacji.
- [ ] Wykonać restore i build Release w czystym checkoutcie; zachować raport błędów, ostrzeżeń oraz zależności bezpośrednich i przechodnich.
- [ ] Ustalić aktywny hosting i profil publikacji. Repo zawiera zarówno workflow wdrożeń, jak i profile Web Deploy/FTP — pliki nie dowodzą, który cel jest obecnie używany.
- [ ] Przygotować odizolowaną bazę testową z reprezentatywnymi danymi oraz bazowy zestaw scenariuszy i czasów odpowiedzi.

Kryterium zakończenia: nowy checkout daje odtwarzalny build na wybranym SDK, a cel wdrożenia i baza testowa są określone.

## 2. Pakiety i ostrzeżenia

- [ ] Ustawić spójne, aktualne poprawki `10.0.x` dla rodzin EF Core/Identity oraz zgodne narzędzia `dotnet-ef`; zapisać narzędzie w lokalnym manifeście.
- [ ] Wykonać `dotnet list Asklepios.sln package --vulnerable --include-transitive` po świeżym restore. Obecny build zgłasza podatności dla `SixLabors.ImageSharp 3.1.11`, `MailKit 4.14.1`, przechodniego `MimeKit 4.14.0`, `NuGet.Packaging/Protocol 6.12.1`, `System.Security.Cryptography.Pkcs 9.0.4` oraz `System.Security.Cryptography.Xml 9.0.0`. Potwierdzić aktualny stan audytem; nie wyłączać ostrzeżeń jako rozwiązania.
- [ ] Ustalić, przez które pakiety przychodzą podatne zależności, i aktualizować przede wszystkim pakiety nadrzędne. Zweryfikować wysyłanie poczty i ewentualne użycia PDF/obrazów po zmianach.
- [ ] Sprawdzić możliwość usunięcia nieużywanych `PdfSharpCore`, `ImageSharp` i `Swashbuckle`. W przeglądanym kodzie PDF występuje głównie jako import/komentarze; w aktywnym `Program.cs` brak rejestracji Swaggera. Usunięcie wymaga potwierdzenia użyć oraz kompilacji.
- [ ] Usunąć zbędne referencje `Razor.RuntimeCompilation` z Core/Data. W .NET 10 kompilacja Razor podczas działania jest oznaczona jako przestarzała; do pracy lokalnej stosować Hot Reload, a na produkcji kompilację przy buildzie. [Dokumentacja Microsoft](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/razor-runtime-compilation-obsolete?view=aspnetcore-10.0).
- [ ] Przy porządkowaniu zależności zachować jawne odwołanie do ASP.NET Core tam, gdzie jest potrzebne, np. `FrameworkReference` do `Microsoft.AspNetCore.App`: Core używa `IdentityUser<long>` i `IFormFile`. Usunięcie pakietu dostarczającego te referencje pośrednio nie może zerwać kompilacji.
- [ ] Ograniczyć redundantne referencje EF dopiero po sprawdzeniu wymagań runtime i narzędzi projektowych.
- [ ] Ostrzeżenia nullowalności uszeregować według ryzyka: najpierw dereferencje i `.Value` bez sprawdzenia w logowaniu, rezerwacji, dokumentacji i formularzach. Ustalić bazowy poziom pozostałych ostrzeżeń i blokować nowe; nie łączyć migracji z masową przebudową wszystkich modeli.

Kryterium zakończenia: udokumentowany audyt bez nierozwiązanych podatności wysokich/krytycznych, poprawny build i brak nowych ostrzeżeń względem zaakceptowanego poziomu.

## 3. EF Core i zgodność z istniejącą bazą — najwyższe ryzyko

- [ ] Przejrzeć łącznie zmiany niezgodne EF Core 7, 8, 9 i 10. Bezpośrednia aktualizacja do 10 nie wymaga wdrażania wersji pośrednich, lecz wymaga sprawdzenia ich zmian zachowania. [EF 7](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/breaking-changes), [EF 8](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes), [EF 9](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes), [EF 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/breaking-changes).
- [ ] Na kopii bazy odczytać wersję SQL Server, compatibility level, `__EFMigrationsHistory`, triggery i kolumny wyliczane. Zweryfikować połączenie TLS/certyfikat po aktualizacji SqlClient; dobrać konfigurację do serwera, zamiast trwale wyłączać weryfikację certyfikatu.
- [ ] Uzgodnić brakujący snapshot z ostatnią aktywną migracją i rzeczywistym schematem. Snapshot z `migration s_ old` jest wyłączony z kompilacji i nie należy zakładać, że odpowiada migracji `init3` lub działającej bazie.
- [ ] Odtworzyć właściwy punkt odniesienia modelu, porównać go z modelem EF 10 i ocenić każdą wygenerowaną zmianę. Nie tworzyć kolejnej migracji inicjalnej na istniejących tabelach ani nie usuwać historii migracji.
- [ ] Sprawdzić relacje i kaskady dla wizyt, skierowań, recept, pacjentów oraz Identity; kolumny `decimal`, daty `DateTimeOffset` i mapowanie hierarchii `MedicalWorker` z dyskryminatorem.
- [ ] Zweryfikować SQL oraz wyniki i czasy zapytań rezerwacji, grafiku, filtrów i stronicowania. Testować ostateczny SQL z EF 10: strategia tłumaczenia kolekcji ponownie się zmieniła w tej wersji, więc nie stosować mechanicznie obejść opisanych tylko dla EF 8.
- [ ] Zachować deterministyczny model: obecnie `seedDatabase = false`; ewentualne włączenie mocków z bieżącą datą w `HasData` wymaga osobnej decyzji. EF 9+ zgłasza błąd przy nakładaniu migracji z niezapisanymi zmianami modelu.
- [ ] Przygotować i przejrzeć skrypt różnicowy/idempotentny; na kopii istniejącej bazy przetestować aktualizację, a osobno utworzenie pustej bazy od zera. Jeśli schema nie wymaga zmian, nie dodawać zmian tabel tylko z powodu aktualizacji runtime.
- [ ] Zweryfikować backup i odtworzenie przed wdrożeniem. Zmiany schematu stosować osobno, według sprawdzonej procedury.

Kryterium zakończenia: spójna historia i snapshot, zweryfikowany skrypt lub potwierdzenie braku zmian schematu, poprawne odczyty/zapisy na reprezentatywnej bazie i udana próba odtworzenia backupu.

## 4. Uruchamianie, MVC, Identity i sesje

- [ ] Przyjąć `Asklepios.Web/Program.cs` jako konfigurację aplikacji Web. Ustalić rolę `TestConsole`, który korzysta z `Startup.cs` i uruchamia odmiennie skonfigurowany host.
- [ ] Naprawić lub wycofać tę drugą ścieżkę: `Startup.cs` rejestruje `AddIdentity<User, IdentityRole>`, a model i aktywny Web używają kluczy `long` i `IdentityRole<long>`. Kompilacja nie potwierdza poprawności takiej rejestracji DI podczas uruchamiania.
- [ ] Zweryfikować logowanie, wylogowanie, role, `FallbackPolicy`, przekierowania, wygasanie cookies i sesji oraz walidację antiforgery. Zachować zgodność kluczy Data Protection i konfiguracji cookies między wdrożeniami, jeśli mają przetrwać istniejące logowania.
- [ ] Sprawdzić routing wszystkich pięciu obszarów i Razor Pages oraz wyświetlanie/wiązanie formularzy, polskich dat i kwot.
- [ ] Sprawdzić obsługę utraty sesji przez żądania AJAX/API. .NET 10 zwraca 401/403 dla rozpoznanych endpointów API chronionych cookie zamiast przekierowania do logowania; wpływ zależy od metadanych konkretnych endpointów. [Dokumentacja Microsoft](https://learn.microsoft.com/en-us/aspnet/core/breaking-changes/10/cookie-authentication-api-endpoints?view=aspnetcore-10.0).
- [ ] Rozwiązać `ASP0014` przez rejestrację tras bezpośrednio na `app`, zachowując dotychczasowe wzorce tras; sprawdzić kolejność middleware.
- [ ] Przejrzeć mające zastosowanie zmiany ASP.NET Core/runtime od 7 do 10, zwłaszcza przy pracy za reverse proxy: HTTPS, przekierowania, schemat żądania i konfiguracja zaufanych proxy.

Kryterium zakończenia: host startuje na .NET 10, scenariusze uwierzytelniania i autoryzacji przechodzą, a strony i formularze działają na konfiguracji zbliżonej do docelowej.

## 5. Regresja funkcjonalna

Dodać projekt testów integracyjnych z hostem Web i odizolowanym SQL Server. EF InMemory nie potwierdzi zgodności zapytań, constraintów ani transakcji z SQL Server. `TestConsole` nie zastępuje tego projektu.

Minimalny zestaw automatyczny i uzupełniająca lista testów manualnych:

| Scenariusz | Weryfikacja |
| --- | --- |
| Strona publiczna i logowanie | Dostęp anonimowy, poprawne/błędne hasło, wylogowanie, wygaśnięcie sesji |
| Uprawnienia | Dostęp pacjenta, personelu medycznego, obsługi i administracji; odmowa dostępu do cudzych danych |
| Rezerwacja wizyty | Wyszukanie, filtrowanie, rezerwacja, anulowanie, ochrona przed podwójną rezerwacją |
| Grafik i administracja | Zapis/edycja grafiku, usług, placówek i personelu |
| Dokumentacja | Zapis podsumowania, recepty, skierowania i wyniku badania |
| Pliki | Upload i pobranie istniejących dokumentów/zdjęć; zachowanie zawartości po wdrożeniu |
| E-mail | Wysłanie przez testowy SMTP oraz kontrolowana obsługa błędu połączenia |
| Dane i wydajność | Zgodność rezultatów kluczowych zapytań, dat, kwot i stronicowania; brak istotnej regresji względem ustalonego pomiaru |

Kryterium zakończenia: wszystkie krytyczne scenariusze przechodzą, błędy mają wyjaśnienie, a CI rzeczywiście uruchamia i raportuje testy.

## 6. CI, publikacja i wdrożenie

- [ ] We wszystkich trzech plikach `.github/workflows` ustawić SDK 10.0 zgodny z `global.json`; usunąć `include-prerelease`.
- [ ] Zaktualizować `checkout`, `setup-dotnet`, upload/download artefaktów i akcję wdrożeniową do wspieranych wersji zgodnych z runnerem. Stare akcje artefaktów wymagają wymiany; dobrać parę upload/download według dokumentacji. [Repozytorium akcji](https://github.com/actions/upload-artifact).
- [ ] Publikować jawnie `Asklepios.Web/Asklepios.Web.csproj`, zamiast całego solution do wspólnego katalogu.
- [ ] Zaktualizować aktywne `.pubxml`: framework, ścieżki wyjściowe, RID oraz jawny wybór `SelfContained`. Oznaczyć nieużywane profile jako archiwalne lub usunąć je w osobnej zmianie.
- [ ] Sprawdzić obsługę .NET 10 na faktycznym hostingu: runtime i integrację serwera, system operacyjny, architekturę oraz model procesu. Dla IIS sprawdzić Hosting Bundle/ASP.NET Core Module; self-contained nie zastępuje integracji z IIS.
- [ ] Wykonać test publikowanego artefaktu w środowisku testowym. Sprawdzić konfigurację połączenia `Webio` wybieranego w `Program.cs`, ustawienia SMTP i katalogi plików bez zapisywania sekretów w dokumencie lub logach.
- [ ] Zachować uploady i wyniki badań, w tym pliki z `wwwroot/Results`, przy wymianie katalogu aplikacji. Ustalić ich trwałą lokalizację i backup.
- [ ] Wdrożyć sprawdzony artefakt, przeprowadzić smoke test logowania, rezerwacji, odczytu dokumentacji i połączenia z bazą; monitorować błędy oraz czasy odpowiedzi.

Rollback: zachować poprzedni działający artefakt, konfigurację i klucze Data Protection. Powrót do niego jest bezpieczny tylko przy zgodnym schemacie bazy. W razie niezgodności zatrzymać zapisy i zastosować wcześniej przetestowaną procedurę bazy; odtworzenie backupu może utracić zapisy wykonane od jego utworzenia. Nie traktować samej metody `Down` jako sprawdzonego planu odzyskiwania.

## Kolejność dostarczenia i nakład

| Pakiet prac | Zawartość | Orientacyjny nakład |
| --- | --- | --- |
| PR 1 | SDK, świeży build/audyt, pakiety i potrzebne referencje, priorytetowe ostrzeżenia | 1–2 dni |
| PR 2 | Historia/snapshot EF, zgodność SQL i skrypty, testy danych | 2–4 dni |
| PR 3 | Konfiguracja uruchamiania, Identity/MVC, testy krytycznych scenariuszy | 2–3 dni |
| PR 4 | Workflow, profile, publikacja próbna, regresja i próba rollbacku | 1–2 dni |

Szacunek: 6–11 dni roboczych jednej osoby, przy dostępie do bazy testowej i hostingu. To szacunek planistyczny; brak aktualnego schematu bazy lub konieczność zmiany hostingu mogą go zwiększyć. PR 2 i PR 3 wymagają ustabilizowanych zależności z PR 1; PR 4 można przygotowywać wcześniej, lecz wdrożenie zależy od wyników obu.

Migrację uznajemy za zakończoną po świeżym buildzie/testach/publikacji w CI, usunięciu blokujących podatności, potwierdzeniu zgodności bazy i funkcji, uruchomieniu docelowego artefaktu na hostingu oraz sprawdzeniu procedury powrotu.

Poza zakresem podstawowym: przebudowa architektury, wymiana MVC na SPA/Blazor, modernizacja całego frontendu i hurtowe usunięcie wszystkich ostrzeżeń. Osobno zaplanować usunięcie artefaktów `bin/obj` z indeksu Git: repo śledzi ich wiele mimo `.gitignore`, co utrudnia ocenę zmian i czyste budowanie.
