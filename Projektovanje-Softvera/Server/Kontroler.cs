using Common.Domen;
using SistemskeOperacije;
using SistemskeOperacije.BrokerSO;
using SistemskeOperacije.DogadjajSO;
using SistemskeOperacije.KartaSO;
using SistemskeOperacije.KategorijaDogadjajaSO;
using SistemskeOperacije.KonsignatorSO;
using SistemskeOperacije.ListingSO;
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

        #region Konsignator
        public Konsignator KreirajKonsignator(Konsignator k)
        {
            KreirajKonsignatorSO so = new KreirajKonsignatorSO(k);
            so.ExecuteTemplate();
            return (Konsignator)so.Result!;
        }

        public Konsignator PromeniKonsignator(Konsignator k)
        {
            PromeniKonsignatorSO so = new PromeniKonsignatorSO(k);
            so.ExecuteTemplate();
            return (Konsignator)so.Result!;
        }

        public Konsignator PretraziKonsignator(Konsignator k)
        {
            PretraziKonsignatorSO so = new PretraziKonsignatorSO(k);
            so.ExecuteTemplate();
            return (Konsignator)so.Result!;
        }

        public Konsignator ObrisiKonsignator(Konsignator k)
        {
            ObrisiKonsignatorSO so = new ObrisiKonsignatorSO(k);
            so.ExecuteTemplate();
            return (Konsignator)so.Result!;
        }

        public List<Konsignator> VratiListuKonsignator(Konsignator k)
        {
            VratiListuKonsignatorSO so = new VratiListuKonsignatorSO(k);
            so.ExecuteTemplate();
            return (List<Konsignator>)so.Result!;
        }

        public List<Konsignator> VratiListuSviKonsignator()
        {
            VratiListuSviKonsignatorSO so = new VratiListuSviKonsignatorSO();
            so.ExecuteTemplate();
            return (List<Konsignator>)so.Result!;
        }
        #endregion

        #region Karta
        public Karta KreirajKarta(Karta ka)
        {
            KreirajKartaSO so = new KreirajKartaSO(ka);
            so.ExecuteTemplate();
            return (Karta)so.Result!;
        }

        public Karta PromeniKarta(Karta ka)
        {
            PromeniKartaSO so = new PromeniKartaSO(ka);
            so.ExecuteTemplate();
            return (Karta)so.Result!;
        }

        public Karta PretraziKarta(Karta ka)
        {
            PretraziKartaSO so = new PretraziKartaSO(ka);
            so.ExecuteTemplate();
            return (Karta)so.Result!;
        }

        public List<Karta> VratiListuKarta(Karta ka)
        {
            VratiListuKartaSO so = new VratiListuKartaSO(ka);
            so.ExecuteTemplate();
            return (List<Karta>)so.Result!;
        }

        public List<Karta> VratiListuSviKarta()
        {
            VratiListuSviKartaSO so = new VratiListuSviKartaSO();
            so.ExecuteTemplate();
            return (List<Karta>)so.Result!;
        }
        #endregion

        #region Listing
        public Listing KreirajListing(Listing l)
        {
            KreirajListingSO so = new KreirajListingSO(l);
            so.ExecuteTemplate();
            return (Listing)so.Result!;
        }

        public Listing PromeniListing(Listing l)
        {
            PromeniListingSO so = new PromeniListingSO(l);
            so.ExecuteTemplate();
            return (Listing)so.Result!;
        }

        public Listing PretraziListing(Listing l)
        {
            PretraziListingSO so = new PretraziListingSO(l);
            so.ExecuteTemplate();
            return (Listing)so.Result!;
        }

        public List<Listing> VratiListuListing(Listing l)
        {
            VratiListuListingSO so = new VratiListuListingSO(l);
            so.ExecuteTemplate();
            return (List<Listing>)so.Result!;
        }
        #endregion
    }
}
