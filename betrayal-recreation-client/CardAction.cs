namespace betrayal_recreation_shared
{
    public enum CardEventTriggers { PICKUP, LOST, USE }
    public abstract class CardAction : BasicObjectInformation
    {
        CardEventTriggers trigger;
        public CardAction(int id, string name, string description, CardEventTriggers trigger)
            : base(id, name, description)
        {
            this.trigger = trigger;
        }

        public abstract void Run (Player player);
    }
}