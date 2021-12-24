using System.Drawing;

namespace betrayal_recreation_client
{
    public class Card : BasicObjectInformation
    {
        public Card(int id, string name, string desc) :
            base (id, name, desc)
        {

        }
    }

    public class Omen : Card
    {
        public bool HasBeenUsed { get; set; }
        public bool CanDrop { get; set; }
        
        public Omen(int id, string name, string desc, bool canDrop) :
            base(id, name, desc)
        {
            CanDrop = canDrop;
        }
    }
    public class Event : Card
    {
        public Event(int id, string name, string desc) :
        base(id, name, desc)
        {

        }
    }
    public class Item : Card
    {
        public Item(int id, string name, string desc) :
        base(id, name, desc)
        {

        }
    }
}
