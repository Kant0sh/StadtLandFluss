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

        public static Message GetType(string[] splitMsg)
        {
            return (Message)Enum.Parse(typeof(Message), splitMsg[0]);
        }

        public static Message GetType(string msg)
        {
            return GetType(GetSplitMessage(msg));
        }

        public static string CreateMessage(Message type, string msg)
        {
            return type.ToString() + SPLIT + msg;
        }

        public static string CreateMessage(Message type, int msgVal)
        {
            return type.ToString() + SPLIT + msgVal.ToString();
        }

        public static string CreateMessage(Message type, bool msgBool)
        {
            int i = 0;
            if (msgBool) i = 1;
            return type.ToString() + SPLIT + i.ToString();
        }

        public static string ConvertFromCatList(List<Category> catList)
        {
            string s = string.Empty;
            for(int i = 0; i < catList.Count; i++)
            {
                s += catList[i].getCatText();
                if (i != catList.Count - 1) s += SPLIT.ToString();
            }
            return s;
        }

        public static List<Category> ConvertToCatList(string msg)
        {
            string[] msgArray = GetMessageArray(msg);
            List<Category> catList = new List<Category>();
            foreach(string s in msgArray)
            {
                catList.Add(new Category(s));
            }
            return catList;
        }

        public static bool GetSingleBool(string msg)
        {
            if (GetMessageArray(msg)[0] == "1")
            {
                return true;
            }
            return false;
        }

        public static int GetSingleInt(string msg)
        {
            return Int32.Parse(GetMessageArray(msg)[0]);
        }

    }
}
