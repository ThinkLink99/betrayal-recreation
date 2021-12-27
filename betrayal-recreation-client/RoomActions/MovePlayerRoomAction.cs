using System;

namespace betrayal_recreation_client
{
    public class MovePlayerRoomAction : RoomAction
    {
        Room _targetRoom;
        public MovePlayerRoomAction(Room targetRoom) 
            : base(0, "Move Player", "Move player to target room")
        {
            _targetRoom = targetRoom;
        }

        public override void Run(Player player)
        {
            Console.WriteLine($"{player.Name} has moved to {_targetRoom.Name}");
            player.SetCurrentRoom (_targetRoom);
        }
    }
}