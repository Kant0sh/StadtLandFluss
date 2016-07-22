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

        public ServerData(Server server)
        {
            this.server = server;
            server.ClientConnectedEvent += Server_ClientConnectedEvent;
        }

        private void Server_ClientConnectedEvent()
        {
            server.SendToAll(MessageToolkit.CreateMessage((int)Message.ConnectionCount, server.clientList.Count));
        }
    }
}
