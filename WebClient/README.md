# WebClient

Folder zawiera prosty klient demonstracyjny dla projektu MailSender.

## Zawartość

- `index.html` - strona HTML z formularzem wysyłki wiadomości.
- `app.js` - prosty klient JavaScript wykorzystujący `fetch`.
- `style.css` - podstawowe style strony.
- `typescript-client` - klient TypeScript zgodny ze strukturą klienta generowanego z OpenAPI.
- `javascript-client` - uproszczona wersja klienta JavaScript.
- `openapi-mail-sender.json` - lokalna specyfikacja OpenAPI endpointów używanych przez klienta.

## Użycie

1. Uruchom API na HTTP:

```bash
dotnet run --project MailSender.Api
```

2. Wejdź w Swagger:

```text
http://localhost:5134/swagger
```

3. Zarejestruj aplikację przez `/client-app/register` i skopiuj wartość `key`.
4. Otwórz `WebClient/index.html` w przeglądarce.
5. Wklej token JWT do pola formularza.
6. Uzupełnij odbiorcę, temat i treść.
7. Kliknij `Wyślij maila`.

## Generowanie klienta TypeScript z OpenAPI

Docelowe polecenie do wygenerowania klienta TypeScript:

```bash
npx @openapitools/openapi-generator-cli generate -i http://localhost:5134/swagger/v1/swagger.json -g typescript-fetch -o WebClient/typescript-client-generated
```

Jeżeli lokalny `npm` ma problem z uprawnieniami, można użyć folderu `typescript-client`, który zawiera przygotowaną implementację klienta TypeScript dla endpointów projektu.

## Uwaga o CORS

Jeśli przeglądarka blokuje żądanie z `index.html`, w API należy włączyć CORS dla klienta testowego.
