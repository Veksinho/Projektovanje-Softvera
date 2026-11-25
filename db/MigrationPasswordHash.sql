/* ============================================================================
   Sistem za preprodaju karata  -  Faza 3: preimenovanje kolone Broker.sifra
   u Broker.sifraHash (docs/HESIRANJE-SIFARA.md, H3)

   Redosled prelaska razvojne baze na heširane šifre:
     1. Bekap baze Seminarski-Softveri.
     2. Ova skripta (db/MigrationPasswordHash.sql): preimenuje kolonu.
     3. Alat MigracijaSifara (Projektovanje-Softvera/MigracijaSifara):
        hešira postojeće vrednosti koje još nisu heš.
     4. Pokretanje servera.

   Tip kolone ostaje nvarchar(100) NOT NULL.
   Skripta sme da se pokrene više puta: ako je kolona već preimenovana,
   samo ispiše poruku i ništa ne menja.
   ============================================================================ */

USE [Seminarski-Softveri];
GO

IF COL_LENGTH(N'dbo.Broker', N'sifra') IS NOT NULL
   AND COL_LENGTH(N'dbo.Broker', N'sifraHash') IS NULL
BEGIN
    EXEC sp_rename N'dbo.Broker.sifra', N'sifraHash', N'COLUMN';
    PRINT N'Kolona dbo.Broker.sifra je preimenovana u sifraHash.';
END
ELSE IF COL_LENGTH(N'dbo.Broker', N'sifraHash') IS NOT NULL
BEGIN
    PRINT N'Kolona dbo.Broker.sifraHash već postoji (kolona je već preimenovana). Nema izmena.';
END
ELSE
BEGIN
    RAISERROR(N'Tabela dbo.Broker nema ni kolonu sifra ni kolonu sifraHash. Proverite bazu.', 16, 1);
END
GO
