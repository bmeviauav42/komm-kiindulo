# Mikroszolgáltatások és konténer alapú szoftverfejlesztés

## Kommunikációs lehetőségek - gyakorlat

A laborfeladatok célja a mikroszolgáltatások fejlesztése során leggyakrabban felmerülő megoldások alapszintű gyakorlása.

### Előkövetelmények
* Előadás anyaga: TODO
* Docker Desktop
* Visual Studio 2019 
    * min v16.3 
    * ASP.NET Core 3.0 SDK

### REST webszolgáltatások készítése

A labor külön nem tér ki a REST szerű webszolgáltatások készítésének módszereire, arra a szakirányos képzés és a Szoftverfejlesztés .NET platformra című választható tárgy ASP.NET Core anyaga az ajánlott irodalom.

Néhány ajánlott módszer a REST webszolgáltatások készítése során:
* Legyen a szolgáltatás állapotmentes
* Használjuk a HTTP protokoll adottságait
    * HTTP igék megfelelő használata
    * HTTP státuszkódok megfelelő használata
* Érdemes gondolni a kliens és a szerver oldali cache-elés lehetőségeire
    * Kliens oldali támogatáshoz gyenge és erős ETag használata
* Itt is használhatunk contract-first megközelítést
    * Swagger/OpenAPI leíróból kliens és szerver oldali kód generálása
* Authentikációt/Authorizációt törekedjünk a szabványos protokolokkal megoldani
    * JWT, OAuth 2.0, OpenID Connect

### Hibatűrő kommunikációs módszerek

Az előadáson tárgyalt tervezési minták nem csak a kommunikáció implementációja során hasznos, hanem bármilyen olyan komponens hívása során, ami nem várt tranziens hibajelenséget produkálhat. Tény, hogy leggyakrabban egy távoli hívás kommunikációja során történhet ilyen, így ott mindenképpen érdemes a hibatűrést valamilyen módon megvalósítani.

A laborfeladat során két ASP.NET Core mikroszolgáltatás közötti HTTP REST-es kommunikációt szeretnénk hibatűrőbbé tenni. A Polly osztálykönyvtárat hívjuk segítségül, ami a leggyakoribb mintákat megvalósítja, nekünk csak felkonfigurálnunk kell. Az egyszerűség kedvéért most a Retry miintát valósítsuk meg.

#### Kiinduló projekt áttekintése

Klónozzuk le a kiinduló projektet, és nyissuk meg a solutiont Visual Studio-val.

```
mkdir c:\munka\[neptun]\MSA\komm
cd c:\munka\[neptun]\MSA\komm
git clone TODO
```

Mind a két projekt Dockerizált (Projekten jobb gomb / Add / Docker support), a teljes solutionhöz pedig tartozik egy Docker Compose leíró (Projekteken jobb gomb / Add / Docker Orchestrator support / Docker Compose), amit egyben a futtatandó projekt is.
