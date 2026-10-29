using Common.Domen;
using SistemskeOperacije.KategorijaDogadjajaSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KategorijaDogadjajaPosebniZnaciTest : IntegracioniTest
    {
        private static KategorijaDogadjaja Pretrazi(int id)
        {
            PretraziKategorijaDogadjajaSO so = new PretraziKategorijaDogadjajaSO(
                new KategorijaDogadjaja { IdKategorijaDogadjaja = id });
            so.ExecuteTemplate();
            return (KategorijaDogadjaja)so.Result!;
        }

        private static List<KategorijaDogadjaja> VratiListu(KategorijaDogadjaja kriterijum)
        {
            VratiListuKategorijaDogadjajaSO so = new VratiListuKategorijaDogadjajaSO(kriterijum);
            so.ExecuteTemplate();
            return (List<KategorijaDogadjaja>)so.Result!;
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            KategorijaDogadjaja kategorija = new KategorijaDogadjaja { Naziv = tekst, Opis = tekst };

            new UbaciKategorijaDogadjajaSO(kategorija).ExecuteTemplate();

            KategorijaDogadjaja sacuvana = Pretrazi(kategorija.IdKategorijaDogadjaja);
            Assert.AreEqual(tekst, sacuvana.Naziv);
            Assert.AreEqual(tekst, sacuvana.Opis);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            KategorijaDogadjaja kategorija = TestPodaci.KreirajKategoriju();
            kategorija.Naziv = tekst;
            kategorija.Opis = tekst;

            new PromeniKategorijaDogadjajaSO(kategorija).ExecuteTemplate();

            KategorijaDogadjaja sacuvana = Pretrazi(kategorija.IdKategorijaDogadjaja);
            Assert.AreEqual(tekst, sacuvana.Naziv);
            Assert.AreEqual(tekst, sacuvana.Opis);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoNazivu(string tekst)
        {
            TestPodaci.KreirajKategoriju();
            KategorijaDogadjaja kategorija = new KategorijaDogadjaja { Naziv = tekst, Opis = "Opis" };
            new UbaciKategorijaDogadjajaSO(kategorija).ExecuteTemplate();

            List<KategorijaDogadjaja> rezultat = VratiListu(
                new KategorijaDogadjaja { Naziv = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(kategorija.IdKategorijaDogadjaja, rezultat[0].IdKategorijaDogadjaja);
            Assert.AreEqual(tekst, rezultat[0].Naziv);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoOpisu(string tekst)
        {
            TestPodaci.KreirajKategoriju();
            KategorijaDogadjaja kategorija = new KategorijaDogadjaja { Naziv = "Naziv", Opis = tekst };
            new UbaciKategorijaDogadjajaSO(kategorija).ExecuteTemplate();

            List<KategorijaDogadjaja> rezultat = VratiListu(
                new KategorijaDogadjaja { Opis = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(kategorija.IdKategorijaDogadjaja, rezultat[0].IdKategorijaDogadjaja);
            Assert.AreEqual(tekst, rezultat[0].Opis);
        }
    }
}
