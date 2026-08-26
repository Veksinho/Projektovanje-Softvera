using Common.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klijent.Utils
{
    public class Session
    {
        private static Session? instance;

        public static Session Instance => instance ??= new Session();

        private Session()
        {
        }

        public Broker? LoggedInBroker { get; set; }

        public bool IsLoggedIn => LoggedInBroker != null;

        public void LogOut()
        {
            LoggedInBroker = null;
        }
    }
}
