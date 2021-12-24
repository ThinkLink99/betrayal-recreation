using System;
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

        public Session (List<Room> rooms)
        {
            _characters = new List<Character>();
            _players = new List<Player>();

            _roomDeck = new Deck<Room>(rooms.Where(r => !r.StartingRoom).ToList());

            _floors = new List<Grid>()
            {
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
                new Grid(1, FLOOR_WIDTH, FLOOR_HEIGHT),
            };

            SetStartingRooms(rooms.Where(r => r.StartingRoom).ToArray());
        }

        public List<Grid> Floors { get => _floors; set => _floors = value; }

        public void AddCharacter (Character character)
        {
            _characters.Add(character);
        }
        public void LogCharacters ()
        {
            Console.WriteLine("-- Characters --\n");
            foreach (Character c in _characters)
            {
                Console.WriteLine($"{c.Name} ({c.Color} {c.Side})\n");
            }
        }

        public Room DrawRoom ()
        {
            var r = _roomDeck.Draw();
            GameEvents.RoomDrawn(r);
            return r;
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
