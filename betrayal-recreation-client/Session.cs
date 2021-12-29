using System;
using System.Collections.Generic;
using System.Linq;

namespace betrayal_recreation_shared
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

        public static event Action<Room, Player> onRoomEnter;
        public static void RoomEnter (Room room, Player player)
        {
            onRoomEnter?.Invoke(room, player);
        }

        public static event Action<Room, Player> onRoomLeave;
        public static void RoomLeave(Room room, Player player)
        {
            onRoomLeave?.Invoke(room, player);
        }

        public static event Action<Omen, Player> onOmenPickup;
        public static void OmenPickup(Omen omen, Player player)
        {
            onOmenPickup?.Invoke(omen, player);
        }
        public static event Action<Omen, Player> onOmenLost;
        public static void OmenLost(Omen omen, Player player)
        {
            onOmenLost?.Invoke(omen, player);
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

        private TurnOrder _turnOrder;

        public Session (List<Room> rooms)
        {
            NewSession(rooms);
        }
        public Session(List<Room> rooms, List<Character> characters)
        {
            NewSession(rooms, characters);
        }
        public Session(List<Room> rooms, List<Character> characters, List<Player> players)
        {
            NewSession(rooms, characters, players);
        }

        public List<Character> Characters { get => _characters; set => _characters = value; }
        public List<Grid> Floors { get => _floors; set => _floors = value; }
        public TurnOrder TurnOrder { get => _turnOrder; set => _turnOrder = value; }
        public List<Player> Players { get => _players; set => _players = value; }
        public List<Room> StartingRooms { get => _startingRooms; set => _startingRooms = value; }

        private void NewSession (List<Room> rooms)
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

            GameEvents.onRoomEnter += delegate (Room r, Player p) { r.RoomEnter(p); };
            GameEvents.onRoomLeave += delegate (Room r, Player p) { r.RoomLeave(p); };
        }
        private void NewSession(List<Room> rooms, List<Character> characters)
        {
            NewSession(rooms);

            _characters = characters;
        }
        private void NewSession(List<Room> rooms, List<Character> characters, List<Player> players)
        {
            NewSession(rooms, characters);

            _players = players;
            _turnOrder = new TurnOrder(players);
        }

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
                _roomDeck.Discard(r);
                return DrawRoom(currentFloor);
            }
        }
        public void MoveCurrentPlayer (Room.Directions direction)
        {
            int x = 0;
            int y = 0;
            Room.Directions opposite = direction;

            switch (direction)
            {
                case Room.Directions.North:
                    opposite = Room.Directions.South;
                    y = -1;
                    break;
                case Room.Directions.South:
                    opposite = Room.Directions.North;
                    y = 1;
                    break;
                case Room.Directions.West:
                    opposite = Room.Directions.East;
                    x = -1;
                    break;
                case Room.Directions.East:
                    opposite = Room.Directions.West;
                    x = 1;
                    break;
            }

            var move_status = TurnOrder.CurrentPlayer.Move(direction);

            if (move_status == MoveStatus.NEED_ROOM)
            {
                // Draw Room Tile
                var room = DrawRoom(TurnOrder.CurrentPlayer.CurrentFloor);
                bool cardinRoom = room.CardInRoom != CardType.None;

                room.AdjacentRooms[(int)opposite] = TurnOrder.CurrentPlayer.GetCurrentRoom();
                TurnOrder.CurrentPlayer
                    .GetCurrentRoom()
                    .AdjacentRooms[(int)direction] = room;

                _floors[(int)TurnOrder.CurrentPlayer.CurrentFloor]
                    .Find(
                        TurnOrder.CurrentPlayer.GetCurrentRoom(), 
                        out int gX, out int gY);

                if (_floors[(int)TurnOrder.CurrentPlayer.CurrentFloor].NullOrEmptyAt (gX + x, gY + y))
                {
                    // we need to check for a room that does exist in the spot we are placing a tile on
                    // that doesn't connect to the room we are coming from

                    _floors[(int)TurnOrder.CurrentPlayer.CurrentFloor]
                        .SetCell(gX + x, gY + y, room);

                    MoveCurrentPlayer(direction);
                    if (cardinRoom)
                    {
                        TurnOrder.CurrentPlayer.SpeedRemaining = 0;
                        Card card = null;
                        switch (room.CardInRoom)
                        {
                            case CardType.Omen:
                                card = _omenDeck.Draw();

                                GameEvents.OmenPickup((Omen)card, TurnOrder.CurrentPlayer);
                                break;
                            case CardType.Item:
                                card = _itemDeck.Draw();
                                break;
                            case CardType.Event:
                                card = _eventDeck.Draw();
                                break;
                        }

                        TurnOrder.CurrentPlayer.AddCard(card);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Room already exists at this spot");
                }
            }
            else if (move_status == MoveStatus.NO_DOOR)
            {
                // alert the user there was no connected door in this direction
                Console.WriteLine("ERROR: No Connecting Doorway.");
            }
            else if (move_status == MoveStatus.OUT_OF_SPEED)
            {
                // alert the user there was no connected door in this direction
                Console.WriteLine("ERROR: Ran out of speed");
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
        public void LogTurns ()
        {
            Console.WriteLine($"Current Turn: {TurnOrder.CurrentPlayer.Name}");
            Console.WriteLine($"Next Turn: {TurnOrder.NextPlayer.Name}");
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

        public void StartTurn ()
        {
            TurnOrder.CurrentPlayer.StartTurn();
        }
        public void EndTurn ()
        {
            TurnOrder.EndTurn();
        }
    }
}
