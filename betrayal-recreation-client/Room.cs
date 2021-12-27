using System;
using System.Collections.Generic;

namespace betrayal_recreation_client
{
    public class Room : BasicObjectInformation
    {
        public enum Directions
        {
            North = 0,
            South = 2,
            East = 1,
            West = 3
        }
        public enum Floors
        {
            Basement = 0,
            Ground = 1,
            Upper = 2
        }

        private Room[] _adjacentRooms = new Room[4];
        private CardType _cardInRoom;
        private bool[] _hasDoors = new bool[4];
        private Floors[] _levels = new Floors[3];
        private bool _startingRoom = false;

        public Room[] AdjacentRooms { get => _adjacentRooms; set => _adjacentRooms = value; }
        public CardType CardInRoom { get => _cardInRoom; }
        public bool[] HasDoors { get => _hasDoors; set => _hasDoors = value; }
        public Floors[] Levels { get { return _levels; } }
        public bool StartingRoom { get => _startingRoom; set => _startingRoom = value; }

        public Room(int id, string name, string description, CardType cardInRoom, Floors[] levels, bool startingRoom, bool[] hasDoors = null) :
            base(id, name, description)
        {
            _cardInRoom = cardInRoom;
            _levels = levels;
            _startingRoom = startingRoom;
            _hasDoors = hasDoors ?? new bool[4];
        }

        public virtual void RoomEnter (Player player)
        {
            if (player == null) return;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Room)) return false;

            var b = (Room)obj;

            if (b == null) return false;
            if (this.ID == b.ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Room Empty { get => new Room(-1, "EMPTY", "EMPTY", CardType.None, null, false, new bool[] { false, false, false, false }); }
    }

    public class SpecialRoom : Room
    {
        public List<RoomAction> _roomActions;

        public SpecialRoom(int id, string name, string description, Floors[] levels, bool startingRoom, bool[] hasDoors, params RoomAction[] roomActions)
            : base(id, name, description, CardType.None, levels, startingRoom, hasDoors)
        {
            _roomActions = new List<RoomAction>(roomActions);
        }

        public override void RoomEnter(Player player)
        {
            for (int i = 0; i < _roomActions.Count; i++)
                _roomActions[i].Run(player);
        }
    }

    public abstract class RoomAction : BasicObjectInformation
    {
        protected RoomAction(int id, string name, string description) 
            : base(id, name, description)
        {
        }

        public abstract void Run(Player player);
    }

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