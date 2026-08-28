using Common.Domen;
using SistemskeOperacije;
using SistemskeOperacije.KategorijaDogadjajaSO;
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

        #region KategorijaDogadjaja
        public KategorijaDogadjaja UbaciKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            UbaciKategorijaDogadjajaSO so = new UbaciKategorijaDogadjajaSO(kd);
            so.ExecuteTemplate();
            return (KategorijaDogadjaja)so.Result!;
        }

        public KategorijaDogadjaja PromeniKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            PromeniKategorijaDogadjajaSO so = new PromeniKategorijaDogadjajaSO(kd);
            so.ExecuteTemplate();
            return (KategorijaDogadjaja)so.Result!;
        }

        public KategorijaDogadjaja PretraziKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            PretraziKategorijaDogadjajaSO so = new PretraziKategorijaDogadjajaSO(kd);
            so.ExecuteTemplate();
            return (KategorijaDogadjaja)so.Result!;

        }

        public KategorijaDogadjaja ObrisiKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            ObrisiKategorijaDogadjajaSO so = new ObrisiKategorijaDogadjajaSO(kd);
            so.ExecuteTemplate();
            return (KategorijaDogadjaja)so.Result!;
        }

        public List<KategorijaDogadjaja> VratiListuKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            VratiListuKategorijaDogadjajaSO so = new VratiListuKategorijaDogadjajaSO(kd);
            so.ExecuteTemplate();
            return (List<KategorijaDogadjaja>)so.Result!;
        }

        public List<KategorijaDogadjaja> VratiListuSviKategorijaDogadjaja()
        {
            VratiListuSviKategorijaDogadjajaSO so = new VratiListuSviKategorijaDogadjajaSO();
            so.ExecuteTemplate();
            return (List<KategorijaDogadjaja>)so.Result!;
        }
        #endregion

    }
}
