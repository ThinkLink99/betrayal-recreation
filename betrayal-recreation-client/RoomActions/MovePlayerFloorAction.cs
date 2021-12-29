using System;

namespace betrayal_recreation_shared
{
    public class MovePlayerFloorAction : RoomAction
    {
        Room.Floors _targetFloor;
        public MovePlayerFloorAction(Room.Floors targetFloor)
            : base(0, "Move Player", "Move player to target room")
        {
            _targetFloor = targetFloor;
        }

        public override void Run(Player player)
        {
            Console.WriteLine($"{player.Name} has moved to {_targetFloor} floor");
            player.CurrentFloor = _targetFloor;
        }
    }
}