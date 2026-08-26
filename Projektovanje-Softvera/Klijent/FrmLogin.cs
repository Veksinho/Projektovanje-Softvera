using Klijent.GuiControllers;

namespace Klijent
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public bool Validacija()
        {
            txtKorisnickoIme.BackColor = Color.White;
            txtSifra.BackColor = Color.White;
            bool isValid = true;
            if (string.IsNullOrEmpty(txtKorisnickoIme.Text))
            {
                txtKorisnickoIme.BackColor = Color.Salmon;
                isValid = false;
            }
            if (string.IsNullOrEmpty(txtSifra.Text))
            {
                txtSifra.BackColor = Color.Salmon;
                isValid = false;
            }
            return isValid;
        }

        private void btnPrijavi_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            bool isLoggedIn = LoginGuiController.Instance.Login(
                txtKorisnickoIme.Text.Trim(), txtSifra.Text);

            Cursor = Cursors.Default;

            if (isLoggedIn)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            txtSifra.Focus();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
