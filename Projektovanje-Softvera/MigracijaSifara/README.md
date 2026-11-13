# MigracijaSifara

Konzolni alat za prelazak razvojne baze `Seminarski-Softveri` na heširane šifre brokera (Faza 3, `docs/HESIRANJE-SIFARA.md`, H3).

## Namena

- **Migracija:** postojeće šifre u koloni `dbo.Broker.sifraHash` koje još nisu heš (ne počinju sa `$2`) hešira preko `PasswordHasher.Hash` i upisuje heš na isto mesto. Sve se radi u jednoj transakciji. Alat sme da se pokrene više puta: već heširane vrednosti preskače, a kad nema šta da radi, to i kaže.
- **Heširanje jedne šifre (`--hesiraj`):** ispisuje samo BCrypt heš zadate šifre, bez pristupa bazi. Koristi se za ručno pisane SQL skripte i kasnije za generator podataka.

Baza i work factor se čitaju iz `appsettings.json` ovog projekta (`ConnectionStrings:SeminarskiSoftveri`, `PasswordHashing:WorkFactor` = 11, kao na serveru). Alat odbija da radi nad bazom čiji naziv završava na `_Test`, jer se test baza puni iz `ResetujBazu`. Šifre i heševe iz baze nikad ne ispisuje.

## Redosled koraka

1. Bekap baze `Seminarski-Softveri`.
2. `db/MigrationPasswordHash.sql` – preimenuje kolonu `Broker.sifra` u `sifraHash`.
3. Ovaj alat bez argumenata – hešira postojeće šifre.
4. Pokretanje servera.

Ako kolona `sifraHash` ne postoji, alat javlja da prvo treba pokrenuti korak 2 i ne menja ništa.

## Primeri poziva

Iz foldera `Projektovanje-Softvera`:

```bash
dotnet run --project MigracijaSifara
```

Ispisuje server i bazu, broj brokera, broj već heširanih i broj onih koje treba heširati, pa traži potvrdu `Nastaviti? (d/n)`. Na kraju ispisuje koliko je redova promenjeno.

```bash
dotnet run --project MigracijaSifara -- --hesiraj "Sifra#2026"
```

Ispisuje samo heš, npr. `$2a$11$...`. Svaki poziv daje drugačiji heš (nova so), a svi prolaze `PasswordHasher.Verify`.

Izlazni kôd je 0 kad je posao završen ili nije bilo šta da se radi, a 1 kod greške ili odbijenog pokretanja.
