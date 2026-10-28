using Common.Domen;
using Common.Domen.Enumeracije;
using SistemskeOperacije.KonsignatorSO;

namespace SistemskeOperacije.Tests
{
    [TestClass]
    public class VratiListuKonsignatorSOTest : IntegracioniTest
    {
        private VratiListuKonsignatorSO so = null!;
        private FizickoLice fizickoLice = null!;
        private PravnoLice pravnoLice = null!;

        [TestInitialize]
        public void SetUp()
        {
            fizickoLice = TestPodaci.NapraviFizickoLice();
            fizickoLice.Ime = "Petar";
            fizickoLice.Prezime = "Jovanović";
            new KreirajKonsignatorSO(fizickoLice).ExecuteTemplate();

            pravnoLice = TestPodaci.NapraviPravnoLice();
            pravnoLice.NazivFirme = "Jovanović DOO";
            new KreirajKonsignatorSO(pravnoLice).ExecuteTemplate();
        }

        private List<Konsignator> Izvrsi(Konsignator kriterijum)
        {
            so = new VratiListuKonsignatorSO(kriterijum);
            so.ExecuteTemplate();
            return (List<Konsignator>)so.Result!;
        }

        [TestMethod]
        public void TestPredusloviPrazanKriterijum()
        {
            so = new VratiListuKonsignatorSO(new Konsignator());

            Assert.ThrowsExactly<ArgumentException>(() => so.ExecuteTemplate());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoTipu()
        {
            List<Konsignator> result = Izvrsi(new Konsignator { TipKriterijum = TipKonsignatora.fizicko_lice });

            Assert.AreEqual(1, result.Count);
            Assert.IsInstanceOfType<FizickoLice>(result[0]);
            Assert.AreEqual(fizickoLice.IdKonsignator, result[0].IdKonsignator);
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoNazivu()
        {
            List<Konsignator> result = Izvrsi(new Konsignator { NazivKriterijum = "Jovanović" });

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result.OfType<FizickoLice>().Count());
            Assert.AreEqual(1, result.OfType<PravnoLice>().Count());
        }

        [TestMethod]
        public void TestIzvrsiOperacijuPoPoljuPodtipa()
        {
            List<Konsignator> result = Izvrsi(new FizickoLice { Ime = "Pet" });

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(fizickoLice.IdKonsignator, result[0].IdKonsignator);
        }
    }
}
