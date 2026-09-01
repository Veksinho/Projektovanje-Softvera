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
    public partial class UCFizickoLice : UserControl, ISubtypeControl
    {
        public UCFizickoLice()
        {
            InitializeComponent();
        }

        public Konsignator NapraviPrazan() => new FizickoLice();

        public void Popuni(Konsignator k)
        {
            if (k is not FizickoLice fl) return;

            txtJmbg.Text = fl.Jmbg;
            txtIme.Text = fl.Ime;
            txtPrezime.Text = fl.Prezime;
            txtBrojLicneKarte.Text = fl.BrojLicneKarte;
        }

        public void Procitaj(Konsignator k)
        {
            if (k is not FizickoLice fl) return;

            fl.Jmbg = txtJmbg.Text.Trim();
            fl.Ime = txtIme.Text.Trim();
            fl.Prezime = txtPrezime.Text.Trim();
            fl.BrojLicneKarte = txtBrojLicneKarte.Text.Trim();
        }

        public void SrediFormu(FormMode mode)
        {
            if (mode != FormMode.Details) return;

            txtJmbg.Enabled = false;
            txtIme.Enabled = false;
            txtPrezime.Enabled = false;
            txtBrojLicneKarte.Enabled = false;
        }
    }
}
