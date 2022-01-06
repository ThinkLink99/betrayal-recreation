using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public enum CardType { Event, Item, Omen, None }
    public abstract class Card : PlayerItem
    {
        CardType _type;
        protected List<ICardAction> _cardActions;

        public CardType Type { get => _type; }

        public Card(int id, string name, string desc, CardType cardType, bool canDrop = true, bool canTrade = true, bool canSteal = true) :
            base (id, name, desc, canDrop, canTrade, canSteal)
        {
            _type = cardType;
        }
    }
}
