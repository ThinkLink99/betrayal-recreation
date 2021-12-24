﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace betrayal_recreation_client
{
    public static class GameEvents
    {
        public static event Action onDeckReshuffled;
        public static void DeckReshuffled ()
        {
            onDeckReshuffled?.Invoke();
        }

        public static event Action<Room> onRoomDrawn;
        public static void RoomDrawn (Room room)
        {
            onRoomDrawn?.Invoke(room);
        }

        // TODO: Replace string with Player once player controllers are set up
        public static event Action<Room, string> onRoomLeave;
    }

    public class Session
    {
        const int FLOOR_WIDTH = 10;
        const int FLOOR_HEIGHT = 10;

        private List<Character> _characters;
        private List<Player> _players;

        private Deck<Item> _itemDeck;
        private Deck<Event> _eventDeck;
        private Deck<Omen> _omenDeck;

        private Deck<Room> _roomDeck;

        private List<Room> _startingRooms;

        private List<Grid> _floors;

        private TurnOrder _turnOrder;

        public Session (List<Room> rooms)
        {
            _characters = new List<Character>();
            _players = new List<Player>();

            _roomDeck = new Deck<Room>(rooms.Where(r => !r.StartingRoom).ToList());

            _turnOrder = new TurnOrder(new List<Player>());

            _floors = new List<Grid>()
            {
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
            };

            SetStartingRooms(rooms.Where(r => r.StartingRoom).ToArray());
        }
        public Session(List<Room> rooms, List<Player> players)
        {
            _characters = new List<Character>();
            _players = players;

            _roomDeck = new Deck<Room>(rooms.Where(r => !r.StartingRoom).ToList());

            _turnOrder = new TurnOrder(players);

            _floors = new List<Grid>()
            {
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
            };

            SetStartingRooms(rooms.Where(r => r.StartingRoom).ToArray());
        }
        public Session(List<Room> rooms, List<Player> players, List<Character> characters)
        {
            _characters = characters;
            _players = players;

            _roomDeck = new Deck<Room>(rooms.Where(r => !r.StartingRoom).ToList());

            _turnOrder = new TurnOrder(players);

            _floors = new List<Grid>()
            {
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
            };

            SetStartingRooms(rooms.Where(r => r.StartingRoom).ToArray());
        }

        public List<Character> Characters { get => _characters; set => _characters = value; }
        public List<Grid> Floors { get => _floors; set => _floors = value; }
        public TurnOrder TurnOrder { get => _turnOrder; set => _turnOrder = value; }
        public List<Player> Players { get => _players; set => _players = value; }
        public List<Room> StartingRooms { get => _startingRooms; set => _startingRooms = value; }

        public void AddPlayer (Player player)
        {
            if (player == null) return;

            _players.Add(player);
            _turnOrder.AddPlayer(player);
        }
        public void AddCharacter (Character character)
        {
            _characters.Add(character);
        }

        public void AssignCharacterToPlayer(int playerID, int characterID)
        {
            _players[playerID].Character = _characters[characterID];
        }

        public Room DrawRoom (Room.Floors currentFloor)
        {
            var r = _roomDeck.Draw();

            if (r.Levels.Contains(currentFloor))
            {
                GameEvents.RoomDrawn(r);
                return r;
            }
            else
            {
                return DrawRoom(currentFloor);
            }
        }
        public void PlaceRoomOnFloor (Room.Floors floor, Room room, int x, int y)
        {
            _floors[(int)floor].SetCell(x, y, room);
        }
        public void LogFloors ()
        {
            Console.WriteLine(" == Basement Floor == ");
            Floors[(int)Room.Floors.Basement].Print();

            Console.WriteLine(" == Ground Floor == ");
            Floors[(int)Room.Floors.Ground].Print();

            Console.WriteLine(" == Upper Floor == ");
            Floors[(int)Room.Floors.Upper].Print();
        }

        private void SetStartingRooms (Room[] startingRooms)
        {
            _startingRooms = new List<Room>(startingRooms);

            for (int i = 0; i < startingRooms.Length; i++)
            {
                Room room = startingRooms[i];
                if (room.Levels[0] == Room.Floors.Ground)
                {
                    _floors[(int)room.Levels[0]].SetCell(FLOOR_WIDTH / 2, (FLOOR_HEIGHT / 2) - i, room);
                }
                else
                {
                    _floors[(int)room.Levels[0]].SetCell(FLOOR_WIDTH / 2, (FLOOR_HEIGHT / 2), room);
                }
            }
        }
    }
}
