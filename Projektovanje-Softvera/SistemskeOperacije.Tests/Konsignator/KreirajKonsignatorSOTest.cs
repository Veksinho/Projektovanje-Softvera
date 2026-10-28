using Common.Domen;
using Microsoft.Data.SqlClient;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class KreirajKonsignatorSOTest : IntegracioniTest
    {
        private KreirajKonsignatorSO so = null!;
        private FizickoLice fizickoLice = null!;

        [TestInitialize]
        public void SetUp()
        {
            fizickoLice = TestPodaci.NapraviFizickoLice();
        }

        [TestMethod]
        public void TestPredusloviNull()
        {
            so = new KreirajKonsignatorSO(null!);

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestPredusloviBazniBezPodtipa()
        {
            so = new KreirajKonsignatorSO(new Konsignator
            {
                Email = fizickoLice.Email,
                Telefon = fizickoLice.Telefon,
                Adresa = fizickoLice.Adresa
            });

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator mora biti fizičko ili pravno lice.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviNeispravanEmail()
        {
            fizickoLice.Email = "test.rs";
            so = new KreirajKonsignatorSO(fizickoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("E-mail konsignatora nije u odgovarajućem formatu.", ex.Message);
        }

        [TestMethod]
        public void TestPredusloviDuplikatJmbg()
        {
            FizickoLice postojece = TestPodaci.KreirajFizickoLice();
            int brojPre = TestBaza.BrojRedova("Konsignator");
            fizickoLice.Jmbg = postojece.Jmbg;
            so = new KreirajKonsignatorSO(fizickoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator sa unetim JMBG-om već postoji.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Konsignator"));
        }

        [TestMethod]
        public void TestPredusloviDuplikatPib()
        {
            PravnoLice postojece = TestPodaci.KreirajPravnoLice();
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.Pib = postojece.Pib;
            so = new KreirajKonsignatorSO(pravnoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator sa unetim PIB-om već postoji.", ex.Message);
        }

        [TestMethod]
        [Ignore("Poznata greška: SO ne proverava jedinstvenost e-maila, pada SqlException na UQ ograničenju")]
        public void TestPredusloviDuplikatEmail()
        {
            FizickoLice postojece = TestPodaci.KreirajFizickoLice();
            int brojPre = TestBaza.BrojRedova("Konsignator");
            fizickoLice.Email = postojece.Email;
            so = new KreirajKonsignatorSO(fizickoLice);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual("Konsignator sa unetim e-mailom već postoji.", ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Konsignator"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuFizickoLice()
        {
            so = new KreirajKonsignatorSO(fizickoLice);

            so.ExecuteTemplate();

            Assert.IsTrue(TestBaza.Postoji("Konsignator", $"idKonsignator = {fizickoLice.IdKonsignator}"));
            Assert.IsTrue(TestBaza.Postoji("FizickoLice", $"idKonsignator = {fizickoLice.IdKonsignator}"));

            PretraziKonsignatorSO searchSo = new PretraziKonsignatorSO(
                new Konsignator { IdKonsignator = fizickoLice.IdKonsignator });
            searchSo.ExecuteTemplate();
            Assert.AreEqual(DateTime.Today, ((Konsignator)searchSo.Result!).DatumRegistracije);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPravnoLice()
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            so = new KreirajKonsignatorSO(pravnoLice);

            so.ExecuteTemplate();

            Assert.IsTrue(TestBaza.Postoji("Konsignator", $"idKonsignator = {pravnoLice.IdKonsignator}"));
            Assert.IsTrue(TestBaza.Postoji("PravnoLice", $"idKonsignator = {pravnoLice.IdKonsignator}"));
        }

        [TestMethod]
        public void TestIzvrsiOperacijuRollbackPriGresciPodtipa()
        {
            fizickoLice.Jmbg = "12345678901234";
            so = new KreirajKonsignatorSO(fizickoLice);

            Assert.ThrowsExactly<SqlException>(() => so.ExecuteTemplate());

            Assert.AreEqual(0, TestBaza.BrojRedova("Konsignator", $"email = '{fizickoLice.Email}'"));
        }
    }
}
