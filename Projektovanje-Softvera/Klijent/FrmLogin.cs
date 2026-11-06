using Common.Domen;
using Common.Validacija;
using Klijent.GuiControllers;
using Klijent.Utils;

namespace Klijent
{
    public partial class FrmLogin : Form
    {
        private bool unosIspravan;

        internal ProveraUnosa ProveraUnosa { get; }
        internal IReadOnlyDictionary<string, Control> Polja { get; }

        public FrmLogin()
        {
            InitializeComponent();
            txtKorisnickoIme.MaxLength = Ogranicenja.BrokerKorisnickoIme;

            ProveraUnosa = new ProveraUnosa(this);
            Polja = new Dictionary<string, Control>
            {
                [nameof(Broker.KorisnickoIme)] = txtKorisnickoIme,
                [nameof(Broker.Sifra)] = txtSifra
            };
        }

        public bool Validacija(Broker broker)
        {
            unosIspravan = ProveraUnosa.Prikazi(PravilaBroker.ProveriPrijavu(broker), Polja);
            return unosIspravan;
        }

        private void btnPrijavi_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            bool isLoggedIn = LoginGuiController.Instance.Login(
                txtKorisnickoIme.Text.Trim(), txtSifra.Text.Trim());

            Cursor = Cursors.Default;

            if (isLoggedIn)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (unosIspravan)
                txtSifra.Focus();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
