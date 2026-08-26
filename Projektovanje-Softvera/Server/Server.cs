using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        private const string IpAddress = "127.0.0.1";
        private const int Port = 9999;

        private const int Backlog = 5;

        private Socket serverSocket;

        private volatile bool isRunning;

        private readonly List<ClientHandler> clients = new List<ClientHandler>();

        public bool IsRunning => isRunning;

        public Server()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (isRunning)
            {
                return;
            }

            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(IpAddress), Port));
            serverSocket.Listen(Backlog);

            isRunning = true;

            Thread acceptClientsThread = new Thread(AcceptClient)
            {
                IsBackground = true
            };
            acceptClientsThread.Start();
        }

        public void Stop()
        {
            if (!isRunning)
            {
                return;
            }

            isRunning = false;

            lock (clients)
            {
                foreach (ClientHandler client in clients.ToList())
                {
                    client.Stop();
                }

                clients.Clear();
            }

            serverSocket?.Close();
        }

        private void AcceptClient()
        {
            try
            {
                while (isRunning)
                {
                    Socket clientSocket = serverSocket!.Accept();

                    ClientHandler client = new ClientHandler(clientSocket);
                    client.Disconnected += OnClientDisconnected;

                    lock (clients)
                    {
                        clients.Add(client);
                    }

                    Thread clientThread = new Thread(client.Handle)
                    {
                        IsBackground = true
                    };
                    clientThread.Start();
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("SE>>> " + ex.Message);
            }
            catch (IOException ex)
            {
                Debug.WriteLine("IOE>>> " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        private void OnClientDisconnected(ClientHandler client)
        {
            lock (clients)
            {
                clients.Remove(client);
            }
        }
    }
}
