namespace Common.Validacija
{
    public static class Ogranicenja
    {
        public const int BrokerKorisnickoIme = 50;
        public const int BrokerSifra = 100;
        public const int BrokerIme = 50;
        public const int BrokerPrezime = 50;
        public const int BrokerTelefon = 30;

        public const int DogadjajNaziv = 150;
        public const int DogadjajMesto = 150;

        public const int FizickoLiceJmbg = 13;
        public const int FizickoLiceIme = 50;
        public const int FizickoLicePrezime = 50;
        public const int FizickoLiceBrojLicneKarte = 20;

        public const int KartaSektor = 50;
        public const int KartaRed = 10;
        public const int KartaSediste = 10;
        public const int KartaTip = 20;
        public const int KartaFormat = 20;
        public const int KartaStatus = 20;

        public const int KategorijaDogadjajaNaziv = 100;
        public const int KategorijaDogadjajaOpis = 500;

        public const int KonsignatorEmail = 100;
        public const int KonsignatorTelefon = 30;
        public const int KonsignatorAdresa = 200;

        public const int ListingStatus = 20;
        public const int ListingSplit = 20;
        public const int ListingNapomena = 1000;

        public const int PravnoLicePib = 9;
        public const int PravnoLiceMaticniBroj = 8;
        public const int PravnoLiceNaziv = 150;

        public const int JmbgDuzina = 13;
        public const int PibDuzina = 9;
        public const int MaticniBrojDuzina = 8;
        public const int BrojLicneKarteDuzina = 9;
        public const int TelefonMinCifara = 6;
        public const int TelefonMaxCifara = 15;
        public const int SifraMin = 8;
        public const int SifraMax = 64;
        public const decimal MaxIznos = 99_999_999.99m;
        public const decimal MaxProvizija = 100m;
        public const int BrojDecimala = 2;

        public const int TelefonMaxLength = TelefonMaxCifara + 1;
        public const int NazivKonsignatoraPretragaMaxLength = 150;
    }
}
