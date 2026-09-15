using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.GuiControllers
{
    internal class MainCoordinator
    {
        private static MainCoordinator? instance;

        public static MainCoordinator Instance => instance ??= new MainCoordinator();

        private MainCoordinator()
        {
        }

        private FrmGlavna? frmGlavna;

        public bool ShowFrmGlavna()
        {
            frmGlavna = new FrmGlavna();
            Application.Run(frmGlavna);

            bool odjava = frmGlavna.LogoutRequested;
            frmGlavna = null;
            return odjava;
        }

        public void ChangePanel(UserControl control)
        {
            frmGlavna?.ChangePanel(control);
        }

        public void OsveziPrijavljenogBrokera()
        {
            frmGlavna?.PrikaziPrijavljenogBrokera();
        }

        public void OdjaviBrokera()
        {
            frmGlavna?.Odjavi();
        }
    }
}
