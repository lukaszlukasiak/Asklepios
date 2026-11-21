# Asklepios

Portal pacjenta oparty na ASP.NET Core MVC, Identity i SQL Server.

Gałąź `main` zachowuje dotychczasową wersję aplikacji (`net6.0`). Migracja do .NET 10 jest przygotowywana osobno; czyszczenie historii nie wdraża nowej wersji aplikacji.

## Konfiguracja lokalna

Skopiuj `Asklepios.Web/appsettings.example.json` do `Asklepios.Web/appsettings.json`, jeśli nie masz jeszcze własnej konfiguracji. Ustaw `ConnectionStrings:Webio` dla swojej bazy oraz sekcję `Email` dla testowego SMTP. Szablon korzysta z Windows LocalDB; `TrustServerCertificate=True` jest ustawieniem wyłącznie lokalnym. Środowisko docelowe powinno weryfikować certyfikat serwera.

Rzeczywiste hasła, konfigurację środowiskową i profile publikacji przechowuj poza Git. Dane w `wwwroot/Results`, uploady oraz katalogi `bin`, `obj`, `.vs` i `artifacts` są lokalne i nie należą do repozytorium. Przykładowa konfiguracja nie zawiera działających poświadczeń. Uruchomienie aplikacji nie oznacza automatycznej aktualizacji bazy.

## Po oczyszczeniu historii

Historia repozytorium została przepisana w celu usunięcia artefaktów, lokalnej konfiguracji i poświadczeń. Dotychczasowe klony należy ponownie sklonować albo świadomie uzgodnić z nową historią; nie wypychaj starych gałęzi, ponieważ przywróciłoby to usunięte dane.

Hasła SQL, SMTP i poświadczenia publikacji ujawnione w dawnych commitach wymagają wymiany u dostawców usług. Samo czyszczenie Git ich nie unieważnia. Hashe haseł dawnych kont startowych zostały usunięte; przy inicjalizacji nowej bazy konta te wymagają ustawienia nowych haseł. Istniejącej bazy nie zmieniono.

Workflow `Secret scanning` sprawdza całą historię. Zasoby demonstracyjne nie powinny zawierać rzeczywistych danych pacjentów.
