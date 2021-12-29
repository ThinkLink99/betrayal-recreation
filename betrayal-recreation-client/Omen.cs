namespace betrayal_recreation_shared
{
    public class Omen : Card
    {
        public Omen(int id, string name, string desc, bool canDrop, bool canTrade, bool canSteal, params CardAction[] cardActions) :
            base(id, name, desc, CardType.Omen, canDrop, canTrade, canSteal)
        {
            base._cardActions = new System.Collections.Generic.List<CardAction>(cardActions);
        }
    }
}
