using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class ObrisiKonsignatorSOTest : IntegracioniTest
    {
        private ObrisiKonsignatorSO so = null!;
        private FizickoLice fizickoLice = null!;

        [TestInitialize]
        public void SetUp()
        {
            fizickoLice = TestPodaci.KreirajFizickoLice();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new ObrisiKonsignatorSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBazniBezPodtipa()
        {
            so = new ObrisiKonsignatorSO(new Konsignator { IdKonsignator = fizickoLice.IdKonsignator });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator mora biti fizičko ili pravno lice.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviKonsignatorSaKartama()
        {
            TestPodaci.KreirajKartu(TestPodaci.KreirajDogadjaj(), fizickoLice);
            so = new ObrisiKonsignatorSO(fizickoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator ima unete karte i ne može se obrisati.", ex.Message);
            Assert.IsTrue(TestBaza.Postoji("Konsignator", $"idKonsignator = {fizickoLice.IdKonsignator}"));
            Assert.IsTrue(TestBaza.Postoji("FizickoLice", $"idKonsignator = {fizickoLice.IdKonsignator}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuFizickoLice()
        {
            so = new ObrisiKonsignatorSO(fizickoLice);

            so.ExecuteTemplate();

            Assert.IsFalse(TestBaza.Postoji("FizickoLice", $"idKonsignator = {fizickoLice.IdKonsignator}"));
            Assert.IsFalse(TestBaza.Postoji("Konsignator", $"idKonsignator = {fizickoLice.IdKonsignator}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPravnoLice()
        {
            PravnoLice pravnoLice = TestPodaci.KreirajPravnoLice();
            so = new ObrisiKonsignatorSO(pravnoLice);

            so.ExecuteTemplate();

            Assert.IsFalse(TestBaza.Postoji("PravnoLice", $"idKonsignator = {pravnoLice.IdKonsignator}"));
            Assert.IsFalse(TestBaza.Postoji("Konsignator", $"idKonsignator = {pravnoLice.IdKonsignator}"));
        }
    }
}
