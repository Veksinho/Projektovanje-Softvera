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

        public void PokreniGlavnuFormu()
        {
            frmGlavna = new FrmGlavna();
            Application.Run(frmGlavna);
        }

        public void PromeniSadrzaj(UserControl control)
        {
            frmGlavna?.PostaviSadrzaj(control);
        }
    }
}
