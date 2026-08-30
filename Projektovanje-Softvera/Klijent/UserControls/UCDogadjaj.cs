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
    public partial class UCDogadjaj : UserControl
    {
        private readonly Dogadjaj d;

        public UCDogadjaj(FormMode mode, Dogadjaj dogadjaj)
        {
            InitializeComponent();
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
