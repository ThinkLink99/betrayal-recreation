using System;
using System.Linq;

namespace betrayal_recreation_shared
{
    public class MovePlayerFloorAction : IRoomAction
    {
        Room.Floors _targetFloor;
        RoomEventTriggers[] _roomTriggers;

        public MovePlayerFloorAction(Room.Floors targetFloor, params RoomEventTriggers[] roomTriggers)
        {
            _targetFloor = targetFloor;
            _roomTriggers = roomTriggers;
        }

        public RoomEventTriggers[] RoomTriggers => _roomTriggers;

        public bool ContainsTrigger(RoomEventTriggers trigger)
        {
            if (RoomTriggers == null) return false;
            if (RoomTriggers.Contains(trigger))
            {
                return true;
            }
            else { return false; }
        }

        public void Run(Player player)
        {
            Console.WriteLine($"{player.Name} has moved to {_targetFloor} floor");
            player.CurrentFloor = _targetFloor;
        }
    }
}