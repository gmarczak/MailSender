# MailSender

MailSender to aplikacja backendowa napisana w **ASP.NET Core (.NET 9)** umożliwiająca rejestrację aplikacji klienckich oraz wysyłanie wiadomości e-mail z wykorzystaniem tokenów JWT i usługi **Brevo**.

Projekt został wykonany w ramach przedmiotu **Programowanie Aplikacji ** na WSEI.

---

# Główne funkcjonalności

✔ Rejestracja aplikacji klienckiej

✔ Autoryzacja JWT

✔ Dokumentacja API w Swagger

✔ Wysyłanie wiadomości e-mail przez Brevo

✔ Przetwarzanie wiadomości zgodnie z wymaganiami projektu

✔ WebClient umożliwiający wysyłanie wiadomości z poziomu przeglądarki

✔ Przechowywanie poufnych danych z wykorzystaniem User Secrets

---

# Technologie

Projekt został wykonany z wykorzystaniem:

- ASP.NET Core (.NET 9)
- C#
- JWT Authentication
- Swagger / OpenAPI
- Brevo Email API
- Dependency Injection
- HttpClient
- User Secrets
- HTML
- JavaScript

---

# Architektura projektu

Projekt został podzielony na trzy warstwy.

```
MailSender
│
├── MailSender.Api
│
│   • Kontrolery
│   • Konfiguracja JWT
│   • Swagger
│   • Dependency Injection
│
├── MailSender.Core
│
│   • Modele
│   • Interfejsy
│   • Logika biznesowa
│
├── MailSender.Infrastructure
│
│   • BrevoMailSenderProvider
│   • Integracja z API Brevo
│
└── WebClient
    • HTML
    • JavaScript
```

Takie rozdzielenie pozwala oddzielić logikę aplikacji od komunikacji z zewnętrznym dostawcą wiadomości.

---

# Funkcjonalność API

## Rejestracja aplikacji

Endpoint

```
POST /client-app/register
```

umożliwia zarejestrowanie aplikacji klienckiej.

Przykładowe żądanie

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "pass": "q##waQ53"
}
```

Po poprawnej weryfikacji zwracany jest token JWT ważny przez **90 dni**.

Przykładowa odpowiedź

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "key": "eyJhbGciOi..."
}
```

---

## Wysyłanie wiadomości

Endpoint

```
POST /mail/send
```

jest zabezpieczony tokenem JWT.

Do wykonania żądania wymagane jest wcześniejsze uzyskanie tokena podczas rejestracji aplikacji.

Przykładowe dane

```json
{
  "to": "example@gmail.com",
  "subject": "Czy działa?",
  "body": "Test Koń"
}
```

---

# Logika biznesowa

Przed wysłaniem wiadomości aplikacja wykonuje dodatkowe operacje wymagane przez specyfikację projektu.

### 1. Modyfikacja tematu wiadomości

Jeżeli temat wiadomości kończy się znakiem zapytania

```
?
```

automatycznie dodawany jest prefiks

```
[Q]
```

Przykład

```
Czy działa?
```

↓

```
[Q] Czy działa?
```

---

### 2. Oznaczanie nazwiska

Jeżeli treść wiadomości zawiera nazwisko autora projektu

```
Koń
```

zostaje ono automatycznie oznaczone

```
[student.surname]Koń[/student.surname]
```

zgodnie z wymaganiami projektu.

---

# Integracja z Brevo

Projekt wykorzystuje REST API usługi **Brevo** do rzeczywistego wysyłania wiadomości e-mail.

Komunikacja realizowana jest przez klasę

```
BrevoMailSenderProvider
```

wykorzystującą klasę `HttpClient`.

Poufne dane (API Key oraz dane nadawcy) przechowywane są poza repozytorium z wykorzystaniem **User Secrets**.

---

# Swagger

Projekt posiada pełną dokumentację API wygenerowaną przez Swagger.

Swagger umożliwia:

- rejestrację aplikacji,
- pobranie tokena JWT,
- autoryzację przy pomocy przycisku **Authorize**,
- testowanie wszystkich endpointów bez użycia zewnętrznych narzędzi.

---

# WebClient

W projekcie znajduje się prosty klient demonstracyjny.

```
WebClient/
```

Aplikacja umożliwia:

- wklejenie tokena JWT,
- podanie odbiorcy,
- wpisanie tematu,
- wpisanie treści wiadomości,
- wysłanie wiadomości do backendu,
- wyświetlenie odpowiedzi API.

Dzięki temu możliwe jest przetestowanie działania backendu również bez korzystania ze Swaggera.

---

# Konfiguracja

Po pobraniu projektu należy skonfigurować User Secrets.

```
cd MailSender.Api
```

```
dotnet user-secrets init
```

Następnie dodać wymagane sekrety.

```
JwtSettings:SecretKey
ExpectedClientPassword
Brevo:ApiKey
Brevo:SenderEmail
Brevo:SenderName
```

Dzięki temu poufne dane nie są przechowywane w repozytorium Git.

---

# Uruchomienie projektu

Przywrócenie zależności

```
dotnet restore
```

Uruchomienie projektu

```
dotnet run --project MailSender.Api
```

Swagger dostępny jest pod adresem

```
http://localhost:5134/swagger
```

---

# Test działania

Projekt został przetestowany w dwóch scenariuszach.

### Swagger

- rejestracja aplikacji,
- wygenerowanie tokena JWT,
- autoryzacja,
- wysłanie wiadomości,
- otrzymanie wiadomości e-mail.

### WebClient

- wpisanie tokena,
- wpisanie danych wiadomości,
- wysłanie wiadomości,
- odebranie odpowiedzi API.

---

# Zrealizowane wymagania

| Wymaganie | Status |
|-----------|:------:|
| Backend ASP.NET Core | ✅ |
| JWT Authentication | ✅ |
| Swagger | ✅ |
| Rejestracja aplikacji | ✅ |
| Token JWT (90 dni) | ✅ |
| Endpoint chroniony JWT | ✅ |
| Prefix **[Q]** | ✅ |
| Tagowanie nazwiska | ✅ |
| Integracja z Brevo | ✅ |
| User Secrets | ✅ |
| WebClient HTML/JavaScript | ✅ |
| Test działania | ✅ |

---

# Autorzy

Projekt wykonany w ramach przedmiotu **Programowanie Aplikacji Backendowych**.

```
