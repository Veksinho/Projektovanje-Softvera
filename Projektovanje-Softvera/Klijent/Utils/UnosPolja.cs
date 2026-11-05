using Common.Validacija;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Klijent.Utils
{
    internal static class UnosPolja
    {
        private static readonly int DecimalniBrojMaxLength =
            ((long)Ogranicenja.MaxIznos).ToString(CultureInfo.InvariantCulture).Length
            + 1 + Ogranicenja.BrojDecimala;

        public static void SamoCifre(TextBox txt, int duzina)
        {
            txt.MaxLength = duzina;
            txt.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !JeCifra(e.KeyChar))
                    e.Handled = true;
            };
            txt.TextChanged += (s, e) => Ocisti(txt, OcistiCifre);
        }

        public static void Telefon(TextBox txt)
        {
            txt.MaxLength = Ogranicenja.TelefonMaxLength;
            txt.KeyPress += (s, e) =>
            {
                char c = e.KeyChar;
                if (char.IsControl(c))
                    return;

                bool kursorNaPocetku = txt.SelectionStart == 0;
                bool plusOstaje = txt.Text.StartsWith('+') && !(kursorNaPocetku && txt.SelectionLength > 0);

                if (c == '+')
                    e.Handled = !kursorNaPocetku || plusOstaje;
                else if (JeCifra(c))
                    e.Handled = kursorNaPocetku && plusOstaje;
                else
                    e.Handled = true;
            };
            txt.TextChanged += (s, e) => Ocisti(txt, OcistiTelefon);
        }

        public static void DecimalniBroj(TextBox txt)
        {
            txt.MaxLength = DecimalniBrojMaxLength;
            txt.KeyPress += (s, e) =>
            {
                char c = e.KeyChar;
                if (char.IsControl(c))
                    return;

                string separator = DecimalniSeparator;
                string bezSelekcije = txt.Text.Remove(txt.SelectionStart, txt.SelectionLength);
                int pozicijaSeparatora = bezSelekcije.IndexOf(separator, StringComparison.Ordinal);

                if (JeSeparator(c))
                {
                    if (pozicijaSeparatora >= 0 || separator.Length != 1)
                    {
                        e.Handled = true;
                        return;
                    }
                    e.KeyChar = separator[0];
                    return;
                }

                if (!JeCifra(c))
                {
                    e.Handled = true;
                    return;
                }

                bool posleSeparatora = pozicijaSeparatora >= 0 && txt.SelectionStart > pozicijaSeparatora;
                int brojDecimala = pozicijaSeparatora < 0
                    ? 0
                    : bezSelekcije.Length - pozicijaSeparatora - separator.Length;
                if (posleSeparatora && brojDecimala >= Ogranicenja.BrojDecimala)
                    e.Handled = true;
            };
            txt.TextChanged += (s, e) => Ocisti(txt, OcistiDecimalniBroj);
        }

        public static bool TryParseIznos(string tekst, out decimal iznos)
        {
            return decimal.TryParse(tekst,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
                CultureInfo.CurrentCulture, out iznos);
        }

        private static string DecimalniSeparator =>
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        // Cifra je samo 0-9, ne char.IsDigit (PRAVILA-VALIDACIJE.md).
        private static bool JeCifra(char c) => c >= '0' && c <= '9';

        private static bool JeSeparator(char c) =>
            c == '.' || c == ',' || DecimalniSeparator.IndexOf(c) >= 0;

        private static void Ocisti(TextBox txt, Func<string, string> ocisti)
        {
            string tekst = txt.Text;
            string ocisceno = ocisti(tekst);
            if (ocisceno == tekst)
                return;

            int kursor = Math.Min(txt.SelectionStart, tekst.Length);
            int noviKursor = ocisti(tekst.Substring(0, kursor)).Length;

            txt.Text = ocisceno;
            txt.SelectionStart = Math.Min(noviKursor, ocisceno.Length);
            txt.SelectionLength = 0;
        }

        private static string OcistiCifre(string tekst)
        {
            StringBuilder sb = new StringBuilder(tekst.Length);
            foreach (char c in tekst)
                if (JeCifra(c))
                    sb.Append(c);
            return sb.ToString();
        }

        private static string OcistiTelefon(string tekst)
        {
            StringBuilder sb = new StringBuilder(tekst.Length);
            foreach (char c in tekst)
                if (JeCifra(c) || (c == '+' && sb.Length == 0))
                    sb.Append(c);
            return sb.ToString();
        }

        private static string OcistiDecimalniBroj(string tekst)
        {
            string separator = DecimalniSeparator;
            StringBuilder sb = new StringBuilder(tekst.Length);
            bool imaSeparator = false;
            int brojDecimala = 0;

            for (int i = 0; i < tekst.Length; i++)
            {
                if (string.CompareOrdinal(tekst, i, separator, 0, separator.Length) == 0)
                {
                    if (!imaSeparator)
                    {
                        sb.Append(separator);
                        imaSeparator = true;
                    }
                    i += separator.Length - 1;
                    continue;
                }

                char c = tekst[i];
                if (JeCifra(c))
                {
                    if (!imaSeparator)
                        sb.Append(c);
                    else if (brojDecimala < Ogranicenja.BrojDecimala)
                    {
                        sb.Append(c);
                        brojDecimala++;
                    }
                }
                else if (c == '.' || c == ',')
                {
                    if (!imaSeparator)
                    {
                        sb.Append(separator);
                        imaSeparator = true;
                    }
                }
            }
            return sb.ToString();
        }
    }
}
