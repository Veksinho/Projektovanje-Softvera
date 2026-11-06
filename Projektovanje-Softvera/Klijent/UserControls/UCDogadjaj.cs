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
    public partial class UCDogadjaj : UserControl
    {
        private readonly Dogadjaj d;

        internal ProveraUnosa ProveraUnosa { get; }
        internal IReadOnlyDictionary<string, Control> Polja { get; }

        public UCDogadjaj(FormMode mode, Dogadjaj dogadjaj)
        {
            InitializeComponent();
            txtNaziv.MaxLength = Ogranicenja.DogadjajNaziv;
            txtMesto.MaxLength = Ogranicenja.DogadjajMesto;

            ProveraUnosa = new ProveraUnosa(this);
            Polja = new Dictionary<string, Control>
            {
                [nameof(Dogadjaj.Naziv)] = txtNaziv,
                [nameof(Dogadjaj.Mesto)] = txtMesto,
                [nameof(Dogadjaj.DatumOdrzavanja)] = dtpDatumOdrzavanja
            };
            this.d = dogadjaj;

            txtId.Text = mode == FormMode.Add ? "" : d.IdDogadjaj.ToString();
            txtNaziv.Text = d.Naziv;
            txtMesto.Text = d.Mesto;
            dtpDatumOdrzavanja.Value = d.DatumOdrzavanja == default
                ? DateTime.Today
                : d.DatumOdrzavanja;

            SrediFormu(mode);
        }

        public Dogadjaj VratiObjekat()
        {
            d.Naziv = txtNaziv.Text.Trim();
            d.Mesto = txtMesto.Text.Trim();
            d.DatumOdrzavanja = dtpDatumOdrzavanja.Value.Date;
            return d;
        }

        private void SrediFormu(FormMode mode)
        {
            switch (mode)
            {
                case FormMode.Add:
                    dtpDatumOdrzavanja.MinDate = DateTime.Today;
                    btnIzmeni.Visible = false;
                    btnNazad.Visible = false;
                    break;
                case FormMode.Edit:
                    btnKreiraj.Visible = false;
                    break;
                case FormMode.Details:
                    btnKreiraj.Visible = false;
                    btnIzmeni.Visible = false;
                    txtNaziv.Enabled = false;
                    txtMesto.Enabled = false;
                    dtpDatumOdrzavanja.Enabled = false;
                    break;
            }
        }
    }
}
