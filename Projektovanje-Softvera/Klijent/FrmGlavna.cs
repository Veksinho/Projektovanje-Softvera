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

            mniKonsignatorKreiraj.Click += (s, e) => KonsignatorGuiController.Instance.PrikaziFormuNova();
            mniKonsignatorPretrazi.Click += (s, e) => KonsignatorGuiController.Instance.PrikaziFormuPretraga();

            mniKartaKreiraj.Click += (s, e) => KartaGuiController.Instance.PrikaziFormuNova();
            mniKartaPretrazi.Click += (s, e) => KartaGuiController.Instance.PrikaziFormuPretraga();

            mniListingKreiraj.Click += (s, e) => ListingGuiController.Instance.PrikaziFormuNova();
            mniListingPretrazi.Click += (s, e) => ListingGuiController.Instance.PrikaziFormuPretraga();
        }

        public void ChangePanel(UserControl control)
        {
            foreach (Control c in pnlSadrzaj.Controls) c.Dispose();
            pnlSadrzaj.Controls.Clear();
            control.Dock = DockStyle.Fill;
            pnlSadrzaj.Controls.Add(control);
        }

        public void PrikaziPrijavljenogBrokera()
        {
            if (Session.Instance.LoggedInBroker == null)
            {
                lblPrijavljeniBroker.Text = "Niko nije prijavljen";
                return;
            }

            lblPrijavljeniBroker.Text = $"{Session.Instance.LoggedInBroker}";
        }

        public bool LogoutRequested { get; private set; }

        private void mniOdjava_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show(
                "Da li zelite da se odjavite?", "Odjava",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmation != DialogResult.Yes) return;

            Odjavi();
        }

        public void Odjavi()
        {
            LogoutRequested = true;
            Close();
        }
    }
}
