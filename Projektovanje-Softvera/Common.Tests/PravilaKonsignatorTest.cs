using Common.Domen;
using Common.Validacija;
using static Common.Tests.PomocPravila;

namespace Common.Tests
{
    [TestClass]
    public class PravilaKonsignatorTest
    {
        private static FizickoLice IspravnoFizicko() => new FizickoLice
        {
            Email = "o'brien@test.rs",
            Telefon = "0641234567",
            Adresa = "Knez Mihailova 1",
            Jmbg = "0101990710001",
            Ime = "Marko",
            Prezime = "Marković",
            BrojLicneKarte = "123456789"
        };

        private static PravnoLice IspravnoPravno() => new PravnoLice
        {
            Email = "firma@test.rs",
            Telefon = "+38111123456",
            Adresa = "Bulevar 10",
            NazivFirme = "Firma d.o.o.",
            Pib = "123456789",
            MaticniBroj = "12345678"
        };

        [TestMethod]
        public void TestIspravnoFizickoLiceNemaGresaka()
        {
            NemaGresaka(PravilaKonsignator.Proveri(IspravnoFizicko()));
        }

        [TestMethod]
        public void TestIspravnoPravnoLiceNemaGresaka()
        {
            NemaGresaka(PravilaKonsignator.Proveri(IspravnoPravno()));
        }

        [TestMethod]
        public void TestKonsignatorMoraBitiFizickoIliPravnoLice()
        {
            Konsignator k = new Konsignator { Email = "a@b.rs", Telefon = "0641234567", Adresa = "Adresa" };

            ArgumentException ex = Assert.ThrowsExactly<ArgumentException>(() => PravilaKonsignator.Proveri(k));
            Assert.AreEqual("k", ex.ParamName);
            StringAssert.StartsWith(ex.Message, "Konsignator mora biti fizičko ili pravno lice.");
        }

        // Zajednička polja

        [TestMethod]
        public void TestEmailObavezan()
        {
            FizickoLice k = IspravnoFizicko();
            k.Email = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(Konsignator.Email), "E-mail konsignatora je obavezna.");
        }

