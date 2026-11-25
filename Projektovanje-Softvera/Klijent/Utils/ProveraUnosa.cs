using Common.Validacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Klijent.Utils
{
    internal sealed class ProveraUnosa
    {
        private readonly ErrorProvider errorProvider;
        private readonly HashSet<Control> pracene = new HashSet<Control>();

        public ProveraUnosa(ContainerControl uc)
        {
            errorProvider = new ErrorProvider
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink,
                ContainerControl = uc
            };
            uc.Disposed += (s, e) => errorProvider.Dispose();
        }

        public bool Prikazi(List<GreskaValidacije> greske, IReadOnlyDictionary<string, Control> polja)
        {
            Ocisti();

            Control? prva = null;

            foreach (IGrouping<string, GreskaValidacije> grupa in greske.GroupBy(g => g.Polje))
            {
                if (!polja.TryGetValue(grupa.Key, out Control? kontrola))
                    throw new InvalidOperationException($"Polje '{grupa.Key}' nema kontrolu na formi.");

                errorProvider.SetError(kontrola,
                    string.Join(Environment.NewLine, grupa.Select(g => g.Poruka)));
                PratiPromenu(kontrola);

                prva ??= kontrola;
            }

            prva?.Focus();

            return greske.Count == 0;
        }

        public void Ocisti()
        {
            errorProvider.Clear();
        }

        public static List<GreskaValidacije> Spoji(List<GreskaValidacije> klijentske,
            List<GreskaValidacije> pravila)
        {
            var polja = new HashSet<string>(klijentske.Select(g => g.Polje));
            return klijentske.Concat(pravila.Where(g => !polja.Contains(g.Polje))).ToList();
        }

        private void PratiPromenu(Control kontrola)
        {
            if (!pracene.Add(kontrola)) return;

            EventHandler obrisi = (s, e) => errorProvider.SetError(kontrola, "");

            switch (kontrola)
            {
                case TextBoxBase txt:
                    txt.TextChanged += obrisi;
                    break;
                case ComboBox cmb:
                    cmb.SelectedIndexChanged += obrisi;
                    break;
                case DateTimePicker dtp:
                    dtp.ValueChanged += obrisi;
                    break;
                case NumericUpDown num:
                    num.ValueChanged += obrisi;
                    break;
                case DataGridView dgv:
                    dgv.RowsAdded += (s, e) => obrisi(s, e);
                    dgv.RowsRemoved += (s, e) => obrisi(s, e);
                    break;
                default:
                    kontrola.TextChanged += obrisi;
                    break;
            }
        }
    }
}
