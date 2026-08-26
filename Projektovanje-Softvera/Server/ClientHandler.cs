using Common.Domen;
using Common.Komunikacija;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ClientHandler
    {
        private readonly Socket clientSocket;
        private readonly JsonNetworkSerializer serializer;

        public event Action<ClientHandler>? Disconnected;

        public ClientHandler(Socket socket)
        {
            this.clientSocket = socket;
            serializer = new JsonNetworkSerializer(socket);
        }

        public void Handle()
        {
            try
            {
                while (true)
                {
                    Zahtev zahtev = serializer.Receive<Zahtev>();
                    Odgovor odgovor = ProcessRequest(zahtev);
                    serializer.Send(odgovor);
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("Komunikacija sa klijentom je prekinuta");
                Debug.WriteLine(">>>SOCKET>>> " + ex.Message);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("Komunikacija sa klijentom je prekinuta");
                Debug.WriteLine(">>>IO>>> " + ex.Message);
            }
            finally
            {
                Stop();
                Disconnected?.Invoke(this);
            }
        }

        private Odgovor ProcessRequest(Zahtev zahtev)
        {
            try
            {
                switch (zahtev.Operacija)
                {
                    case Operacija.PrijaviBroker:
                        Broker broker = serializer.ReadType<Broker>(zahtev.Objekat)!;
                        return Odgovor.Uspeh(Kontroler.Instance.PrijaviBroker(broker));

                    default:
                        return Odgovor.Neuspeh($"Operacija {zahtev.Operacija} još uvek nije implementirana.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Odgovor.Neuspeh(ex.Message);
            }
        }

        public void Stop()
        {
            serializer.Close();

            try
            {
                clientSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            clientSocket.Close();
        }
    }
}