        [TestMethod]
        public void TestEmailDuzina()
        {
            FizickoLice k = IspravnoFizicko();
            k.Email = new string('a', 92) + "@test.rs";
            NemaGresaka(PravilaKonsignator.Proveri(k));

            k.Email = new string('a', 93) + "@test.rs";
            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(Konsignator.Email), "E-mail konsignatora može imati najviše 100 znakova.");
        }

        [TestMethod]
        [DataRow("pera")]
        [DataRow("pera@")]
        [DataRow("Pera <pera@test.rs>")]
        public void TestEmailFormat(string email)
        {
            FizickoLice k = IspravnoFizicko();
            k.Email = email;

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(Konsignator.Email), "E-mail konsignatora nije u odgovarajućem formatu.");
        }

        [TestMethod]
        public void TestTelefonObavezan()
        {
            PravnoLice k = IspravnoPravno();
            k.Telefon = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(Konsignator.Telefon), "Telefon konsignatora je obavezan.");
        }

        [TestMethod]
        public void TestTelefonFormat()
        {
            PravnoLice k = IspravnoPravno();
            k.Telefon = "064-1234567";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(Konsignator.Telefon),
                "Telefon konsignatora nije u odgovarajućem formatu.");
        }

        [TestMethod]
        public void TestAdresaObavezna()
        {
            PravnoLice k = IspravnoPravno();
            k.Adresa = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(Konsignator.Adresa), "Adresa konsignatora je obavezna.");
        }

        [TestMethod]
        public void TestAdresaDuzina()
        {
            PravnoLice k = IspravnoPravno();
            k.Adresa = new string('a', 200);
            NemaGresaka(PravilaKonsignator.Proveri(k));

            k.Adresa = new string('a', 201);
            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(Konsignator.Adresa), "Adresa konsignatora može imati najviše 200 znakova.");
        }

        // Fizičko lice

        [TestMethod]
        public void TestJmbgObavezan()
        {
            FizickoLice k = IspravnoFizicko();
            k.Jmbg = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(FizickoLice.Jmbg), "JMBG fizičkog lica je obavezan.");
        }

        [TestMethod]
        [DataRow("010199071000")]
        [DataRow("01019907100012")]
        [DataRow("010199071000a")]
        public void TestJmbgFormat(string jmbg)
        {
            FizickoLice k = IspravnoFizicko();
            k.Jmbg = jmbg;

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(FizickoLice.Jmbg), "JMBG fizičkog lica mora imati tačno 13 cifara.");
        }

        [TestMethod]
        public void TestImeObavezno()
        {
            FizickoLice k = IspravnoFizicko();
            k.Ime = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(FizickoLice.Ime), "Ime fizičkog lica je obavezno.");
        }

        [TestMethod]
        public void TestImeDuzina()
        {
            FizickoLice k = IspravnoFizicko();
            k.Ime = new string('a', 51);

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(FizickoLice.Ime), "Ime fizičkog lica može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestPrezimeObavezno()
        {
            FizickoLice k = IspravnoFizicko();
            k.Prezime = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(FizickoLice.Prezime), "Prezime fizičkog lica je obavezno.");
        }

        [TestMethod]
        public void TestPrezimeDuzina()
        {
            FizickoLice k = IspravnoFizicko();
            k.Prezime = new string('a', 51);

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(FizickoLice.Prezime), "Prezime fizičkog lica može imati najviše 50 znakova.");
        }

        [TestMethod]
        public void TestBrojLicneKarteObavezan()
        {
            FizickoLice k = IspravnoFizicko();
            k.BrojLicneKarte = "";

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(FizickoLice.BrojLicneKarte), "Broj lične karte fizičkog lica je obavezan.");
        }

        [TestMethod]
        [DataRow("12345678")]
        [DataRow("1234567890")]
        public void TestBrojLicneKarteFormat(string broj)
        {
            FizickoLice k = IspravnoFizicko();
            k.BrojLicneKarte = broj;

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(FizickoLice.BrojLicneKarte), "Broj lične karte fizičkog lica mora imati tačno 9 cifara.");
        }

        // Pravno lice

        [TestMethod]
        public void TestNazivFirmeObavezan()
        {
            PravnoLice k = IspravnoPravno();
            k.NazivFirme = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(PravnoLice.NazivFirme), "Naziv pravnog lica je obavezan.");
        }

        [TestMethod]
        public void TestNazivFirmeDuzina()
        {
            PravnoLice k = IspravnoPravno();
            k.NazivFirme = new string('a', 150);
            NemaGresaka(PravilaKonsignator.Proveri(k));

            k.NazivFirme = new string('a', 151);
            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(PravnoLice.NazivFirme), "Naziv pravnog lica može imati najviše 150 znakova.");
        }

        [TestMethod]
        public void TestPibObavezan()
        {
            PravnoLice k = IspravnoPravno();
            k.Pib = "";

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(PravnoLice.Pib), "PIB pravnog lica je obavezan.");
        }

        [TestMethod]
        [DataRow("12345678")]
        [DataRow("12345678a")]
        public void TestPibFormat(string pib)
        {
            PravnoLice k = IspravnoPravno();
            k.Pib = pib;

            JednaGreska(PravilaKonsignator.Proveri(k), nameof(PravnoLice.Pib), "PIB pravnog lica mora imati tačno 9 cifara.");
        }

        [TestMethod]
        public void TestMaticniBrojObavezan()
        {
            PravnoLice k = IspravnoPravno();
            k.MaticniBroj = "";

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(PravnoLice.MaticniBroj), "Matični broj pravnog lica je obavezan.");
        }

        [TestMethod]
        [DataRow("1234567")]
        [DataRow("123456789")]
        public void TestMaticniBrojFormat(string mb)
        {
            PravnoLice k = IspravnoPravno();
            k.MaticniBroj = mb;

            JednaGreska(PravilaKonsignator.Proveri(k),
                nameof(PravnoLice.MaticniBroj), "Matični broj pravnog lica mora imati tačno 8 cifara.");
        }
    }
}
