﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace betrayal_recreation_shared
{
    public enum MoveStatus { NO_MOVE, MOVED, CARD_IN_ROOM, NEED_ROOM, NO_DOOR, OUT_OF_SPEED }
    public class Player : BasicObjectInformation
    {
        Character _character;
        Room.Floors _currentFloor;
        Room _currentRoom;
        int _speedRemaining = 0;

        public Character Character { get => _character; set => _character = value; }
        public Room.Floors CurrentFloor { get => _currentFloor; set => _currentFloor = value; }
        public int SpeedRemaining { get => _speedRemaining; set => _speedRemaining = value; }

        public Player(int id, string name, Character character)
            : base(id, name, "")
        {
            Name = name;
            _character = character;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _speedRemaining = 3;
        }
        public Player(int id, string name)
            : base(id, name, "")
        {
            Name = name;
            _currentFloor = Room.Floors.Ground;
            _currentRoom = null;
            _speedRemaining = 3;
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
    }
}
