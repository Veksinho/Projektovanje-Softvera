using Common.Domen;
using Common.Komunikacija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Klijent
{
    internal class Komunikacija
    {
        private const string IpAddress = "127.0.0.1";
        private const int Port = 9999;

        private static Komunikacija? instance;

        public static Komunikacija Instance
        {
            get
            {
                instance ??= new Komunikacija();
                return instance;
            }
        }

        private Socket? socket;
        private JsonNetworkSerializer? serializer;

        private Komunikacija()
        {
        }

        public bool IsConnected => socket != null && socket.Connected;

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(IpAddress), Port));
            serializer = new JsonNetworkSerializer(socket);
        }

        public void Disconnect()
        {
            serializer?.Close();

            try
            {
                socket?.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {
            }

            socket?.Close();

            serializer = null;
            socket = null;
        }

        public Broker PrijaviBroker(Broker broker)
        {
            Odgovor response = SendRequest(Operacija.PrijaviBroker, broker);
            return serializer.ReadType<Broker>(response.Objekat)!;
        }

        #region KategorijaDogadjaja
        public KategorijaDogadjaja UbaciKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            Odgovor response = SendRequest(Operacija.UbaciKategorijaDogadjaja, kd);
            return serializer.ReadType<KategorijaDogadjaja>(response.Objekat)!;
        }

        public KategorijaDogadjaja PromeniKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            Odgovor response = SendRequest(Operacija.PromeniKategorijaDogadjaja, kd);
            return serializer.ReadType<KategorijaDogadjaja>(response.Objekat)!;
        }

        public KategorijaDogadjaja PretraziKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            Odgovor response = SendRequest(Operacija.PretraziKategorijaDogadjaja, kd);
            return serializer.ReadType<KategorijaDogadjaja>(response.Objekat)!;
        }

        public KategorijaDogadjaja ObrisiKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            Odgovor response = SendRequest(Operacija.ObrisiKategorijaDogadjaja, kd);
            return serializer.ReadType<KategorijaDogadjaja>(response.Objekat)!;
        }

        public List<KategorijaDogadjaja> VratiListuKategorijaDogadjaja(KategorijaDogadjaja kd)
        {
            Odgovor response = SendRequest(Operacija.VratiListuKategorijaDogadjaja, kd);
            return serializer.ReadType<List<KategorijaDogadjaja>>(response.Objekat)!;
        }

        public List<KategorijaDogadjaja> VratiListuSviKategorijaDogadjaja()
        {
            Odgovor response = SendRequest(Operacija.VratiListuSviKategorijaDogadjaja, null);
            return serializer.ReadType<List<KategorijaDogadjaja>>(response.Objekat)!;
        }
        #endregion

        #region Dogadjaj
        public Dogadjaj UbaciDogadjaj(Dogadjaj d)
        {
            Odgovor response = SendRequest(Operacija.UbaciDogadjaj, d);
            return serializer.ReadType<Dogadjaj>(response.Objekat)!;
        }

        public Dogadjaj PromeniDogadjaj(Dogadjaj d)
        {
            Odgovor response = SendRequest(Operacija.PromeniDogadjaj, d);
            return serializer.ReadType<Dogadjaj>(response.Objekat)!;
        }

        public Dogadjaj PretraziDogadjaj(Dogadjaj d)
        {
            Odgovor response = SendRequest(Operacija.PretraziDogadjaj, d);
            return serializer.ReadType<Dogadjaj>(response.Objekat)!;
        }

        public Dogadjaj ObrisiDogadjaj(Dogadjaj d)
        {
            Odgovor response = SendRequest(Operacija.ObrisiDogadjaj, d);
            return serializer.ReadType<Dogadjaj>(response.Objekat)!;
        }

        public List<Dogadjaj> VratiListuDogadjaj(Dogadjaj d)
        {
            Odgovor response = SendRequest(Operacija.VratiListuDogadjaj, d);
            return serializer.ReadType<List<Dogadjaj>>(response.Objekat)!;
        }

        public List<Dogadjaj> VratiListuSviDogadjaj()
        {
            Odgovor response = SendRequest(Operacija.VratiListuSviDogadjaj, null);
            return serializer.ReadType<List<Dogadjaj>>(response.Objekat)!;
        }
        #endregion


        private Odgovor SendRequest(Operacija operation, object? data)
        {
            if (!IsConnected)
            {
                throw new Exception("Veza sa serverom nije uspostavljena.");
            }

            serializer!.Send(new Zahtev(operation, data));

            Odgovor response = serializer.Receive<Odgovor>();

            if (!response.Uspesno)
            {
                throw new Exception(response.Greska ?? "Nepoznata greska na serveru.");
            }

            return response;
        }
    }
}
