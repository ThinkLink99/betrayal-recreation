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

            _floors[(int)Room.LevelsEnum.Ground].SetCell(FLOOR_WIDTH / 2, (FLOOR_HEIGHT / 2) - 1, rooms[0]); 
            _floors[(int)Room.LevelsEnum.Ground].SetCell(FLOOR_WIDTH / 2, (FLOOR_HEIGHT / 2), rooms[1]); 
            _floors[(int)Room.LevelsEnum.Ground].SetCell(FLOOR_WIDTH / 2, (FLOOR_HEIGHT / 2) + 1, rooms[2]);
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
    }
}
