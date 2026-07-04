# MailSender

MailSender to aplikacja Web API napisana w **ASP.NET Core (.NET 9)**, której zadaniem jest rejestracja aplikacji klienckich oraz bezpieczna wysyłka wiadomości e-mail z wykorzystaniem autoryzacji JWT i zewnętrznego dostawcy poczty **Brevo**.

Projekt został wykonany w ramach zajęć z tworzenia aplikacji backendowych.

---

# Funkcjonalności

Aplikacja umożliwia:

- rejestrację aplikacji klienckiej,
- generowanie tokenu JWT ważnego przez 90 dni,
- autoryzację endpointów z wykorzystaniem JWT,
- wysyłanie wiadomości e-mail po poprawnej autoryzacji,
- integrację z zewnętrznym dostawcą poczty **Brevo**,
- modyfikację treści wiadomości zgodnie z wymaganiami biznesowymi,
- konfigurację poufnych danych z wykorzystaniem **User Secrets**.

---

# Zrealizowane wymagania projektu (Basic 3.5)

✔ Utworzenie projektu backendowego ASP.NET Core

✔ Konfiguracja uwierzytelniania JWT

✔ Dokumentacja API przy użyciu Swagger

✔ Endpoint rejestracji aplikacji:

```
POST /client-app/register
```

✔ Endpoint wysyłki wiadomości:

```
POST /mail/send
```

✔ Integracja z usługą Brevo

✔ Rzeczywista wysyłka wiadomości e-mail

✔ Test pełnego przepływu działania aplikacji

---

# Reguły biznesowe

Przed wysłaniem wiadomości aplikacja automatycznie wykonuje następujące operacje:

### 1. Modyfikacja tematu

Jeżeli temat wiadomości kończy się znakiem zapytania (`?`), dodawany jest prefiks:

```
[Q]
```

Przykład:

```
Czy działa?
```

zamienia się na:

```
[Q] Czy działa?
```

---

### 2. Oznaczanie nazwiska

Jeżeli treść wiadomości zawiera nazwisko autora projektu, zostaje ono automatycznie oznaczone:

```
[student.surname]Nazwisko[/student.surname]
```

Przykład:

```
Test Koń
```

zamienia się na:

```
Test [student.surname]Koń[/student.surname]
```

---

# Architektura projektu

Projekt został podzielony na trzy warstwy:

## MailSender.Api

Warstwa prezentacji.

Zawiera:

- kontrolery,
- konfigurację JWT,
- konfigurację Swagger,
- konfigurację Dependency Injection.

---

## MailSender.Core

Warstwa logiki biznesowej.

Zawiera:

- modele danych,
- serwisy odpowiedzialne za przetwarzanie wiadomości,
- interfejs `IMailSenderProvider`.

---

## MailSender.Infrastructure

Warstwa infrastruktury.

Zawiera implementację wysyłki wiadomości z wykorzystaniem usługi **Brevo**.

---

# Wykorzystane technologie

- .NET 9
- ASP.NET Core Web API
- JWT Authentication
- Swagger / OpenAPI
- Brevo Email API
- Dependency Injection
- User Secrets

---

# Wymagania

- .NET SDK 9.0
- Visual Studio 2022 lub Visual Studio Code
- konto Brevo
- zweryfikowany nadawca w Brevo

---

# Instalacja

Przywrócenie pakietów:

```bash
dotnet restore
```

Kompilacja:

```bash
dotnet build MailSender.sln
```

Uruchomienie:

```bash
dotnet run --project MailSender.Api
```

---

# Konfiguracja User Secrets

Projekt wykorzystuje **User Secrets** do przechowywania poufnych danych.

W katalogu projektu API wykonaj:

```bash
dotnet user-secrets init
```

Następnie ustaw wymagane wartości:

```bash
dotnet user-secrets set "JwtSettings:SecretKey" "twoj_bardzo_dlugi_klucz"
dotnet user-secrets set "ExpectedClientPassword" "q##waQXX"

dotnet user-secrets set "Brevo:ApiKey" "xkeysib-..."
dotnet user-secrets set "Brevo:SenderEmail" "twoj@email.pl"
dotnet user-secrets set "Brevo:SenderName" "MailSender"
```

> Dane poufne nie są przechowywane w repozytorium Git.

---

# Uruchomienie aplikacji

Po uruchomieniu aplikacji dostępny będzie Swagger:

```
https://localhost:xxxx/swagger
```

---

# Przykładowy przebieg działania

## 1. Rejestracja aplikacji

```
POST /client-app/register
```

Body:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "pass": "q##waQXX"
}
```

Odpowiedź:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "key": "JWT_TOKEN"
}
```

---

## 2. Autoryzacja

W Swaggerze wybierz przycisk **Authorize** i wklej:

```
Bearer JWT_TOKEN
```

---

## 3. Wysłanie wiadomości

```
POST /mail/send
```

Przykład:

```json
{
  "to": "example@email.com",
  "subject": "Czy działa?",
  "body": "Test Koń"
}
```

Przykładowa odpowiedź:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "status": "queued",
  "email": {
    "to": "example@email.com",
    "subject": "[Q] Czy działa?",
    "body": "Test [student.surname]Koń[/student.surname]"
  }
}
```

---

# Test działania

Przetestowano poprawność działania aplikacji w środowisku lokalnym.

Zweryfikowano pełny scenariusz:

1. Rejestracja aplikacji.
2. Wygenerowanie tokenu JWT.
3. Autoryzacja w Swagger.
4. Wysłanie wiadomości.
5. Integracja z Brevo.
6. Dostarczenie wiadomości e-mail.

---

# Autorzy

Projekt wykonany w ramach zajęć laboratoryjnych przez : 
-Szymon Koń 
-Grzegorz Marczak
-Konrad Francuz
-Jakub Cybak
