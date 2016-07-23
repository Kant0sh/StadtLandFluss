using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLF.Net;

namespace SLF
{
    public class ServerData
    {

        Server server;

        public int catListsReceived = 0;
        public List<Category> allCatList = new List<Category>();
        public int readyToSendCatCount = 0;

        public ServerData(Server server)
        {
            this.server = server;
            server.ClientConnectedEvent += Server_ClientConnectedEvent;

            server.CatListReceivedEvent += Server_CatListReceivedEvent;
            server.ReadyToSendCatReceivedEvent += Server_ReadyToSendCatReceivedEvent;
        }

        private void Server_ReadyToSendCatReceivedEvent(bool ready)
        {
            if (ready)
            {
                readyToSendCatCount++;
                server.SendToAll(MessageToolkit.CreateMessage(Message.ReadyToSendCat, readyToSendCatCount));
                return;
            }
            readyToSendCatCount--;
            server.SendToAll(MessageToolkit.CreateMessage(Message.ReadyToSendCat, readyToSendCatCount));
        }

        private void Server_CatListReceivedEvent(List<Category> catList)
        {
            catListsReceived++;
            allCatList.AddRange(catList);
            if (catListsReceived >= server.clientList.Count)
            {
                server.SendToAll(MessageToolkit.CreateMessage(Message.CatList, MessageToolkit.ConvertFromCatList(allCatList)));
            }
        }

        private void Server_ClientConnectedEvent()
        {
            server.SendToAll(MessageToolkit.CreateMessage(Message.ConnectionCount, server.clientList.Count));
        }
    }
}
