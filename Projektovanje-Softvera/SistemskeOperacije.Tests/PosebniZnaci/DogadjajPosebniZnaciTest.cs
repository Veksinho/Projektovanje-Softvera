using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class DogadjajPosebniZnaciTest : IntegracioniTest
    {
        private static Dogadjaj Pretrazi(int id)
        {
            PretraziDogadjajSO so = new PretraziDogadjajSO(new Dogadjaj { IdDogadjaj = id });
            so.ExecuteTemplate();
            return (Dogadjaj)so.Result!;
        }

        private static List<Dogadjaj> VratiListu(Dogadjaj kriterijum)
        {
            VratiListuDogadjajSO so = new VratiListuDogadjajSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Dogadjaj>)so.Result!;
        }

        private static Dogadjaj Ubaci(string naziv, string mesto)
        {
            Dogadjaj dogadjaj = TestPodaci.NapraviDogadjaj();
            dogadjaj.Naziv = naziv;
            dogadjaj.Mesto = mesto;
            new UbaciDogadjajSO(dogadjaj).ExecuteTemplate();
            return dogadjaj;
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuUpis(string tekst)
        {
            Dogadjaj dogadjaj = Ubaci(tekst, tekst);

            Dogadjaj sacuvan = Pretrazi(dogadjaj.IdDogadjaj);
            Assert.AreEqual(tekst, sacuvan.Naziv);
            Assert.AreEqual(tekst, sacuvan.Mesto);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuIzmena(string tekst)
        {
            Dogadjaj dogadjaj = TestPodaci.KreirajDogadjaj();
            dogadjaj.Naziv = tekst;
            dogadjaj.Mesto = tekst;

            new PromeniDogadjajSO(dogadjaj).ExecuteTemplate();

            Dogadjaj sacuvan = Pretrazi(dogadjaj.IdDogadjaj);
            Assert.AreEqual(tekst, sacuvan.Naziv);
            Assert.AreEqual(tekst, sacuvan.Mesto);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoNazivu(string tekst)
        {
            TestPodaci.KreirajDogadjaj();
            Dogadjaj dogadjaj = Ubaci(tekst, "Beograd");

            List<Dogadjaj> rezultat = VratiListu(new Dogadjaj { Naziv = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(dogadjaj.IdDogadjaj, rezultat[0].IdDogadjaj);
            Assert.AreEqual(tekst, rezultat[0].Naziv);
        }

        [TestMethod]
        [DataRow(TestPodaci.Apostrof)]
        [DataRow(TestPodaci.SrpskaSlova)]
        [DataRow(TestPodaci.Kombinovano)]
        [DataRow(TestPodaci.InjekcijaUslov)]
        [DataRow(TestPodaci.InjekcijaNaredba)]
        public void TestIzvrsiOperacijuPretragaPoMestu(string tekst)
        {
            TestPodaci.KreirajDogadjaj();
            Dogadjaj dogadjaj = Ubaci("Koncert", tekst);

            List<Dogadjaj> rezultat = VratiListu(new Dogadjaj { Mesto = TestPodaci.DeoTeksta(tekst) });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(dogadjaj.IdDogadjaj, rezultat[0].IdDogadjaj);
            Assert.AreEqual(tekst, rezultat[0].Mesto);
        }

        [TestMethod]
        [Ignore("Ograničenje do faze 6.1: džoker znaci u LIKE")]
        public void TestIzvrsiOperacijuPretragaDzokerZnak()
        {
            Dogadjaj saProcentom = Ubaci("Popust 100% rasprodato", "Beograd");
            Ubaci("Koncert bez popusta", "Beograd");

            List<Dogadjaj> rezultat = VratiListu(new Dogadjaj { Naziv = "%" });

            Assert.AreEqual(1, rezultat.Count);
            Assert.AreEqual(saProcentom.IdDogadjaj, rezultat[0].IdDogadjaj);
            Assert.IsTrue(rezultat.All(d => d.Naziv.Contains('%')));
        }
    }
}
