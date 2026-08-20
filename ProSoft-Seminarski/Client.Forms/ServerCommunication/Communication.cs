using Client.Forms.Exceptions;
using Common.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client.Forms.ServerCommunication
{
    public class Communication
    {
        private Socket socket;
        private CommunicationHelper helper;
        private static Communication instance;

        private Communication() {}
        public static Communication Instance
        {
            get
            {
                if (instance == null) instance = new Communication();
                return instance;
            }
        }
        public Output SendRequestGetResult<Output>(Operation op, object i = null) where Output : class
        {
            SendRequest(op, i);
            return GetResult<Output>();
        }

        public void SendRequestNoResult(Operation op, object i)
        {
            SendRequest(op, i);
            GetResult();
        }

        private T GetResult<T>() where T : class
        {
            Response response = helper.Receive<Response>();
            if (response.IsSuccessful)
            {
                return (T)response.Result;
            }
            else
            {
                throw new SystemOperationException(response.Message);
            }
        }

        private void GetResult()
        {
            Response response = helper.Receive<Response>();
            if (!response.IsSuccessful)
            {
                throw new SystemOperationException(response.Message);
            }
        }

        private void SendRequest(Operation operation, object requestObject = null)
        {
            try
            {
                Request r = new Request
                {
                    Operation = operation,
                    RequestObject = requestObject
                };
                helper.Send(r);
            }
            catch (IOException ex)
            {
                throw new ServerCommunicationException(ex.Message);
            }
        }

        internal void Connect()
        {
            if (socket == null || !socket.Connected)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect("127.0.0.1", 4000);
                helper = new CommunicationHelper(socket);
            }
        }

        internal void Close()
        {
            if (socket == null) return;
            Request request = new Request
            {
                Operation = Operation.ZatvoriAplikaciju,
            };
            helper.Send(request);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
}
