using SLF.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLF
{
    public class GameData
    {

        public Client client;

        public int connectionCount = 0;
        public int readyToSendCatCount = 0;

        public List<Category> catList = new List<Category>();
        public List<Category> allCatList;

        public GameData(Client client)
        {
            this.client = client;

            client.ConnectionCountEvent += Client_ConnectionCountEvent;
            client.CatListReceivedEvent += Client_CatListReceivedEvent;
            client.ReadyToSendCatEvent += Client_ReadyToSendCatEvent;
        }

        private void Client_ReadyToSendCatEvent(int count)
        {
            readyToSendCatCount = count;
            CheckReadys();
        }

        private void Client_CatListReceivedEvent(List<Category> catList)
        {
            allCatList = catList;
        }

        private void Client_ConnectionCountEvent(string[] msgArray)
        {
            int i = 0;
            try
            {
                i = Int32.Parse(msgArray[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            connectionCount = i;
        }

        private void CheckReadys()
        {

            if(readyToSendCatCount >= connectionCount)
            {
                client.Send(MessageToolkit.CreateMessage(Message.CatList, MessageToolkit.ConvertFromCatList(catList)));
            }

        }

    }
}
