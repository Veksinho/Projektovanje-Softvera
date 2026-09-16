using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.BrokerSO;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    internal static class TestPodaci
    {
        private static string UniqueSuffix() => Guid.NewGuid().ToString("N")[..8];

        public static Broker KreirajBrokera(string password)
        {
            Broker broker = new Broker
            {
                KorisnickoIme = "test_" + UniqueSuffix(),
                Sifra = password,
                Ime = "Test",
                Prezime = "Broker",
                Telefon = "0601234567"
            };

            new KreirajBrokerSO(broker).ExecuteTemplate();
            return broker;
        }

        public static void ObrisiBrokera(int brokerId) =>
            TestBaza.Execute($"DELETE FROM BrKd WHERE idBroker = {brokerId}; DELETE FROM Broker WHERE idBroker = {brokerId};");

        public static Dogadjaj KreirajDogadjaj()
        {
            Dogadjaj dogadjaj = new Dogadjaj
            {
                Naziv = "Test događaj " + UniqueSuffix(),
                DatumOdrzavanja = DateTime.Today.AddMonths(1),
                Mesto = "Beograd"
            };

            new UbaciDogadjajSO(dogadjaj).ExecuteTemplate();
            return dogadjaj;
        }

        public static void ObrisiKarteDogadjaja(int dogadjajId) =>
            TestBaza.Execute($"DELETE FROM Karta WHERE idDogadjaj = {dogadjajId};");

        public static void ObrisiDogadjaj(int dogadjajId) =>
            TestBaza.Execute($"DELETE FROM Karta WHERE idDogadjaj = {dogadjajId}; DELETE FROM Dogadjaj WHERE idDogadjaj = {dogadjajId};");

        public static Konsignator VratiPostojecegKonsignatora()
        {
            VratiListuSviKonsignatorSO so = new VratiListuSviKonsignatorSO();
            so.ExecuteTemplate();
            List<Konsignator> consignors = (List<Konsignator>)so.Result!;

            if (consignors.Count == 0)
                Assert.Inconclusive("U bazi mora postojati bar jedan konsignator da bi se testovi izvršili.");

            return consignors[0];
        }

        public static Karta NapraviKartu(Dogadjaj dogadjaj, Konsignator konsignator,
            string sektor = "A", string red = "1", string sediste = "1") => new Karta
        {
            Sektor = sektor,
            Red = red,
            Sediste = sediste,
            NominalnaCena = 5000m,
            Tip = Enum.GetValues<TipKarte>()[0],
            Format = Enum.GetValues<FormatKarte>()[0],
            Dogadjaj = dogadjaj,
            Konsignator = konsignator
        };
    }
}
