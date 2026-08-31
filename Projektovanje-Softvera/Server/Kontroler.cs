using Common.Domen;
using SistemskeOperacije;
using SistemskeOperacije.BrokerSO;
using SistemskeOperacije.DogadjajSO;
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

        #region Dogadjaj
        public Dogadjaj UbaciDogadjaj(Dogadjaj d)
        {
            UbaciDogadjajSO so = new UbaciDogadjajSO(d);
            so.ExecuteTemplate();
            return (Dogadjaj)so.Result!;
        }

        public Dogadjaj PromeniDogadjaj(Dogadjaj d)
        {
            PromeniDogadjajSO so = new PromeniDogadjajSO(d);
            so.ExecuteTemplate();
            return (Dogadjaj)so.Result!;
        }

        public Dogadjaj PretraziDogadjaj(Dogadjaj d)
        {
            PretraziDogadjajSO so = new PretraziDogadjajSO(d);
            so.ExecuteTemplate();
            return (Dogadjaj)so.Result!;
        }

        public Dogadjaj ObrisiDogadjaj(Dogadjaj d)
        {
            ObrisiDogadjajSO so = new ObrisiDogadjajSO(d);
            so.ExecuteTemplate();
            return (Dogadjaj)so.Result!;
        }

        public List<Dogadjaj> VratiListuDogadjaj(Dogadjaj d)
        {
            VratiListuDogadjajSO so = new VratiListuDogadjajSO(d);
            so.ExecuteTemplate();
            return (List<Dogadjaj>)so.Result!;
        }

        public List<Dogadjaj> VratiListuSviDogadjaj()
        {
            VratiListuSviDogadjajSO so = new VratiListuSviDogadjajSO();
            so.ExecuteTemplate();
            return (List<Dogadjaj>)so.Result!;
        }
        #endregion

        #region Broker
        public Broker KreirajBroker(Broker b)
        {
            KreirajBrokerSO so = new KreirajBrokerSO(b);
            so.ExecuteTemplate();
            return (Broker)so.Result!;
        }

        public Broker PromeniBroker(Broker b)
        {
            PromeniBrokerSO so = new PromeniBrokerSO(b);
            so.ExecuteTemplate();
            return (Broker)so.Result!;
        }

        public Broker PretraziBroker(Broker b)
        {
            PretraziBrokerSO so = new PretraziBrokerSO(b);
            so.ExecuteTemplate();
            return (Broker)so.Result!;
        }

        public Broker ObrisiBroker(Broker b)
        {
            ObrisiBrokerSO so = new ObrisiBrokerSO(b);
            so.ExecuteTemplate();
            return (Broker)so.Result!;
        }

        public List<Broker> VratiListuBroker(Broker b)
        {
            VratiListuBrokerSO so = new VratiListuBrokerSO(b);
            so.ExecuteTemplate();
            return (List<Broker>)so.Result!;
        }

        public List<Broker> VratiListuSviBroker()
        {
            VratiListuSviBrokerSO so = new VratiListuSviBrokerSO();
            so.ExecuteTemplate();
            return (List<Broker>)so.Result!;
        }
        #endregion
    }
}
