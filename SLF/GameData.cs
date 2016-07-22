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

        public GameData(Client client)
        {
            this.client = client;
            client.ConnectionCountEvent += Client_ConnectionCountEvent;
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
            Console.WriteLine("#####   " + i);
        }
    }
}
