namespace betrayal_recreation_shared
{
    public class Item : Card
    {
        public Item(int id, string name, string desc) :
        base(id, name, desc, CardType.Item)
        {

        }
    }
}
