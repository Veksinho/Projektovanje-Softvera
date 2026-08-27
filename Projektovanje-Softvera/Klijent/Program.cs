using Klijent.GuiControllers;

namespace Klijent
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                if (LoginGuiController.Instance.ShowFrmLogin())
                {
                    MainCoordinator.Instance.PokreniGlavnuFormu();
                }
            }
            finally
            {
                Komunikacija.Instance.Disconnect();
            }
        }
    }
}