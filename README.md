Zadanie polega na stworzeniu REST-owego API dostarczającego kursy walut w kontekście daty.
API powinno być zabezpieczone unikalnym kluczem przechowywanym lokalnie. Jego generowanie
powinno być możliwe korzystając z dodatkowego end-pointa. Każde odpytanie API powinno być
logowane lokalnie (forma dowolna).

Do zaimplementowania są więc dwa end-pointy:

1. Pobranie kursów walut z zakresu dat, dane wejściowe: Dictionary<string, string>
currencyCodes, DateTime startDate, DateTime endDate, string apiKey

2. Generowanie nowego klucza implementowanego API.
Objaśnienie: currencyCodes to kolekcja trzyliterowych kodów waluty (np. USD, EUR) gdzie klucz
słownika to waluta źródłowa, a wartość słownika to waluta docelowa; (start/end)Date to zakres
dat do pobrania kursu (w przypadku pobierania kursu dla pojedynczego dnia startDate = endDate);
apiKey to klucz do zweryfikowania dostępu do implementowanego API.

Dodatkowo, w przypadku odpytania API o datę z przyszłości należy zwrócić odpowiedni kod błędu
(404), a w przypadku odpytania API o kurs z przeszłości z dnia, w którym kurs nie był dostępny w
zewnętrznym API (np. dni wolne od pracy) należy zwrócić ostatni dostępny kurs walut (np. jeżeli
odpytanie nastąpiło o kurs z dnia 1 maja, należy zwrócić kurs z dnia 30 kwietnia).
