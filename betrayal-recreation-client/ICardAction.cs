namespace betrayal_recreation_shared
{
    public enum CardEventTriggers { PICKUP, LOST, USE }
    public interface ICardAction
    {
        CardEventTriggers[] CardTriggers { get; }
        void Run (Player player);
        bool ContainsTrigger(CardEventTriggers trigger);
    }
}