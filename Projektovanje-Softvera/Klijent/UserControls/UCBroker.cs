using Common.Domen;
using Common.Validacija;
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

        internal ProveraUnosa ProveraUnosa { get; }
        internal IReadOnlyDictionary<string, Control> Polja { get; }

        internal string PotvrdaSifre => txtPotvrdaSifre.Text.Trim();

        public UCBroker(FormMode mode, Broker broker, List<KategorijaDogadjaja> sveKategorije)
        {
            InitializeComponent();
            txtKorisnickoIme.MaxLength = Ogranicenja.BrokerKorisnickoIme;
            txtSifra.MaxLength = Ogranicenja.SifraMax;
            txtPotvrdaSifre.MaxLength = Ogranicenja.SifraMax;
            txtIme.MaxLength = Ogranicenja.BrokerIme;
            txtPrezime.MaxLength = Ogranicenja.BrokerPrezime;
            UnosPolja.Telefon(txtTelefon);

            ProveraUnosa = new ProveraUnosa(this);
            Polja = new Dictionary<string, Control>
            {
                [nameof(Broker.KorisnickoIme)] = txtKorisnickoIme,
                [nameof(Broker.Sifra)] = txtSifra,
                ["PotvrdaSifre"] = txtPotvrdaSifre,
                [nameof(Broker.Ime)] = txtIme,
                [nameof(Broker.Prezime)] = txtPrezime,
                [nameof(Broker.Telefon)] = txtTelefon
            };

            this.b = broker;

            txtId.Text = mode == FormMode.Add ? "" : b.IdBroker.ToString();
            txtKorisnickoIme.Text = b.KorisnickoIme;
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
            string sifra = txtSifra.Text.Trim();
            b.Sifra = sifra.Length == 0 ? null : sifra;
            b.Ime = txtIme.Text.Trim();
            b.Prezime = txtPrezime.Text.Trim();
            b.Telefon = txtTelefon.Text.Trim();

            var nove = new List<BrokerKategorijaDogadjaja>();

            foreach (KategorijaDogadjaja k in clbKategorije.CheckedItems)
            {
                BrokerKategorijaDogadjaja? stara = b.Specijalizacije.FirstOrDefault(s =>
                    s.KategorijaDogadjaja != null
                    && s.KategorijaDogadjaja.IdKategorijaDogadjaja == k.IdKategorijaDogadjaja);

                nove.Add(new BrokerKategorijaDogadjaja
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
                    lblSifra.Text = "Šifra";
                    lblPotvrdaSifre.Text = "Potvrda šifre";
                    break;

                case FormMode.Edit:
                    btnKreiraj.Visible = false;
                    lblSifra.Text = "Nova šifra";
                    lblPotvrdaSifre.Text = "Potvrda nove šifre";
                    var toolTip = new ToolTip();
                    toolTip.SetToolTip(txtSifra, "Ostavite prazno ako ne menjate šifru.");
                    toolTip.SetToolTip(txtPotvrdaSifre, "Ostavite prazno ako ne menjate šifru.");
                    Disposed += (s, e) => toolTip.Dispose();
                    break;

                case FormMode.Details:
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    txtKorisnickoIme.Enabled = false;
                    lblSifra.Visible = false;
                    txtSifra.Visible = false;
                    lblPotvrdaSifre.Visible = false;
                    txtPotvrdaSifre.Visible = false;
                    txtIme.Enabled = false;
                    txtPrezime.Enabled = false;
                    txtTelefon.Enabled = false;
                    clbKategorije.Enabled = false;
                    break;
            }
        }
    }
}
