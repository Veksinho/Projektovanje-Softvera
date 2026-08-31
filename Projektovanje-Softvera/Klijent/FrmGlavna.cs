using Klijent.GuiControllers;
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

namespace Klijent
{
    public partial class FrmGlavna : Form
    {
        public FrmGlavna()
        {
            InitializeComponent();
            PrikaziPrijavljenogBrokera();

            mniKategorijaDogadjajaUbaci.Click += (sender, e) => KategorijaDogadjajaGuiController.Instance.PrikaziFormuNova();
            mniKategorijaDogadjajaPretrazi.Click += (sender, e) => KategorijaDogadjajaGuiController.Instance.PrikaziFormuPretraga();

            mniDogadjajUbaci.Click += (s, e) => DogadjajGuiController.Instance.PrikaziFormuNova();
            mniDogadjajPretrazi.Click += (s, e) => DogadjajGuiController.Instance.PrikaziFormuPretraga();

            mniBrokerKreiraj.Click += (s, e) => BrokerGuiController.Instance.PrikaziFormuNova();
            mniBrokerPretrazi.Click += (s, e) => BrokerGuiController.Instance.PrikaziFormuPretraga();

        }

        public void ChangePanel(UserControl control)
        {
            pnlSadrzaj.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlSadrzaj.Controls.Add(control);
            pnlSadrzaj.AutoSize = true;
            pnlSadrzaj.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void PrikaziPrijavljenogBrokera()
        {
            if (Session.Instance.LoggedInBroker == null)
            {
                lblPrijavljeniBroker.Text = "Niko nije prijavljen";
                return;
            }

            lblPrijavljeniBroker.Text = $"{Session.Instance.LoggedInBroker.Ime} {Session.Instance.LoggedInBroker.Prezime}";
        }

        private void mniOdjava_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show(
                "Da li zelite da se odjavite?", "Odjava",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation == DialogResult.Yes)
            {
                LoginGuiController.Instance.Odjavi();
                Close();
            }
        }

        private void FrmGlavna_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginGuiController.Instance.Odjavi();
        }
    }
}
