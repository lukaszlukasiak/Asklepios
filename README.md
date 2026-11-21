# Asklepios

Portal pacjenta oparty na ASP.NET Core MVC, Identity i SQL Server. Trwa migracja do .NET 10; [plan](docs/plan-migracji-net10.md) i [postęp](docs/postep-migracji-net10.md) opisują jej zakres.

## Uruchomienie lokalne

1. Zainstaluj SDK .NET wskazane przez `global.json` oraz SQL Server lub LocalDB.
2. Utwórz lokalną konfigurację, zachowując istniejący plik, jeśli już go masz:

   ```powershell
   if (-not (Test-Path Asklepios.Web/appsettings.json)) {
       Copy-Item Asklepios.Web/appsettings.example.json Asklepios.Web/appsettings.json
   }
   ```

3. W lokalnym `appsettings.json` ustaw połączenie `ConnectionStrings:Webio` do własnej bazy. Nazwa `Webio` jest obecnie używana przez aplikację niezależnie od dostawcy hostingu. Przykład używa uwierzytelniania Windows w LocalDB; `TrustServerCertificate=True` służy wyłącznie lokalnemu środowisku, a konfiguracja serwera docelowego powinna weryfikować jego certyfikat.
4. Jeśli potrzebujesz wysyłania poczty, uzupełnij sekcję `Email` danymi własnego testowego SMTP. Bieżąca implementacja `MailServices` czyta tę sekcję bezpośrednio z `appsettings.json`. Szablon nie zawiera działających poświadczeń.
5. Odtwórz pakiety i zbuduj aplikację:

   ```powershell
   dotnet tool restore
   dotnet restore Asklepios.sln
   dotnet build Asklepios.sln -c Release --no-restore
   dotnet run --project Asklepios.Web
   ```

Uruchomienie nie tworzy ani nie aktualizuje automatycznie bazy. Przed zastosowaniem migracji należy uzgodnić historię EF z istniejącym schematem, zgodnie z planem migracji.

## Pliki lokalne i dane

Konfiguracja `appsettings.json`, jej warianty środowiskowe, profile publikacji, pliki użytkownika IDE, bazy, backupy, katalogi `bin`, `obj`, `.vs` oraz `artifacts` są ignorowane przez Git. Do repozytorium trafiają wyłącznie bezpieczne szablony konfiguracji.

Dokumenty w `Asklepios.Web/wwwroot/Results`, uploady i `App_Data` są lokalnymi danymi aplikacji. Trzeba zapewnić ich własne przechowywanie i backup; nie należy dołączać rzeczywistych danych pacjentów do repozytorium.

Jeżeli repozytorium pochodzi sprzed czyszczenia historii, nie należy wypychać z niego starych gałęzi. Szczegóły czyszczenia opisuje [procedura](docs/czyszczenie-repozytorium.md). Ujawnione wcześniej hasła i tokeny wymagają unieważnienia lub wymiany, nawet po usunięciu plików z Git.
