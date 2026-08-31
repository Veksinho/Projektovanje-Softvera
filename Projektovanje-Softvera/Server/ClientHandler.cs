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

                    #region KategorijaDogadjaja
                    case Operacija.UbaciKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.UbaciKategorijaDogadjaja(serializer.ReadType<KategorijaDogadjaja>(zahtev.Objekat)!));

                    case Operacija.PromeniKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.PromeniKategorijaDogadjaja(serializer.ReadType<KategorijaDogadjaja>(zahtev.Objekat)!));

                    case Operacija.PretraziKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.PretraziKategorijaDogadjaja(serializer.ReadType<KategorijaDogadjaja>(zahtev.Objekat)!));

                    case Operacija.ObrisiKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.ObrisiKategorijaDogadjaja(serializer.ReadType<KategorijaDogadjaja>(zahtev.Objekat)!));

                    case Operacija.VratiListuKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuKategorijaDogadjaja(serializer.ReadType<KategorijaDogadjaja>(zahtev.Objekat)!));

                    case Operacija.VratiListuSviKategorijaDogadjaja:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuSviKategorijaDogadjaja());
                    #endregion

                    #region Dogadjaj
                    case Operacija.UbaciDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.UbaciDogadjaj(
                            serializer.ReadType<Dogadjaj>(zahtev.Objekat)!));

                    case Operacija.PromeniDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.PromeniDogadjaj(
                            serializer.ReadType<Dogadjaj>(zahtev.Objekat)!));

                    case Operacija.PretraziDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.PretraziDogadjaj(
                            serializer.ReadType<Dogadjaj>(zahtev.Objekat)!));

                    case Operacija.ObrisiDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.ObrisiDogadjaj(
                            serializer.ReadType<Dogadjaj>(zahtev.Objekat)!));

                    case Operacija.VratiListuDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuDogadjaj(
                            serializer.ReadType<Dogadjaj>(zahtev.Objekat)!));

                    case Operacija.VratiListuSviDogadjaj:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuSviDogadjaj());
                    #endregion

                    #region Broker
                    case Operacija.KreirajBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.KreirajBroker(
                            serializer.ReadType<Broker>(zahtev.Objekat)!));

                    case Operacija.PromeniBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.PromeniBroker(
                            serializer.ReadType<Broker>(zahtev.Objekat)!));

                    case Operacija.PretraziBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.PretraziBroker(
                            serializer.ReadType<Broker>(zahtev.Objekat)!));

                    case Operacija.ObrisiBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.ObrisiBroker(
                            serializer.ReadType<Broker>(zahtev.Objekat)!));

                    case Operacija.VratiListuBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuBroker(
                            serializer.ReadType<Broker>(zahtev.Objekat)!));

                    case Operacija.VratiListuSviBroker:
                        return Odgovor.Uspeh(Kontroler.Instance.VratiListuSviBroker());
                    #endregion

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
