# MailSender

Projekt ASP.NET Core do rejestracji aplikacji klienckiej, generowania JWT oraz filtrowania maili przed wysyłką.

## Co już działa

- Rejestracja aplikacji przez endpoint `POST /client-app/register`.
- Generowanie tokena JWT po poprawnej rejestracji.
- Ochrona endpointu `POST /mail/send` autoryzacją JWT.
- Swagger z możliwością wklejenia tokena przez przycisk `Authorize`.
- Modyfikowanie tematu wiadomości, jeśli kończy się znakiem zapytania.
- Oznaczanie nazwisk w treści wiadomości tagami `[student.surname]...[/student.surname]`.
- Odczyt `appId` oraz `appName` z tokena JWT bez bazy danych.

## Struktura projektu

- `MailSender.Api` - API, kontrolery, Swagger i JWT.
- `MailSender.Core` - modele i logika przetwarzania wiadomości.
- `MailSender.Infrastructure` - miejsce na przyszłą integrację z zewnętrznym dostawcą poczty.

## Wymagania

- .NET SDK 9.0.
- Visual Studio 2022 albo VS Code z rozszerzeniem C#.
- Dostęp do `dotnet user-secrets`.

## Instalacja pakietów

W projekcie nie trzeba ręcznie instalować paczek z NuGet, bo są już wpisane w plikach `.csproj`. Wystarczy wykonać restore:

```bash
dotnet restore
```

Jeśli chcesz sprawdzić build od razu po pobraniu zależności:

```bash
dotnet build MailSender.sln
```

## User Secrets

Projekt używa `User Secrets` do trzymania wrażliwych danych. W katalogu projektu API ustaw wymagane sekrety:

```bash
cd MailSender.Api
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "twoj_super_dlugi_i_losowy_klucz"
dotnet user-secrets set "ExpectedClientPassword" "haslo_do_rejestracji"
```

Jeśli później podłączysz Brevo, dodaj tam również klucz API, zamiast trzymać go w `appsettings.json`.

## Uruchomienie

### Z konsoli

```bash
dotnet run --project MailSender.Api
```

### W Visual Studio

- Otwórz `MailSender.sln`.
- Ustaw `MailSender.Api` jako projekt startowy.
- Uruchom aplikację w trybie `Development`.

## Jak używać

### 1. Rejestracja klienta

Wyślij `POST /client-app/register` z danymi:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "pass": "haslo_do_rejestracji"
}
```

W odpowiedzi dostaniesz token JWT (`key`).

### 2. Wysłanie maila

W Swaggerze kliknij `Authorize`, wklej token w formacie:

```text
Bearer twoj_token_jwt
```

Następnie wywołaj `POST /mail/send`.

## Co jest jeszcze potrzebne do wyższej oceny

- Integracja z prawdziwym dostawcą e-mail, np. Brevo.
- Przeniesienie wysyłki maila z logiki testowej do osobnego serwisu infrastruktury.
- Bezpieczne trzymanie klucza API do Brevo w `User Secrets` lub zmiennych środowiskowych.
- Testy end-to-end dla scenariusza: rejestracja -> token JWT -> wysyłka wiadomości.
- Uporządkowanie odpowiedzi API i dodanie lepszej walidacji danych wejściowych.

## Notatka o Swaggerze

W Swaggerze możesz użyć `Authorize` do wklejenia JWT i odblokowania endpointów chronionych autoryzacją. Jeśli chcesz, żeby przy konkretnym endpointcie był widoczny lock icon, trzeba dodatkowo zadbać o poprawną adnotację `Authorize` i konfigurację Swaggera dla wymagań bezpieczeństwa.

## Aktualny stan zadań

- [x] Utworzenie projektu backendu .NET.
- [x] Konfiguracja JWT.
- [x] Swagger z obsługą tokena.
- [x] Rejestracja aplikacji klienta.
- [x] Wysyłka maila po autoryzacji.
- [ ] Integracja z Brevo.
- [ ] Podłączenie realnej wysyłki maila.
- [ ] Testy end-to-end.
