using Common.Domen;
using SistemskeOperacije.DogadjajSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuDogadjajSOTest : IntegracioniTest
    {
        private VratiListuDogadjajSO so = null!;

        [TestInitialize]
        public void SetUp()
        {
            Ubaci("Exit festival", "Novi Sad", DateTime.Today.AddDays(30));
            Ubaci("Exit konferencija", "Beograd", DateTime.Today.AddDays(40));
            Ubaci("Derbi", "Beograd", DateTime.Today.AddDays(30));
        }

        private static void Ubaci(string naziv, string mesto, DateTime datum)
        {
            new UbaciDogadjajSO(new Dogadjaj { Naziv = naziv, Mesto = mesto, DatumOdrzavanja = datum }).ExecuteTemplate();
        }

        private List<string> Izvrsi(Dogadjaj kriterijum)
        {
            so = new VratiListuDogadjajSO(kriterijum);
            so.ExecuteTemplate();
            return ((List<Dogadjaj>)so.Result!).Select(d => d.Naziv).ToList();
        }

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuDogadjajSO(new Dogadjaj());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoNazivu()
        {
            List<string> result = Izvrsi(new Dogadjaj { Naziv = "Exit" });

            CollectionAssert.AreEquivalent(new[] { "Exit festival", "Exit konferencija" }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoMestu()
        {
            List<string> result = Izvrsi(new Dogadjaj { Mesto = "Beograd" });

            CollectionAssert.AreEquivalent(new[] { "Exit konferencija", "Derbi" }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoDatumu()
        {
            List<string> result = Izvrsi(new Dogadjaj { DatumOdrzavanja = DateTime.Today.AddDays(30) });

            CollectionAssert.AreEquivalent(new[] { "Exit festival", "Derbi" }, result);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuKombinovaniKriterijumi()
        {
            List<string> result = Izvrsi(new Dogadjaj { Naziv = "Exit", Mesto = "Beograd" });

            CollectionAssert.AreEquivalent(new[] { "Exit konferencija" }, result);
        }
    }
}
