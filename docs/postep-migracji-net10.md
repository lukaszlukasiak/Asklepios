# Postęp migracji do .NET 10

Stan: 21 listopada 2026 r. Gałąź: `migration-dotnet10`.

## Wykonano

- Plan migracji zapisano w commicie `4cce788`.
- Zainstalowano SDK `10.0.404` lokalnie w ignorowanym katalogu `artifacts/dotnet`, bez zmiany systemowej instalacji .NET. `global.json` wskazuje tę wersję i dopuszcza poprawki w tej samej linii SDK.
- Wersje pakietów przeniesiono do `Directory.Packages.props`; rodziny EF Core i Identity ujednolicono do `10.0.15`.
- Usunięto nieużywane pakiety Razor RuntimeCompilation, EF InMemory z modeli, PdfSharpCore/ImageSharp, Swagger i narzędzia scaffoldingowe Web.CodeGeneration.Design. Core/Data otrzymały jawne referencje do ASP.NET Core. Scaffolding nie jest częścią uruchomienia aplikacji; jego ewentualne ponowne użycie wymaga dobrania osobnego, zgodnego narzędzia.
- Dodano jawną zależność Newtonsoft.Json `13.0.4`, używaną przez aplikację, oraz zaktualizowano MailKit do `5.0.0` z wymaganym importem metod rozszerzających.
- Włączono audyt wszystkich zależności; `NU1901`–`NU1904` są błędami restore/build.
- Manifest `dotnet-ef 10.0.15` przeniesiono do katalogu głównego rozwiązania, zastępując stary manifest Web z wersją `7.0.10`. Wykonano `dotnet tool restore` i potwierdzono wersję narzędzia przez `--version`.
- Poprawiono rejestrację Identity w `Startup.cs` na `IdentityRole<long>`, zgodnie z modelem oraz konfiguracją głównej aplikacji.
- W `Program.cs` trasy zarejestrowano bezpośrednio na `app`, zachowując ich wzorce. Dodano publiczną część klasy `Program` na potrzeby późniejszych testów hosta.
- Uzupełniono `.gitignore` o lokalną konfigurację, sekrety, certyfikaty, profile wdrożeń, dane aplikacji, wyniki testów i artefakty. Zachowano możliwość commitowania szablonów konfiguracji, migracji SQL/EF, manifestów narzędzi i plików blokad zależności.

## Weryfikacja

Wykonano świeży restore rozwiązania z NuGet do osobnego katalogu pakietów i osobnego katalogu wyjściowego. Restore zakończył się powodzeniem, bez zgłoszonych ostrzeżeń o podatnościach. Następnie wykonano build Release na SDK `10.0.404`: **0 błędów, 1688 ostrzeżeń**, przede wszystkim istniejących ostrzeżeń nullowalności. Liczba dotyczy pełnego buildu w nowym katalogu wyjściowym; wcześniejsze 922 ostrzeżenia pochodziły z innego, częściowo przyrostowego buildu i nie stanowią porównywalnego pomiaru regresji.

Polecenia (PowerShell, z katalogu rozwiązania; po standardowej instalacji właściwego SDK można używać `dotnet` zamiast ścieżki lokalnej):

```powershell
$env:DOTNET_CLI_HOME = Join-Path (Get-Location) 'artifacts/cli'
$env:NUGET_PACKAGES = Join-Path (Get-Location) 'artifacts/packages'
$env:DOTNET_ROOT = Join-Path (Get-Location) 'artifacts/dotnet'
./artifacts/dotnet/dotnet.exe restore Asklepios.sln --artifacts-path artifacts/build --source https://api.nuget.org/v3/index.json --force
./artifacts/dotnet/dotnet.exe build Asklepios.sln -c Release --no-restore --artifacts-path artifacts/build
```

Build nie nadpisuje istniejących, śledzonych plików `obj` w projektach. Nie uruchamiano operacji na bazie ani wdrożeń. Kompilacja nie potwierdza poprawności logowania, SMTP ani zgodności schematu bazy.

## Do wykonania

1. Uzgodnienie historii/snapshotu EF z reprezentatywną bazą testową. Nie zmieniono dotychczasowych migracji ani schematu bazy.
2. Testy startu hosta i scenariuszy logowania, ról, rezerwacji oraz dokumentacji; osobne testy integracyjne SQL Server i SMTP.
3. Aktualizacja workflow GitHub Actions i profili publikacji do .NET 10, test artefaktu i procedury powrotu.
4. Wycofanie z indeksu Git już śledzonych artefaktów i lokalnych danych, z zachowaniem plików na dysku. `.gitignore` nie działa wstecz: po jego aktualizacji ponad 11 tysięcy śledzonych plików nadal pasuje do reguł ignorowania, w tym konfiguracja aplikacji i dokumenty w `wwwroot/Results`.

Przed publicznym udostępnieniem potrzebny jest osobny przegląd sekretów i danych także w kodzie oraz historii Git. Usunięcie pliku z bieżącego indeksu nie usuwa poprzednich wersji; ujawnione poświadczenia wymagają wymiany.
