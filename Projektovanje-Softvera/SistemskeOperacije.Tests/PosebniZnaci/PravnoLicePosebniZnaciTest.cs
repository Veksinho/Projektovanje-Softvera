using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PravnoLicePosebniZnaciTest : IntegracioniTest
    {
        private static PravnoLice Pretrazi(int id)
        {
            PretraziKonsignatorSO so = new PretraziKonsignatorSO(new PravnoLice { IdKonsignator = id });
            so.ExecuteTemplate();
            return (PravnoLice)so.Result!;
        }

        private static List<Konsignator> VratiListu(Konsignator kriterijum)
        {
            VratiListuKonsignatorSO so = new VratiListuKonsignatorSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Konsignator>)so.Result!;
        }

        private static PravnoLice Kreiraj(string tekst)
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.NazivFirme = tekst;
            pravnoLice.Adresa = tekst;
            pravnoLice.Email = TestPodaci.EmailSaApostrofom;
            new KreirajKonsignatorSO(pravnoLice).ExecuteTemplate();
            return pravnoLice;
        }

        private static void ProveriPolja(string tekst, PravnoLice sacuvano)
        {
            Assert.AreEqual(tekst, sacuvano.NazivFirme);
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
            PravnoLice pravnoLice = Kreiraj(tekst);

            ProveriPolja(tekst, Pretrazi(pravnoLice.IdKonsignator));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            PravnoLice pravnoLice = TestPodaci.KreirajPravnoLice();
            pravnoLice.NazivFirme = tekst;
            pravnoLice.Adresa = tekst;
            pravnoLice.Email = TestPodaci.EmailSaApostrofom;

            new PromeniKonsignatorSO(pravnoLice).ExecuteTemplate();

            ProveriPolja(tekst, Pretrazi(pravnoLice.IdKonsignator));
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoNazivuFirme(string tekst)
        {
            TestPodaci.KreirajPravnoLice();
            PravnoLice pravnoLice = Kreiraj(tekst);

            List<Konsignator> rezultat = VratiListu(new PravnoLice { NazivFirme = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(pravnoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual(tekst, ((PravnoLice)rezultat[0]).NazivFirme);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoAdresi(string tekst)
        {
            TestPodaci.KreirajPravnoLice();
            PravnoLice pravnoLice = Kreiraj(tekst);

            List<Konsignator> rezultat = VratiListu(new Konsignator { Adresa = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(pravnoLice.IdKonsignator, rezultat[0].IdKonsignator);
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
            PravnoLice pravnoLice = Kreiraj(tekst);

            List<Konsignator> rezultat = VratiListu(new Konsignator { NazivKriterijum = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(pravnoLice.IdKonsignator, rezultat[0].IdKonsignator);
            Assert.AreEqual(tekst, rezultat[0].Name);
        }
    }
}
