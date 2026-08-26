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
