using Common.Domen;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klijent.UserControls
{
    public partial class UCBroker : UserControl
    {
        private readonly Broker b;

        public UCBroker(FormMode mode, Broker broker, List<KategorijaDogadjaja> sveKategorije)
        {
            InitializeComponent();

            this.b = broker;

            txtId.Text = mode == FormMode.Add ? "" : b.IdBroker.ToString();
            txtKorisnickoIme.Text = b.KorisnickoIme;
            txtSifra.Text = b.Sifra;
            txtIme.Text = b.Ime;
            txtPrezime.Text = b.Prezime;
            txtTelefon.Text = b.Telefon;

            PopuniKategorije(sveKategorije);
            SrediFormu(mode);
        }

        private void PopuniKategorije(List<KategorijaDogadjaja> sveKategorije)
        {
            clbKategorije.Items.Clear();

            foreach (KategorijaDogadjaja k in sveKategorije)
            {
                int index = clbKategorije.Items.Add(k);

                bool selected = b.Specijalizacije.Any(s =>
                    s.KategorijaDogadjaja != null
                    && s.KategorijaDogadjaja.IdKategorijaDogadjaja == k.IdKategorijaDogadjaja);

                clbKategorije.SetItemChecked(index, selected);
            }
        }

        public Broker VratiObjekat()
        {
            b.KorisnickoIme = txtKorisnickoIme.Text.Trim();
            b.Sifra = txtSifra.Text.Trim();
            b.Ime = txtIme.Text.Trim();
            b.Prezime = txtPrezime.Text.Trim();
            b.Telefon = txtTelefon.Text.Trim();

            var nove = new List<BrKd>();

            foreach (KategorijaDogadjaja k in clbKategorije.CheckedItems)
            {
                BrKd? stara = b.Specijalizacije.FirstOrDefault(s =>
                    s.KategorijaDogadjaja != null
                    && s.KategorijaDogadjaja.IdKategorijaDogadjaja == k.IdKategorijaDogadjaja);

                nove.Add(new BrKd
                {
                    Broker = b,
                    KategorijaDogadjaja = k,
                    DatumSpecijalizacije = stara != null
                        ? stara.DatumSpecijalizacije
                        : DateTime.Today
                });
            }

            b.Specijalizacije = nove;

            return b;
        }

        private void SrediFormu(FormMode mode)
        {
            switch (mode)
            {
                case FormMode.Add:
                    btnIzmeni.Visible = false;
                    btnNazad.Visible = false;
                    break;

                case FormMode.Edit:
                    btnKreiraj.Visible = false;
                    break;

                case FormMode.Details:
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    txtKorisnickoIme.Enabled = false;
                    txtSifra.Visible = false;
                    txtIme.Enabled = false;
                    txtPrezime.Enabled = false;
                    txtTelefon.Enabled = false;
                    clbKategorije.Enabled = false;
                    break;
            }
        }
    }
}
