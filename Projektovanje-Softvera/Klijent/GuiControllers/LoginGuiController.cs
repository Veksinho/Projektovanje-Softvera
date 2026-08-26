using Common.Domen;
using Klijent.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.GuiControllers
{
    public class LoginGuiController
    {
        private static LoginGuiController? instance;

        public static LoginGuiController Instance => instance ??= new LoginGuiController();

        private LoginGuiController()
        {
        }

        private FrmLogin frmLogin;

        internal void ShowFrmLogin()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmLogin = new FrmLogin();
                frmLogin.AutoSize = true;
                Application.Run(frmLogin);
            }
            catch (SocketException)
            {
                MessageBox.Show("Nije moguce uspostaviti komunikaciju sa serverom!");
            }
        }

        internal bool Login(string username, string password)
        {
            try
            {
                if (!frmLogin.Validacija())
                {
                    MessageBox.Show("Molimo popunite sva polja.");
                    return false;
                }

                Komunikacija.Instance.Connect();

                Broker broker = new Broker
                {
                    KorisnickoIme = username,
                    Sifra = password
                };

                Session.Instance.LoggedInBroker = Komunikacija.Instance.PrijaviBroker(broker);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prijavljivanje",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Odjavi()
        {
            Session.Instance.LogOut();
            Komunikacija.Instance.Disconnect();
        }
    }
}
