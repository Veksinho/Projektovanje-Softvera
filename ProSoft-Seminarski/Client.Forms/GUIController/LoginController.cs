using Client.Forms.Exceptions;
using Client.Forms.ServerCommunication;
using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Forms.Session;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Client.Forms.GUIController
{
    public class LoginController
    {
        public void Connect() { 
            Communication.Instance.Connect();
        }

        internal void Prijavljivanje(FrmLogin frmLogin)
        {
            if (!ValidacijaPrijavljivanja(frmLogin)) return;
            try
            {
                Korisnik k = new Korisnik()
                {
                   Email = frmLogin.TxtEmail.Text,
                   Lozinka = frmLogin.TxtLozinka.Text,
                };

                SessionData.Instance.Korisnik = Communication.Instance.SendRequestGetResult<Korisnik>(Common.Communication.Operation.Prijavljivanje, k);

                if (SessionData.Instance.Korisnik != null) {
                    MessageBox.Show("Uspešno ste se prijavili!");
                    frmLogin.DialogResult = DialogResult.OK;
                } else {
                    MessageBox.Show("Korisnik ne postoji u bazi!");
                }
            }
            catch (SystemOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SocketException)
            {
                MessageBox.Show($"Greska pri radu sa serverom!");
            }
        }

        bool ValidacijaPrijavljivanja(FrmLogin frmLogin)
        {
            bool prazanInput = string.IsNullOrEmpty(frmLogin.TxtEmail.Text) || string.IsNullOrEmpty(frmLogin.TxtLozinka.Text);

            if (prazanInput)
            {
                frmLogin.TxtEmail.BackColor = Color.Salmon;
                frmLogin.TxtLozinka.BackColor = Color.Salmon;
            }

            return prazanInput;
        }
    }
}
