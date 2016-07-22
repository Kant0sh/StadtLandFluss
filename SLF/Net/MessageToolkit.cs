using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLF.Net
{
    public static class MessageToolkit
    {

        public static char SPLIT = '%';

        public static string[] GetSplitMessage(string msg)
        {
            string[] splitMsg = msg.Split(SPLIT);
            return splitMsg;
        }

        public static string[] GetMessageArray(string[] splitMsg)
        {
            string[] msgArray = new string[splitMsg.Length - 1];
            for (int i = 1; i < splitMsg.Length; i++)
            {
                msgArray[i - 1] = splitMsg[i];
            }
            return msgArray;
        }

        public static string[] GetMessageArray(string msg)
        {
            return GetMessageArray(GetSplitMessage(msg));
        }

        public static int GetType(string[] splitMsg)
        {
            int id = -1;
            try
            {
                id = Int32.Parse(splitMsg[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return id;
        }

        public static int GetType(string msg)
        {
            return GetType(GetSplitMessage(msg));
        }

        public static string CreateMessage(int type, string msg)
        {
            return type.ToString() + SPLIT + msg;
        }

        public static string CreateMessage(int type, int msgVal)
        {
            return type.ToString() + SPLIT + msgVal.ToString();
        }

    }
}
