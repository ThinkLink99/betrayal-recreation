using System.Collections.Generic;
using Newtonsoft.Json;

namespace betrayal_recreation_client
{
    public class Player : BasicObjectInformation
    {
        Character _character;
        Room.Floors _currentFloor;
        Room _currentRoom;

        public Character Character { get => _character; set => _character = value; }
        public Room.Floors CurrentFloor { get => _currentFloor; set => _currentFloor = value; }

        public Player(int id, string name, Character character)
            : base(id, name, "")
        {
            Name = name;
            _character = character;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
        }
        public Player(int id, string name)
            : base(id, name, "")
        {
            Name = name;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
        }

        public Room GetCurrentRoom ()
        {
            if (_currentRoom == null) return null;
            return _currentRoom;
        }
        public void SetCurrentRoom (Room room)
        {
            _currentRoom = room;
        }

        public void Move (Room.Directions direction)
        {

        }
    }
}
