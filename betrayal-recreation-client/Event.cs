namespace betrayal_recreation_shared
{
    public class Event : Card
    {
        public Event(int id, string name, string desc, params ICardAction[] cardActions) :
        base(id, name, desc, CardType.Event)
        {
            base._cardActions = new System.Collections.Generic.List<ICardAction>(cardActions);
        }

        public void OnDraw (Player player)
        {
            foreach (ICardAction action in _cardActions)
            {
                if (action.ContainsTrigger(CardEventTriggers.PICKUP))
                    action.Run(player);
            }
        }
    }
}
