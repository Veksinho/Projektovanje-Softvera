using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PretraziKonsignatorSOTest : IntegracioniTest
    {
        private PretraziKonsignatorSO so = null!;

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PretraziKonsignatorSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuVracaFizickoLice()
        {
            FizickoLice fizickoLice = TestPodaci.KreirajFizickoLice();
            so = new PretraziKonsignatorSO(new Konsignator { IdKonsignator = fizickoLice.IdKonsignator });

            so.ExecuteTemplate();

            Assert.IsInstanceOfType<FizickoLice>(so.Result);
            FizickoLice found = (FizickoLice)so.Result!;
            Assert.AreEqual(fizickoLice.Jmbg, found.Jmbg);
            Assert.AreEqual(fizickoLice.Ime, found.Ime);
            Assert.AreEqual(fizickoLice.Prezime, found.Prezime);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuVracaPravnoLice()
        {
            PravnoLice pravnoLice = TestPodaci.KreirajPravnoLice();
            so = new PretraziKonsignatorSO(new Konsignator { IdKonsignator = pravnoLice.IdKonsignator });

            so.ExecuteTemplate();

            Assert.IsInstanceOfType<PravnoLice>(so.Result);
            PravnoLice found = (PravnoLice)so.Result!;
            Assert.AreEqual(pravnoLice.Pib, found.Pib);
            Assert.AreEqual(pravnoLice.NazivFirme, found.NazivFirme);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuNepostojeciKonsignator()
        {
            so = new PretraziKonsignatorSO(new Konsignator { IdKonsignator = 999999 });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Sistem ne može da nađe konsignatora.", ex.Message);
        }
    }
}
