using System.Net.Mail;

namespace Common.Validacija
{
    public static class Provera
    {
        public static bool SamoCifre(string? tekst, int duzina) =>
            tekst != null && tekst.Length == duzina && tekst.All(JeCifra);

        public static bool JeIspravanTelefon(string? tekst)
        {
            if (string.IsNullOrEmpty(tekst)) return false;

            string cifre = tekst[0] == '+' ? tekst.Substring(1) : tekst;

            return cifre.Length >= Ogranicenja.TelefonMinCifara
                && cifre.Length <= Ogranicenja.TelefonMaxCifara
                && cifre.All(JeCifra);
        }

        public static bool JeIspravanEmail(string? tekst) =>
            !string.IsNullOrEmpty(tekst)
            && MailAddress.TryCreate(tekst, out MailAddress? adresa)
            && adresa.Address == tekst;

        public static bool JeIspravnaSifra(string? tekst) =>
            tekst != null
            && tekst.Length >= Ogranicenja.SifraMin
            && tekst.Length <= Ogranicenja.SifraMax
            && tekst.Any(char.IsLetter)
            && tekst.Any(JeCifra);

        public static bool UDuzini(string? tekst, int max) => (tekst?.Length ?? 0) <= max;

        public static bool ImaNajviseDecimala(decimal vrednost, int brojDecimala) =>
            decimal.Round(vrednost, brojDecimala) == vrednost;

        public static bool JeUOpsegu(decimal vrednost, decimal min, decimal max) =>
            vrednost >= min && vrednost <= max;

        private static bool JeCifra(char c) => c >= '0' && c <= '9';
    }
}
