Rozwiązanie zadania "Kursy Walut" dla Aveneo.

Zapimplementowane zostały 2 endpointy:

GeyKey - zwaca nowo wygenerowany klucz api

GetCurrencyRates - zwraca on kursy walut zgodnie ze specyfikacją zapytania podawaną w "body" zapytania.
Wzór body:
{
  "currencyCodes": {
    "PLN" : "EUR"
  },
  "startDate": "2021-09-09",
  "endDate": "2021-09-09",
  "apiKey": "APIKey"
}

Oba Endpointy można testować za pomocą zaimplementowanego swaggera.

Testy integracyjne zostały wykonane za pomocą biblioteki xUnit.
