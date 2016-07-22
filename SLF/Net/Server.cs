using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Collections;

namespace SLF.Net
{
    public class Server
    {

        private int port;

        TcpListener listener;
        public List<TcpClient> clientList;

        public Server(int port)
        {

            this.port = port;

            clientList = new List<TcpClient>();

        }

        public void Run()
        {
            listener = null;
            try
            {

                listener = new TcpListener(new IPEndPoint(IPAddress.Any, port));
                listener.Start();

                Console.WriteLine("Waiting for a Client to connect...");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Client connected!");
                    Thread cliThread = new Thread(ProcessClientRequests);
                    clientList.Add(client);
                    cliThread.Start(client);
                    OnClientConnected();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }

        public delegate void ClientConnected();
        public event ClientConnected ClientConnectedEvent;
        protected virtual void OnClientConnected()
        {
            ClientConnectedEvent?.Invoke();
        }

        private void ProcessClientRequests(object argument)
        {
            TcpClient client = (TcpClient)argument;

            try
            {
                StreamReader sr = new StreamReader(client.GetStream());

                string s = string.Empty;
                while (!(s = sr.ReadLine()).Equals("Exit"))
                {
                    HandleMessage(client, s);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if(client != null)
                {
                    client.Close();
                }
            }

        }

        public void Send(TcpClient client, string msg)
        {
            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.WriteLine(msg);
            sw.Flush();
            sw.Close();
        }

        public void SendToAll(string msg)
        {
            foreach (TcpClient cli in clientList)
            {

                Thread t = new Thread(SendToClient);
                ArrayList args = new ArrayList();
                args.Add(cli);
                args.Add(msg);
                t.Start(args);

            }

        }

        private void SendToClient(object args)
        {
            TcpClient client = (TcpClient)((ArrayList)args)[0];
            string msg = (string)((ArrayList)args)[1];

            //Console.WriteLine("SYNCING...");

            try
            {

                StreamWriter sw = new StreamWriter(client.GetStream());
                //Console.WriteLine("Writing " + syncStr + " to SW!");
                sw.WriteLine(msg);
                sw.Flush();

            }
            catch (Exception e)
            {
                Console.WriteLine("Sync failed!");
            }

        }

        private void HandleMessage(TcpClient cli, string msg)
        {

            int type = MessageToolkit.GetType(msg);
            string[] msgArray = MessageToolkit.GetMessageArray(msg);

            switch (type)
            {
                case (int)Message.ReadyToSendCat:
                    break;
                default:
                    break;
            }

        }
    }
}
