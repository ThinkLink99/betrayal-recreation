using System;

namespace betrayal_recreation_shared
{
    public class MovePlayerRoomAction : IRoomAction, ICardAction
    {
        Room _targetRoom;
        public MovePlayerRoomAction(Room targetRoom) 
        {
            _targetRoom = targetRoom;
        }

        public CardEventTriggers CardTrigger => throw new NotImplementedException();
        public RoomEventTriggers RoomTrigger => RoomEventTriggers.ROOM_ENTER;

        public void Run(Player player)
        {
            Console.WriteLine($"{player.Name} has moved to {_targetRoom.Name}");
            player.SetCurrentRoom (_targetRoom);
        }
    }
}