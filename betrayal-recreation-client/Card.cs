using System.Drawing;

namespace betrayal_recreation_shared
{
    public enum CardType { Event, Item, Omen, None }
    public abstract class Card : BasicObjectInformation
    {
        CardType _type;

        public Card(int id, string name, string desc, CardType cardType) :
            base (id, name, desc)
        {
            _type = cardType;
        }
    }

    public class Omen : Card
    {
        public bool HasBeenUsed { get; set; }
        public bool CanDrop { get; set; }
        
        public Omen(int id, string name, string desc, bool canDrop) :
            base(id, name, desc, CardType.Omen)
        {
            CanDrop = canDrop;
        }
    }
    public class Event : Card
    {
        public Event(int id, string name, string desc) :
        base(id, name, desc, CardType.Event)
        {

        }
    }
    public class Item : Card
    {
        public Item(int id, string name, string desc) :
        base(id, name, desc, CardType.Item)
        {

        }
    }
}
