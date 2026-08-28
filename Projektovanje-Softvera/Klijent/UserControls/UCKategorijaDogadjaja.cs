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
    public partial class UCKategorijaDogadjaja : UserControl
    {
        private readonly KategorijaDogadjaja kd;
        public UCKategorijaDogadjaja(FormMode mode, KategorijaDogadjaja kd)
        {
            InitializeComponent();

            this.kd = kd;
            
            txtId.Text = mode == FormMode.Edit ? kd.IdKategorijaDogadjaja.ToString() : "";
            txtNaziv.Text = kd.Naziv;
            txtOpis.Text = kd.Opis;

            SrediFormu(mode);
        }

        public KategorijaDogadjaja VratiObjekat()
        {
            kd.Naziv = txtNaziv.Text.Trim();
            kd.Opis = txtOpis.Text.Trim();

            return kd;
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
                    txtNaziv.Enabled = false;
                    txtOpis.Enabled = false;
                    break;
            }
        }
    }
}
