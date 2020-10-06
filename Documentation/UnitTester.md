# Unit Tester

Vi ville ha många tester som körs när man pushar kod igenom våra pipelines, så att vi med mer säkerhet
pushar bra kod som fungerar och även så att vi får se hur det fungerar med automatiska tester.



Vi har gjort unit tester som testar olika delar av vårat backend API, där vi gör en Mock av våran
databas och kör tester för att se till att vi får fram rätt information. Detta hjälper extremt mycket
om man mot förmodan skulle vilja ändra något. Till exempel uppdatera databasstrukturen eller
refaktorera en Repository call till databasen.



Har försökt att göra tre tester per metod. Testerna som körs är med korrekt data så att man får ut rätt
saker, men vi testar även gränsfall och om det inte finns något att få tillbaka, så att respons blir
null eller empty när den ska det.

## Exempel

![](Bilder/UnitTest.jpg)

Här ser man ett exempel på ett UnitTest.

Där vi kan se hur vi skapar upp data till tester genom `GenerateParkingspotData()`. Sedan så instansierar
vi Mocken för vårat context. Ger den datan som vi skapade innan och Mockar en logger. För att sist
instasiera upp vårat ParkingspotRepository med data och mock loggern.

I Act i detta exemplet så försöker vi hämta parkeringsplats med id 99999 vilket inte kommer att finnas
i vårat program

Sist så ser vi till att vårat svar är null precis som vi vill och inte en array med 0 i size eller något
annat.
