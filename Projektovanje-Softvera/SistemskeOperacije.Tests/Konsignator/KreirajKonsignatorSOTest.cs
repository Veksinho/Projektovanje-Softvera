using Common.Domen;
using Common.Helpers;
using Common.Validacija;
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

        // Validate() odbija konsignatora sa datom porukom, a broj redova u tabeli Konsignator se ne menja.
        private void ProveriOdbijanje(Konsignator konsignator, string poruka)
        {
            int brojPre = TestBaza.BrojRedova("Konsignator");
            so = new KreirajKonsignatorSO(konsignator);

            Exception ex = Assert.Throws<Exception>(() => so.ExecuteTemplate());

            Assert.AreEqual(poruka, ex.Message);
            Assert.AreEqual(brojPre, TestBaza.BrojRedova("Konsignator"));
        }

        [TestMethod]
        [DataRow("12345678901a3")]
        [DataRow("123456789012")]
        [DataRow("12345678901234")]
        public void TestPredusloviNeispravanJmbg(string jmbg)
        {
            fizickoLice.Jmbg = jmbg;

            ProveriOdbijanje(fizickoLice, "JMBG fizičkog lica mora imati tačno 13 cifara.");
        }

        [TestMethod]
        [DataRow("12345678")]
        public void TestPredusloviNeispravanBrojLicneKarte(string brojLicneKarte)
        {
            fizickoLice.BrojLicneKarte = brojLicneKarte;

            ProveriOdbijanje(fizickoLice, "Broj lične karte fizičkog lica mora imati tačno 9 cifara.");
        }

        [TestMethod]
        [DataRow("12345678")]
        public void TestPredusloviNeispravanPib(string pib)
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.Pib = pib;

            ProveriOdbijanje(pravnoLice, "PIB pravnog lica mora imati tačno 9 cifara.");
        }

        [TestMethod]
        [DataRow("1234567")]
        public void TestPredusloviNeispravanMaticniBroj(string maticniBroj)
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.MaticniBroj = maticniBroj;

            ProveriOdbijanje(pravnoLice, "Matični broj pravnog lica mora imati tačno 8 cifara.");
        }

        [TestMethod]
        [DataRow("060 1234567")]
        [DataRow("060123456a")]
        [DataRow("060+1234567")]
        public void TestPredusloviNeispravanTelefon(string telefon)
        {
            fizickoLice.Telefon = telefon;

            ProveriOdbijanje(fizickoLice, "Telefon konsignatora nije u odgovarajućem formatu.");
        }

        [TestMethod]
        [DataRow("test@")]
        public void TestPredusloviEmailBezDomena(string email)
        {
            fizickoLice.Email = email;

            ProveriOdbijanje(fizickoLice, "E-mail konsignatora nije u odgovarajućem formatu.");
        }

        [TestMethod]
        [DataRow(Ogranicenja.KonsignatorAdresa + 1)]
        public void TestPredusloviPredugackaAdresa(int duzina)
        {
            fizickoLice.Adresa = new string('a', duzina);

            ProveriOdbijanje(fizickoLice, "Adresa konsignatora može imati najviše 200 znakova.");
        }

        [TestMethod]
        [DataRow(Ogranicenja.FizickoLiceIme + 1)]
        public void TestPredusloviPredugackoImeFizickogLica(int duzina)
        {
            fizickoLice.Ime = new string('a', duzina);

            ProveriOdbijanje(fizickoLice, "Ime fizičkog lica može imati najviše 50 znakova.");
        }

        [TestMethod]
        [DataRow(Ogranicenja.PravnoLiceNaziv + 1)]
        public void TestPredusloviPredugackiNazivPravnogLica(int duzina)
        {
            PravnoLice pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.NazivFirme = new string('a', duzina);

            ProveriOdbijanje(pravnoLice, "Naziv pravnog lica može imati najviše 150 znakova.");
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
            const string obrisiOgranicenje =
                "IF OBJECT_ID(N'CK_Test_Rollback', N'C') IS NOT NULL " +
                "ALTER TABLE FizickoLice DROP CONSTRAINT CK_Test_Rollback";

            TestBaza.Execute(obrisiOgranicenje);
            TestBaza.Execute("ALTER TABLE FizickoLice ADD CONSTRAINT CK_Test_Rollback CHECK (ime <> N'ROLLBACK_TEST')");
            try
            {
                fizickoLice.Ime = "ROLLBACK_TEST";
                so = new KreirajKonsignatorSO(fizickoLice);

                Assert.ThrowsExactly<SqlException>(() => so.ExecuteTemplate());

                Assert.AreEqual(0, TestBaza.BrojRedova("Konsignator", $"email = N'{Sql.Tekst(fizickoLice.Email)}'"));
            }
            finally
            {
                TestBaza.Execute(obrisiOgranicenje);
            }
        }
    }
}
