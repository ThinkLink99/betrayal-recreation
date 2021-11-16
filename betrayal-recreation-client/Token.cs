using System;
using System.Linq;

namespace CSOTH_API
{
    public class Token
    {
        public string Name { get; set; }
        public int Required_Roll { get; set; }
        public string Required_Room { get; set; }
        public string Required_Item { get; set; }

        public Token(string name, int roll, string item, string room)
        {
            Name = name;
            Required_Roll = roll;
            Required_Room = room;
            Required_Item = item;
        }
    }
}
