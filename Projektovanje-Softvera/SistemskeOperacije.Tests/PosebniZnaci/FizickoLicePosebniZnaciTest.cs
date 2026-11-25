using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class FizickoLicePosebniZnaciTest : IntegracioniTest
    {
        private static FizickoLice Pretrazi(int id)
        {
            PretraziKonsignatorSO so = new PretraziKonsignatorSO(new FizickoLice { IdKonsignator = id });
            so.ExecuteTemplate();
            return (FizickoLice)so.Result!;
        }

        private static List<Konsignator> VratiListu(Konsignator kriterijum)
        {
            VratiListuKonsignatorSO so = new VratiListuKonsignatorSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Konsignator>)so.Result!;
        }

        private static FizickoLice Kreiraj(string tekst)
        {
            FizickoLice fizickoLice = TestPodaci.NapraviFizickoLice();
            fizickoLice.Ime = tekst;
            fizickoLice.Prezime = tekst;
            fizickoLice.Adresa = tekst;
            fizickoLice.Email = TestPodaci.EmailSaApostrofom;
            new KreirajKonsignatorSO(fizickoLice).ExecuteTemplate();
            return fizickoLice;
        }

        private static void ProveriPolja(string tekst, FizickoLice sacuvano)
        {
            Assert.AreEqual(tekst, sacuvano.Ime);
            Assert.AreEqual(tekst, sacuvano.Prezime);
            Assert.AreEqual(tekst, sacuvano.Adresa);
            Assert.AreEqual(TestPodaci.EmailSaApostrofom, sacuvano.Email);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            FizickoLice fizickoLice = Kreiraj(tekst);

            ProveriPolja(tekst, Pretrazi(fizickoLice.IdKonsignator));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            FizickoLice fizickoLice = TestPodaci.KreirajFizickoLice();
            fizickoLice.Ime = tekst;
            fizickoLice.Prezime = tekst;
            fizickoLice.Adresa = tekst;
            fizickoLice.Email = TestPodaci.EmailSaApostrofom;

            new PromeniKonsignatorSO(fizickoLice).ExecuteTemplate();

            ProveriPolja(tekst, Pretrazi(fizickoLice.IdKonsignator));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoImenuIPrezimenu(string tekst)
        {
            TestPodaci.KreirajFizickoLice();
            FizickoLice fizickoLice = Kreiraj(tekst);
            string deo = TestPodaci.DeoTeksta(tekst);

            List<Konsignator> rezultat = VratiListu(new FizickoLice { Ime = deo, Prezime = deo });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(fizickoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual(tekst, ((FizickoLice)rezultat[0]).Ime);
            Assert.AreEqual(tekst, ((FizickoLice)rezultat[0]).Prezime);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoAdresi(string tekst)
        {
            TestPodaci.KreirajFizickoLice();
            FizickoLice fizickoLice = Kreiraj(tekst);

            List<Konsignator> rezultat = VratiListu(new Konsignator { Adresa = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(fizickoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual(tekst, rezultat[0].Adresa);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoNazivKriterijumu(string tekst)
        {
            TestPodaci.KreirajFizickoLice();
            TestPodaci.KreirajPravnoLice();
            FizickoLice fizickoLice = Kreiraj(tekst);

            List<Konsignator> rezultat = VratiListu(new Konsignator { NazivKriterijum = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(fizickoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual($"{tekst} {tekst}", rezultat[0].Name);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPretragaPoEmailuSaApostrofom()
        {
            TestPodaci.KreirajFizickoLice();
            FizickoLice fizickoLice = Kreiraj(TestPodaci.Apostrof);

            List<Konsignator> rezultat = VratiListu(new Konsignator { Email = "o'brien" });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(fizickoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual(TestPodaci.EmailSaApostrofom, rezultat[0].Email);
        }
    }
}
