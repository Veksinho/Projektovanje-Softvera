using Common.Domen;
using SistemskeOperacije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Kontroler
    {
        private static Kontroler? instance;

        private static readonly object lockObject = new object();

        public static Kontroler Instance
        {
            get
            {
                lock (lockObject)
                {
                    instance ??= new Kontroler();
                    return instance;
                }
            }
        }

        private Kontroler()
        {
        }

        public Broker PrijaviBroker(Broker broker)
        {
            PrijaviBrokerSO operation = new PrijaviBrokerSO(broker);
            operation.ExecuteTemplate();
            return (Broker)operation.Result!;
        }
    }
}
