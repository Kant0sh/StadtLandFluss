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
    class Client
    {

        private string ipAddress;
        private int port;

        TcpClient client;

        StreamWriter sw;
        StreamReader sr;

        public Client(string ipAddress, int port)
        {

            this.ipAddress = ipAddress;
            this.port = port;

        }

        public void Run()
        {
            client = null;
            try
            {

                client = new TcpClient(ipAddress, port);

                Thread t = new Thread(WaitForReply);
                t.Start(client);
                
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (client != null)
                {
                    Close();
                }
            }
        }

        public void Send(string send)
        {
            sw.WriteLine(send);
            sw.Flush();
        }

        public void Close()
        {
            sw.Close();
            sr.Close();
            client.Close();
        }

        private void WaitForReply(object cli)
        {

            TcpClient client = (TcpClient)cli;

            try
            {
                string reply = string.Empty;
                while (!(reply = sr.ReadLine()).Equals("Exit"))
                {
                    OnMessageReceived(reply);
                }
                Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Close();
            }
        }

        public delegate void MessageReceivedHandler(string msg);
        public event MessageReceivedHandler MessageReceived;

        protected virtual void OnMessageReceived(string msg)
        {
            if(MessageReceived != null)
            {
                MessageReceived(msg);
            }
        }

    }
}
