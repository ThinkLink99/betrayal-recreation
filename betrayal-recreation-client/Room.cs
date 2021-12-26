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
        public SpecialRoom(int id, string name, string description, Floors[] levels, bool startingRoom, bool[] hasDoors = null) 
            : base(id, name, description, CardType.None, levels, startingRoom, hasDoors)
        {
        }
    }
}