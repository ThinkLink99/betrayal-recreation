using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public enum CardType { Event, Item, Omen, None }
    public abstract class Card
    {
        CardType _type;
        protected List<ICardAction> _cardActions;

        public Card(int id, string name, string description, CardType cardType, bool canDrop = true, bool canTrade = true, bool canSteal = true)
        {
            _type = cardType;
            Description = description;
        }

        public CardType Type { get => _type; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool CanDrop { get; private set; }
        public bool CanTrade { get; private set; }
        public bool CanSteal { get; private set; }
    }
}
