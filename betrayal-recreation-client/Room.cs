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
        public enum LevelsEnum
        {
            Basement = 0,
            Ground = 1,
            Upper = 2
        }

        private bool _startingRoom = false;
        private LevelsEnum[] _levels = new LevelsEnum[3];

        public bool StartingRoom { get => _startingRoom; set => _startingRoom = value; }
        public LevelsEnum[] Levels { get { return _levels; } }

        public Room(int id, string name, string description, LevelsEnum[] levels, bool startingRoom) :
            base(id, name, description)
        {
            _levels = levels;
            _startingRoom = startingRoom;
        }
    }
}
