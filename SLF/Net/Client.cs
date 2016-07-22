using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;
using SLF.Net;

namespace SLF
{

    public class Client
    {

        public static char SPLIT = '%';

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

                sw = new StreamWriter(client.GetStream());
                sr = new StreamReader(client.GetStream());

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
                    HandleMessage(reply);
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

        private void HandleMessage(string msg)
        {
            if (msg.Length == 0) return;
            string[] splitMsg = msg.Split(SPLIT);
            int id = -1;
            try
            {
                id = Int32.Parse(splitMsg[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            switch (id)
            {
                case (int)Packet.CatList:
                    break;
                default:
                    Close();
                    break;
            }

        }

    }
}
