﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace betrayal_recreation_client
{
    public enum MoveStatus { MOVED, NEED_ROOM, NO_DOOR, OUT_OF_SPEED }
    public class Player : BasicObjectInformation
    {
        Character _character;
        Room.Floors _currentFloor;
        Room _currentRoom;
        int _currentSpeed = 0;

        public Character Character { get => _character; set => _character = value; }
        public Room.Floors CurrentFloor { get => _currentFloor; set => _currentFloor = value; }

        public Player(int id, string name, Character character)
            : base(id, name, "")
        {
            Name = name;
            _character = character;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _currentSpeed = 3;
        }
        public Player(int id, string name)
            : base(id, name, "")
        {
            Name = name;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _currentSpeed = 3;
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
            if (_currentSpeed == 0) return MoveStatus.OUT_OF_SPEED;

            if (_currentRoom.HasDoors[(int)direction])
            {
                if (_currentRoom.AdjacentRooms[(int)direction] != null)
                {
                    _currentRoom = _currentRoom.AdjacentRooms[(int)direction];

                    _currentSpeed--;
                    return MoveStatus.MOVED;
                }
                else return MoveStatus.NEED_ROOM;
            }
            else return MoveStatus.NO_DOOR;
        }
    }
}
