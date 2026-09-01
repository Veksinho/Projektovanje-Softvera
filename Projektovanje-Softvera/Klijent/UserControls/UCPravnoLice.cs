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
    public partial class UCPravnoLice : UserControl, ISubtypeControl
    {
        public UCPravnoLice()
        {
            InitializeComponent();
        }

        public Konsignator NapraviPrazan() => new PravnoLice();

        public void Popuni(Konsignator k)
        {
            if (k is not PravnoLice pl) return;

            txtNaziv.Text = pl.NazivFirme;
            txtPib.Text = pl.Pib;
            txtMaticniBroj.Text = pl.MaticniBroj;
        }

        public void Procitaj(Konsignator k)
        {
            if (k is not PravnoLice pl) return;

            pl.NazivFirme = txtNaziv.Text.Trim();
            pl.Pib = txtPib.Text.Trim();
            pl.MaticniBroj = txtMaticniBroj.Text.Trim();
        }

        public void SrediFormu(FormMode mode)
        {
            if (mode != FormMode.Details) return;

            txtNaziv.Enabled = false;
            txtPib.Enabled = false;
            txtMaticniBroj.Enabled = false;
        }
    }
}
