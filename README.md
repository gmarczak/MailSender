# MailSender

Backend API napisane w ASP.NET Core umożliwiające rejestrację aplikacji klienckich, generowanie tokenów JWT oraz wysyłanie wiadomości e-mail z wykorzystaniem usługi Brevo.

Projekt został wykonany w ramach przedmiotu **Programowanie Aplikacji Biznesowych**.

---

# Funkcjonalności

## Rejestracja aplikacji

Publiczny endpoint:

```
POST /client-app/register
```

- rejestracja aplikacji klienckiej,
- weryfikacja hasła zgodnego z numerem indeksu,
- generowanie tokena JWT ważnego przez 90 dni,
- zwrócenie danych aplikacji oraz wygenerowanego tokena.

Przykładowa odpowiedź:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "key": "jwt_token..."
}
```

---

## Wysyłanie wiadomości

Chroniony endpoint:

```
POST /mail/send
```

Do wykonania żądania wymagany jest token JWT uzyskany podczas rejestracji aplikacji.

Przykładowe dane:

```json
{
  "to": "example@gmail.com",
  "subject": "Czy działa?",
  "body": "Test Koń"
}
```

---

## Logika biznesowa

Przed wysłaniem wiadomości wykonywane są dodatkowe operacje:

- jeżeli temat kończy się znakiem **?**, automatycznie dodawany jest prefiks

```
[Q]
```

- jeżeli treść zawiera nazwisko **Koń**, zostaje ono oznaczone tagami

```
[student.surname]Koń[/student.surname]
```

Przykład odpowiedzi:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "status": "queued",
  "email": {
    "to": "example@gmail.com",
    "subject": "[Q] Czy działa?",
    "body": "Test [student.surname]Koń[/student.surname]"
  }
}
```

---

# Wysyłanie wiadomości

Projekt wykorzystuje usługę **Brevo**.

Dane dostępowe nie są przechowywane w repozytorium i pobierane są z **User Secrets**.

---

# Swagger

Projekt posiada pełną dokumentację OpenAPI dostępną przez Swagger.

Swagger umożliwia:

- rejestrację aplikacji,
- pobranie tokena JWT,
- autoryzację przyciskiem **Authorize**,
- testowanie wszystkich endpointów.

---

# WebClient

Projekt zawiera prostego klienta demonstracyjnego znajdującego się w katalogu:

```
WebClient/
```

Klient umożliwia:

- wklejenie tokena JWT,
- podanie odbiorcy wiadomości,
- wpisanie tematu,
- wpisanie treści,
- wysłanie wiadomości do API,
- wyświetlenie odpowiedzi serwera.

---

# Struktura projektu

```
MailSender
│
├── MailSender.Api
│   ├── Controllers
│   ├── Program.cs
│   └── appsettings.json
│
├── MailSender.Core
│   ├── Models
│   └── Services
│
├── MailSender.Infrastructure
│   └── BrevoMailSenderProvider.cs
│
└── WebClient
```

---

# Wymagania

- .NET 9 SDK
- Visual Studio 2022 lub Visual Studio Code
- Konto Brevo
- User Secrets

---

# Konfiguracja User Secrets

Przejdź do projektu API:

```bash
cd MailSender.Api
```

Zainicjuj User Secrets:

```bash
dotnet user-secrets init
```

Dodaj wymagane wartości:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "YOUR_SECRET_KEY"

dotnet user-secrets set "ExpectedClientPassword" "q##waQ53"

dotnet user-secrets set "Brevo:ApiKey" "YOUR_BREVO_API_KEY"

dotnet user-secrets set "Brevo:SenderEmail" "your_email@gmail.com"

dotnet user-secrets set "Brevo:SenderName" "MailSender"
```

---

# Uruchomienie

Przywrócenie pakietów:

```bash
dotnet restore
```

Uruchomienie aplikacji:

```bash
dotnet run --project MailSender.Api
```

Swagger:

```
http://localhost:5134/swagger
```

---

# Test działania

1. Uruchom aplikację.
2. Otwórz Swagger.
3. Wywołaj **POST /client-app/register**.
4. Skopiuj wygenerowany token JWT.
5. Kliknij **Authorize** i wklej token.
6. Wywołaj **POST /mail/send**.
7. Sprawdź otrzymaną wiadomość e-mail.
8. Alternatywnie użyj klienta znajdującego się w katalogu **WebClient**.

---

# Zrealizowane wymagania

- ✔ Backend ASP.NET Core
- ✔ JWT Authentication
- ✔ Swagger / OpenAPI
- ✔ Rejestracja aplikacji
- ✔ Wysyłanie wiadomości po autoryzacji
- ✔ Modyfikacja tematu wiadomości `[Q]`
- ✔ Oznaczanie nazwiska tagami `[student.surname]`
- ✔ Integracja z Brevo
- ✔ User Secrets
- ✔ WebClient HTML/JavaScript
- ✔ Test poprawnego działania aplikacji
