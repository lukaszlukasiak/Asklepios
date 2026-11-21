# Czyszczenie repozytorium przed publikacją

Data: 21 listopada 2026 r. Zakres: artefakty, lokalna konfiguracja, profile publikacji, dokumenty wgrywane do aplikacji i ujawnione poświadczenia.

## Bieżąca gałąź

Pliki pasujące do `.gitignore` należy usunąć wyłącznie z indeksu Git. Kopie na dysku, w tym działający `Asklepios.Web/appsettings.json` i dokumenty z `wwwroot/Results`, mają zostać zachowane. Publicznym odpowiednikiem konfiguracji jest `Asklepios.Web/appsettings.example.json`; instrukcja utworzenia lokalnej konfiguracji znajduje się w głównym README.

Z kodu usunięto komentarze z nieużywanymi danymi dostępowymi. W danych startowych migracji i dawnym snapshocie zakodowane hashe haseł zastąpiono pustymi wartościami. Nie zmieniono schematu ani istniejącej bazy. Konta tworzone z tych danych podczas inicjalizacji nowej bazy wymagają ustawienia nowych haseł; nie można korzystać z dawnych poświadczeń demonstracyjnych.

`.dockerignore` wyklucza lokalne dane również z kontekstu budowania obrazów. `.gitleaks.toml` rozszerza standardowe reguły Gitleaks o hasła SMTP i połączenia SQL używane przez projekt. Workflow `Secret scanning` skanuje całą pobraną historię. Do czasu zastąpienia starych zdalnych gałęzi oczyszczonymi wersjami może celowo zgłaszać poświadczenia z dawnych commitów.

## Historia i kopia zapasowa

Oryginalna historia i manifest sum kontrolnych plików lokalnych trafiają wyłącznie do ignorowanego katalogu `artifacts/repo-cleanup`. Ten katalog zawiera dane prywatne, w tym kopię historii z dawnymi sekretami i reguły ich zastępowania. Nie wolno publikować go jako archiwum, artefakt CI ani kopię repozytorium.

Skrypt `scripts/security/prepare-public-history.py` przygotowuje osobne repozytorium bare `artifacts/repo-cleanup/clean.git`. Przepisuje tylko tę kopię. Zachowuje rzeczywiste gałęzie i tagi, pomijając prywatne referencje narzędzi oraz referencje śledzące remote. Usuwa historyczne ścieżki pasujące do aktualnych reguł ignorowania i zastępuje znane hasła w zachowanych plikach oraz opisach commitów.

Skrypt nie wykonuje połączeń sieciowych, nie wypycha zmian i nie przepisuje historii bieżącego katalogu roboczego. Narzędzie `git-filter-repo` pobiera się osobno z oficjalnego projektu; nie jest dołączone do repozytorium.

```powershell
# Pierwszy krok wykonuje się przed zmianą indeksu; manifestu nie nadpisuje się.
py -3.14 scripts/security/prepare-public-history.py inventory

# Po wycofaniu plików z indeksu sprawdź, że zostały nienaruszone na dysku.
py -3.14 scripts/security/prepare-public-history.py verify-local-files

# Rozszerz reguły o historyczne hasła SMTP i wyłącz hashe haseł danych startowych.
py -3.14 scripts/security/prepare-public-history.py redact-credentials

# Po zapisaniu zmian w commicie przygotuj osobną, oczyszczoną historię.
py -3.14 scripts/security/prepare-public-history.py mirror --filter-repo artifacts/tools/git-filter-repo.py
```

Kolejne podejście po rozszerzeniu reguł można przygotować pod inną nazwą, np. `--mirror-name clean-v2.git`; skrypt nie nadpisuje istniejących kopii.

Przed publikacją kopia wymaga weryfikacji wszystkich historycznych ścieżek, skanowania sekretów we wszystkich zachowanych gałęziach i sprawdzenia integralności przez `git fsck`. Logi i raporty prywatne przechowuje się pod `artifacts/repo-cleanup`; do publicznego podsumowania trafiają wyłącznie liczby i nazwy reguł, bez wartości sekretów.

## Zmiana repozytorium GitHub

Repozytorium `lukaszlukasiak/Asklepios` było publiczne podczas sprawdzenia. Usunięcie danych lokalnie nie usuwa ich z GitHub. Podmiana historii zdalnej zmieni identyfikatory commitów i wymaga koordynacji z osobami posiadającymi stare klony.

Przed zmianą zdalnych gałęzi należy ponownie odczytać ich aktualne SHA i porównać z punktem odniesienia. Publikacja powinna dotyczyć jawnie wskazanych gałęzi, z kontrolą oczekiwanego poprzedniego SHA (`--force-with-lease`), zamiast bezwarunkowego `push --mirror`. Nie należy wypychać referencji checkpointów, starej historii ani lokalnych kopii zapasowych.

Po podmianie najbezpieczniej ponownie sklonować repozytorium. Pliki lokalne i konfigurację można przenieść z dotychczasowego katalogu, ale nie wolno wypychać z niego starych gałęzi. Usunięte dane mogą nadal występować w cudzych klonach, forkach i referencjach pull requestów; w razie potrzeby należy zastosować procedurę GitHub Support dotyczącą danych wrażliwych.

## Poświadczenia i dane

Ujawnione hasła do baz, konta SMTP i poświadczenia publikacji trzeba wymienić lub unieważnić u dostawców usług oraz zaktualizować wyłącznie w lokalnej konfiguracji i sekretach środowisk. Czyszczenie historii nie zastępuje tej operacji. Nie należy umieszczać nowych haseł w issue, commicie ani raporcie.

Skaner sekretów nie potwierdza, że wszystkie przykładowe dokumenty, zdjęcia i dane demonstracyjne są anonimowe. Rzeczywiste dane pacjentów nie powinny trafić do repozytorium; przed upublicznieniem nowych zasobów konieczna jest ocena ich pochodzenia.

Źródła: [git-filter-repo](https://github.com/newren/git-filter-repo), [usuwanie danych wrażliwych według GitHub](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/removing-sensitive-data-from-a-repository).
