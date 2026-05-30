### 📋 Lista Zadań (To-Do)

- [x] **Zadanie 1: Utworzenie projektu**
  - [x] Utworzenie projektu backendu dotnet o nazwie `MailSender`.

- [x] **Zadanie 2: Konfiguracja autentykacji**
  - [x] Skonfigurowanie autentykacji tokenowej (JWT).

- [x] **Zadanie 3: Dokumentacja API**
  - [x] Dodanie i zweryfikowanie poprawnego działania SwaggerUI z obsługą tokenów (przycisk Authorize).

- [x] **Zadanie 4: Endpoint rejestracji klienta (`POST /client-app/register`)**
  - [x] Przyjmowanie payloadu JSON (`appId`, `appName`, `pass`).
  - [x] Zwracanie statusu `403 Forbidden` w przypadku błędnego hasła.
  - [x] **Wymaganie:** Niezapisywanie hasła (hardcoding) w kodzie aplikacji (zastosowano `User Secrets`).
  - [x] Zwracanie wygenerowanego tokena JWT (`key`) ważnego 90 dni.
  - [x] Zwracanie poprawnie sformatowanego JSON-a po rejestracji (`appId`, `appName`, `key`).

- [x] **Zadanie 5: Endpoint wysyłki wiadomości (`POST /mail/send`)**
  - [x] Objęcie endpointu autentykacją (dostęp tylko z ważnym tokenem).
  - [x] Przyjmowanie payloadu JSON (`to`, `subject`, `body`).
  - [x] **Wymaganie biznesowe:** Dodawanie prefixu `[Q]` do tematu zakończonego znakiem zapytania.
  - [x] **Wymaganie biznesowe:** Dodawanie prefixu `[student.suname]` przed i `[/student.suname]` za nazwiskiem w treści.
  - [x] Zwracanie poprawnej sygnatury odpowiedzi ze statusem `"queued"`.
  - [x] **Wymaganie:** Poprawne odczytywanie `appId` oraz `appName` z tokena JWT bez użycia bazy danych.

- [ ] **Zadanie 6: Serwis integracyjny z dostawcą e-mail (Brevo)**
  - [ ] Założenie konta i pobranie klucza API z https://www.brevo.com/.
  - [ ] Zaimplementowanie prostego serwisu komunikującego się z API Brevo.
  - [ ] Umożliwienie wysłania wiadomości z przekazaniem parametrów: Nadawca (To), Temat (Subject), Treść (Body).
  - [ ] **Wymaganie:** Zabezpieczenie danych autentykacyjnych do Brevo (klucz API nie może znajdować się w kodzie ani pliku appsettings).

- [ ] **Zadanie 7: Podpięcie serwisu wysyłkowego**
  - [ ] Użycie implementacji serwisu Brevo bezpośrednio w endpoincie `/mail/send`.

- [ ] **Zadanie 8: Testy końcowe (End-to-End)**
  - [ ] Sprawdzenie poprawności działania całej aplikacji: rejestracja nowej aplikacji i udane dostarczenie testowej wiadomości na fizyczną skrzynkę pocztową.
