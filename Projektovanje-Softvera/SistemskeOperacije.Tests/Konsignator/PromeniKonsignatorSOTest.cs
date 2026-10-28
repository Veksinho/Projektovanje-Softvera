using Common.Domen;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class PromeniKonsignatorSOTest : IntegracioniTest
    {
        private PromeniKonsignatorSO so = null!;
        private FizickoLice fizickoLice = null!;

        [TestInitialize]
        public void SetUp()
        {
            fizickoLice = TestPodaci.KreirajFizickoLice();
        }

        private static Konsignator Pretrazi(int id)
        {
            PretraziKonsignatorSO searchSo = new PretraziKonsignatorSO(new Konsignator { IdKonsignator = id });
            searchSo.ExecuteTemplate();
            return (Konsignator)searchSo.Result!;
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new PromeniKonsignatorSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void TestPredusloviNeispravanId(int id)
        {
            fizickoLice.IdKonsignator = id;
            so = new PromeniKonsignatorSO(fizickoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Nije prosleđen id konsignatora.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviPromenaTipa()
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.IdKonsignator = fizickoLice.IdKonsignator;
            so = new PromeniKonsignatorSO(pravnoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Tip konsignatora se ne može promeniti.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDuplikatJmbg()
        {
            FizickoLice drugo = TestPodaci.KreirajFizickoLice();
            drugo.Jmbg = fizickoLice.Jmbg;
            so = new PromeniKonsignatorSO(drugo);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator sa unetim JMBG-om već postoji.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviIstiJmbgSopstvenogZapisa()
        {
            fizickoLice.Ime = "Izmenjeno";
            so = new PromeniKonsignatorSO(fizickoLice);

            so.ExecuteTemplate();

            Assert.AreEqual("Izmenjeno", ((FizickoLice)Pretrazi(fizickoLice.IdKonsignator)).Ime);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuFizickoLice()
        {
            fizickoLice.Ime = "Petar";
            fizickoLice.Telefon = "0659999999";
            so = new PromeniKonsignatorSO(fizickoLice);

            so.ExecuteTemplate();

            FizickoLice saved = (FizickoLice)Pretrazi(fizickoLice.IdKonsignator);
            Assert.AreEqual("Petar", saved.Ime);
            Assert.AreEqual("0659999999", saved.Telefon);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPravnoLice()
        {
            PravnoLice pravnoLice = TestPodaci.KreirajPravnoLice();
            pravnoLice.NazivFirme = "Izmenjena firma";
            pravnoLice.Adresa = "Nova adresa 5";
            so = new PromeniKonsignatorSO(pravnoLice);

            so.ExecuteTemplate();

            PravnoLice saved = (PravnoLice)Pretrazi(pravnoLice.IdKonsignator);
            Assert.AreEqual("Izmenjena firma", saved.NazivFirme);
            Assert.AreEqual("Nova adresa 5", saved.Adresa);
        }
    }
}
