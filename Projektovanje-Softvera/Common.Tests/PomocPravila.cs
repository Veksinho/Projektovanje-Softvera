using Common.Validacija;

namespace Common.Tests
{
    internal static class PomocPravila
    {
        public static void NemaGresaka(List<GreskaValidacije> greske)
        {
            Assert.AreEqual(0, greske.Count, string.Join(" | ", greske));
        }

        public static void JednaGreska(List<GreskaValidacije> greske, string polje, string poruka)
        {
            Assert.AreEqual(1, greske.Count, string.Join(" | ", greske));
            Assert.AreEqual(new GreskaValidacije(polje, poruka), greske[0]);
        }
    }
}
