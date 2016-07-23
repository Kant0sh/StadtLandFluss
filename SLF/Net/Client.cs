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

namespace SLF.Net
{

    public class Client
    {

        private string ipAddress;
        private int port;

        StreamWriter sw;
        StreamReader sr;

        public Client(string ipAddress, int port)
        {

            this.ipAddress = ipAddress;
            this.port = port;

        }

        public void Run()
        {

            TcpClient client = new TcpClient(ipAddress, port);

            sw = new StreamWriter(client.GetStream());
            sr = new StreamReader(client.GetStream());

            Thread t = new Thread(WaitForReply);
            t.Start(client);

        }

        public void Send(string send)
        {
            sw.WriteLine(send);
            sw.Flush();
        }

        private void WaitForReply(object cli)
        {

            TcpClient client = (TcpClient)cli;

            try
            {
                sr = new StreamReader(client.GetStream());
                string reply = string.Empty;
                while (!(reply = sr.ReadLine()).Equals("Exit"))
                {
                    HandleMessage(reply);
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
                    sr.Close();
                    client.Close();
                }
            }
        }

        private void HandleMessage(string msg)
        {
            Message type = MessageToolkit.GetType(msg);
            string[] msgArray = MessageToolkit.GetMessageArray(msg);

            switch (type)
            {
                case Message.ConnectionCount:
                    OnMessageConnectionCount(msgArray);
                    break;
                case Message.CatList:
                    OnCatListReceived(MessageToolkit.ConvertToCatList(msg));
                    break;
                case Message.ReadyToSendCat:
                    OnReadyToSendCat(MessageToolkit.GetSingleInt(msg));
                    break;
                default:
                    break;
            }

        }

        public delegate void PacketConnectionCount(string[] msgArray);
        public event PacketConnectionCount ConnectionCountEvent;
        protected virtual void OnMessageConnectionCount(string[] msgArray)
        {
            ConnectionCountEvent?.Invoke(msgArray);
        }

        public delegate void CatListReceived(List<Category> catList);
        public event CatListReceived CatListReceivedEvent;
        protected virtual void OnCatListReceived(List<Category> catList)
        {
            CatListReceivedEvent?.Invoke(catList);
        }

        public delegate void ReadyToSendCat(int count);
        public event ReadyToSendCat ReadyToSendCatEvent;
        protected virtual void OnReadyToSendCat(int count)
        {
            ReadyToSendCatEvent?.Invoke(count);
        }

    }
}
