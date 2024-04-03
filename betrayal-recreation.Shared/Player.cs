using System;
using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public enum PlayerStats { MIGHT, SPEED, SANITY, KNOWLEDGE }
    public enum MoveStatus { NO_MOVE, MOVED, CARD_IN_ROOM, NEED_ROOM, NO_DOOR, OUT_OF_SPEED }
    public class Player : GameObject
    {
        Character _character;
        Room.Floors _currentFloor;
        Room _currentRoom;
        int _speedRemaining = 0;
        int[] _buffs;
        List<Card> _cards;

        public Character Character { get => _character; set => _character = value; }
        public Room.Floors CurrentFloor { get => _currentFloor; set => _currentFloor = value; }
        public int SpeedRemaining { get => _speedRemaining; set => _speedRemaining = value; }

        public int Might => _character.Might + _buffs[(int)PlayerStats.MIGHT];
        public int Speed => _character.Speed + _buffs[(int)PlayerStats.SPEED];
        public int Sanity => _character.Sanity + _buffs[(int)PlayerStats.SANITY];
        public int Knowledge => _character.Knowledge + _buffs[(int)PlayerStats.KNOWLEDGE];

        public Player(int id, string name, Character character)
            : base(id, name, "")
        {
            objectInformation.Name = name;
            _character = character;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _speedRemaining = 3;

            _buffs = new int[4];

            _cards = new List<Card>();
        }
        public Player(int id, string name)
            : base(id, name, "")
        {
            objectInformation.Name = name;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _speedRemaining = 3;

            _buffs = new int[4];

            _cards = new List<Card>();
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

        public MoveStatus Move (Room.Directions direction)
        {
            if (_speedRemaining == 0) return MoveStatus.OUT_OF_SPEED;

            if (direction == Room.Directions.Upstairs && _currentFloor == Room.Floors.Upper) return MoveStatus.NO_MOVE;
            if (direction == Room.Directions.Downstairs && _currentFloor == Room.Floors.Basement) return MoveStatus.NO_MOVE;

            if (_currentRoom.HasDoors[(int)direction])
            {
                if (_currentRoom.AdjacentRooms[(int)direction] != null)
                {
                    if (direction == Room.Directions.Upstairs)
                    {
                        _currentFloor = (Room.Floors)Enum.Parse(typeof(Room.Floors), ((int)_currentFloor + 1).ToString());
                    }
                    if (direction == Room.Directions.Downstairs)
                    {
                        _currentFloor = (Room.Floors)Enum.Parse(typeof(Room.Floors), ((int)_currentFloor - 1).ToString());
                    }

                    _currentRoom = _currentRoom.AdjacentRooms[(int)direction];

                    _speedRemaining--;
                    GameEvents.RoomEnter(_currentRoom, this);

                    return MoveStatus.MOVED;
                }
                else return MoveStatus.NEED_ROOM;
            }
            else return MoveStatus.NO_DOOR;
        }
        public void StartTurn ()
        {
            _speedRemaining = Character.Speed;
        }

        public void AddBuff (PlayerStats stat, int buff)
        {
            _buffs[(int)stat] += buff;
        }
        public void RemoveBuff(PlayerStats stat, int buff)
        {
            _buffs[(int)stat] -= buff;
        }

        public void AddCard (Card card)
        {
            _cards.Add(card);
        }
    }
}
