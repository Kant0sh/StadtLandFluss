using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace SLF
{
    class Server
    {

        private int port;

        TcpListener listener;
        List<TcpClient> clientList;

        private string syncStr = string.Empty;

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

        private void ProcessClientRequests(object argument)
        {

            TcpClient client = (TcpClient)argument;

            try
            {
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());

                string s = string.Empty;
                while (!(s = sr.ReadLine()).Equals("Exit"))
                {
                                        
                }

                sr.Close();
                sw.Close();
                Console.WriteLine("Closing Client Connection!");
                clientList.Remove(client);
                client.Close();

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

        public void Send(string msg)
        {

        }

        private void SyncMessage(string s)
        {

            syncStr = s;

            foreach(TcpClient cli in clientList)
            {

                Thread t = new Thread(SyncToClient);
                t.Start(cli);

            }

        }

        private void SyncToClient(object cli)
        {
            TcpClient client = (TcpClient)cli;

            //Console.WriteLine("SYNCING...");

            try
            {

                StreamWriter sw = new StreamWriter(client.GetStream());
                //Console.WriteLine("Writing " + syncStr + " to SW!");
                sw.WriteLine(syncStr);
                sw.Flush();

            }
            catch(Exception e)
            {
                Console.WriteLine("Sync failed!");
            }

        }

    }
}
