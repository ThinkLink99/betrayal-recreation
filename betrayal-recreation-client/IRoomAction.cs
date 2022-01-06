namespace betrayal_recreation_shared
{
    public enum RoomEventTriggers { ROOM_ENTER, ROOM_CROSS, ROOM_LEAVE }
    public interface IRoomAction
    {
        RoomEventTriggers[] RoomTriggers { get; }
        void Run(Player player);
        bool ContainsTrigger(RoomEventTriggers trigger);
    }
}