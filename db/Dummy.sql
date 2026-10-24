/* ============================================================================
   Sistem za preprodaju karata  -  ubacivanje dummy (probnih) podataka
   Namena: brzo popunjavanje baze radi testiranja aplikacije

   Sadrzaj:
     - 1  Broker
     - 3  KategorijaDogadjaja
     - 3  Dogadjaj
     - 2  Konsignator  (1 FizickoLice + 1 PravnoLice)
     - 10 Karta

   Napomena: Listing NIJE trazen, pa sve karte ostaju u statusu 'u_inventaru'
   sa idListing = NULL - ovo je i model dozvoljava (karta moze postojati u
   inventaru pre nego sto se plasira na listing).

   Id-jevi se hvataju kroz SCOPE_IDENTITY() odmah posle svakog INSERT-a, tako
   da skripta radi bez obzira da li su tabele prazne ili vec sadrze podatke.
   ============================================================================ */

DECLARE @idBroker INT;
DECLARE @idKat1 INT, @idKat2 INT, @idKat3 INT;
DECLARE @idDog1 INT, @idDog2 INT, @idDog3 INT;
DECLARE @idKons1 INT, @idKons2 INT;


/* ============================================================================
   1. Broker (1)
   ============================================================================ */
INSERT INTO Broker (korisnickoIme, sifra, ime, prezime, telefon)
VALUES (N'vtonic', N'Sifra#2026', N'Veljko', N'Tonić', N'0641234567');
SET @idBroker = SCOPE_IDENTITY();


/* ============================================================================
   2. KategorijaDogadjaja (3)
   ============================================================================ */
INSERT INTO KategorijaDogadjaja (naziv, opis)
VALUES (N'Koncert', N'Muzički koncerti i nastupi izvođača');
SET @idKat1 = SCOPE_IDENTITY();

INSERT INTO KategorijaDogadjaja (naziv, opis)
VALUES (N'Sportski događaj', N'Utakmice i sportska takmičenja');
SET @idKat2 = SCOPE_IDENTITY();

INSERT INTO KategorijaDogadjaja (naziv, opis)
VALUES (N'Pozorišna predstava', N'Pozorišne i scenske predstave');
SET @idKat3 = SCOPE_IDENTITY();


/* ============================================================================
   3. Dogadjaj (3)
   Napomena: Dogadjaj nema FK na KategorijaDogadjaja u ovoj semi
   (KategorijaDogadjaja je M:N vezana za Broker preko BrKd, ne za Dogadjaj),
   pa se ovde idKatN ne koristi - ostavljen je u skripti samo zbog preglednosti.
   ============================================================================ */
INSERT INTO Dogadjaj (naziv, datumOdrzavanja, mesto)
VALUES (N'Koncert - Ekvinocijo Tribute Show', '2026-11-15T20:00:00', N'Kombank Arena, Beograd');
SET @idDog1 = SCOPE_IDENTITY();

INSERT INTO Dogadjaj (naziv, datumOdrzavanja, mesto)
VALUES (N'FK Partizan - FK Crvena zvezda', '2026-10-03T18:00:00', N'Stadion Partizana, Beograd');
SET @idDog2 = SCOPE_IDENTITY();

INSERT INTO Dogadjaj (naziv, datumOdrzavanja, mesto)
VALUES (N'Hamlet - Narodno pozorište', '2026-12-05T19:30:00', N'Narodno pozorište, Beograd');
SET @idDog3 = SCOPE_IDENTITY();


/* ============================================================================
   4. Konsignator (2)  ->  1x FizickoLice, 1x PravnoLice
   ============================================================================ */

-- 4a. Konsignator #1 (Fizičko lice)
INSERT INTO Konsignator (email, telefon, adresa, datumRegistracije)
VALUES (N'marko.markovic@gmail.com', N'0641112233', N'Bulevar Kralja Aleksandra 15, Beograd', '2026-01-10T09:00:00');
SET @idKons1 = SCOPE_IDENTITY();

INSERT INTO FizickoLice (idKonsignator, jmbg, ime, prezime, brojLicneKarte)
VALUES (@idKons1, N'0101990710025', N'Marko', N'Marković', N'012345678');

-- 4b. Konsignator #2 (Pravno lice)
INSERT INTO Konsignator (email, telefon, adresa, datumRegistracije)
VALUES (N'office@eventtix.rs', N'0113456789', N'Kneza Miloša 44, Beograd', '2026-02-20T11:30:00');
SET @idKons2 = SCOPE_IDENTITY();

INSERT INTO PravnoLice (idKonsignator, pib, maticniBroj, naziv)
VALUES (@idKons2, N'109876543', N'20654321', N'EventTix DOO Beograd');


/* ============================================================================
   5. Karta (10)
   idListing = NULL, status = 'u_inventaru' (Listing nije tražen)
   Raspoređeno naizmenično po događajima i konsignatorima radi raznovrsnosti.
   ============================================================================ */
INSERT INTO Karta (sektor, red, sediste, nominalnaCena, tip, format, status, idListing, idKonsignator, idDogadjaj)
VALUES
 (N'Sever',  N'A',  N'12', 4500.00, N'sedeca',  N'pdf',      N'u_inventaru', NULL, @idKons1, @idDog1),
 (N'Sever',  N'A',  N'13', 4500.00, N'sedeca',  N'pdf',      N'u_inventaru', NULL, @idKons1, @idDog1),
 (N'Parter', N'-',  N'-',  3000.00, N'stajaca', N'mobilna',  N'u_inventaru', NULL, @idKons1, @idDog1),
 (N'VIP',    N'1',  N'5',  15000.00, N'vip',     N'papirna',  N'u_inventaru', NULL, @idKons2, @idDog1),
 (N'Istok',  N'C',  N'22', 2500.00, N'sedeca',  N'pdf',      N'u_inventaru', NULL, @idKons2, @idDog2),
 (N'Istok',  N'C',  N'23', 2500.00, N'sedeca',  N'pdf',      N'u_inventaru', NULL, @idKons2, @idDog2),
 (N'Sky Box',N'-',  N'2',  20000.00, N'sky_box', N'rfid',     N'u_inventaru', NULL, @idKons2, @idDog2),
 (N'Zapad',  N'F',  N'8',  1800.00, N'sedeca',  N'mobilna',  N'u_inventaru', NULL, @idKons1, @idDog3),
 (N'Zapad',  N'F',  N'9',  1800.00, N'sedeca',  N'mobilna',  N'u_inventaru', NULL, @idKons1, @idDog3),
 (N'Balkon', N'G',  N'3',  1200.00, N'sedeca',  N'papirna',  N'u_inventaru', NULL, @idKons2, @idDog3);
GO


/* ============================================================================
   Provera unetih podataka
   ============================================================================ */
SELECT * FROM Broker;
SELECT * FROM KategorijaDogadjaja;
SELECT * FROM Dogadjaj;
SELECT * FROM Konsignator;
SELECT * FROM FizickoLice;
SELECT * FROM PravnoLice;
SELECT * FROM Karta;
GO