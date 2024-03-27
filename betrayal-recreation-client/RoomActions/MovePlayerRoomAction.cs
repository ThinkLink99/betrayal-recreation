using System;

namespace betrayal_recreation_shared
{
    public class MovePlayerRoomAction : IRoomAction
    {
        Room _targetRoom;
        public MovePlayerRoomAction(Room targetRoom) 
        {
            _targetRoom = targetRoom;
        }

        public CardEventTriggers CardTrigger => throw new NotImplementedException();
        public RoomEventTriggers RoomTrigger => RoomEventTriggers.ROOM_ENTER;

        public RoomEventTriggers[] RoomTriggers => throw new NotImplementedException();

        public bool ContainsTrigger(RoomEventTriggers trigger)
        {
            throw new NotImplementedException();
        }

        public void Run(Player player)
        {
            Console.WriteLine($"{player.Name} has moved to {_targetRoom.Name}");
            player.SetCurrentRoom (_targetRoom);
        }
    }
}