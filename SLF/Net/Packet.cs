using SLF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest2
{
    public class Packet
    {

        public int methodIndex;
        public string message;

        public Packet(int methodIndex, string message)
        {
            this.methodIndex = methodIndex;
            this.message = message;
        }

    }
}
