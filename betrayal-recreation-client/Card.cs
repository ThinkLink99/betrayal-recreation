using System.Drawing;

namespace CSOTH_API
{
    public class Card
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }

        public Card(int id, string name, string desc)
        {
            ID = id;
            Name = name;
            Description = desc;
        }
    }

    public class Omen : Card
    {
        public bool HasBeenUsed { get; set; }
        public bool CanBeDropped { get; set; }
        
        // Omen collect
        public string CollectType { get; set; }
        public string CollectTarget { get; set; }
        public int CollectAmount { get; set; }

        // Omen use
        public string UseType { get; set; }
        public string UseTarget { get; set; }
        public int UseAmount { get; set; }

        public Omen(int id, string name, string desc, bool dropped, string coltype, string coltrgt, int colamnt, string usetype, string usetrgt, int useamnt) :
            base(id, name, desc)
        {
            CanBeDropped = dropped;

            CollectType = coltype;
            CollectTarget = coltrgt;
            CollectAmount = colamnt;

            UseType = usetype;
            UseTarget = usetrgt;
            UseAmount = useamnt;
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
