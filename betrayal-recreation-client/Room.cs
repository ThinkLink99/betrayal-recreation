using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public class Room : BasicObjectInformation
    {
        public enum Directions
        {
            North = 0,
            South = 2,
            East = 1,
            West = 3,
            Upstairs = 4,
            Downstairs = 5
        }
        public enum Floors
        {
            Basement = 0,
            Ground = 1,
            Upper = 2
        }

        private List<Room> _adjacentRooms = new List<Room>(6);
        private CardType _cardInRoom;
        private bool[] _hasDoors = new bool[6];
        private Floors[] _levels = new Floors[3];
        private bool _startingRoom = false;

        public List<Room> AdjacentRooms { get => _adjacentRooms; set => _adjacentRooms = value; }
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
            _hasDoors = hasDoors ?? new bool[6];
        }

        public virtual void RoomEnter (Player player)
        {
            if (player == null) return;
        }
        public virtual void RoomLeave(Player player)
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
        public List<IRoomAction> _roomActions;

        public SpecialRoom(int id, string name, string description, Floors[] levels, bool startingRoom, bool[] hasDoors, params IRoomAction[] roomActions)
            : base(id, name, description, CardType.None, levels, startingRoom, hasDoors)
        {
            _roomActions = new List<IRoomAction>(roomActions);
        }

        public override void RoomEnter(Player player)
        {
            for (int i = 0; i < _roomActions.Count; i++)
            {
                if (_roomActions[i].RunTime1 == IRoomAction.RoomTriggers.ROOM_ENTER)
                    _roomActions[i].Run(player);
            }
        }
        public override void RoomLeave(Player player)
        {
            for (int i = 0; i < _roomActions.Count; i++)
            {
                if (_roomActions[i].RunTime1 == IRoomAction.RoomTriggers.ROOM_LEAVE)
                    _roomActions[i].Run(player);
            }
        }
    }
}