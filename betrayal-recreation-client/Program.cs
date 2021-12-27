using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_client
{
    static class Program
    {
        static Session session;

        public static void Main ()
        {
            GameEvents.onRoomDrawn += delegate (Room r) { Console.WriteLine($"Room drawn was: {r.Name}"); };
            List<Room> rooms = new List<Room>();
            // Starting Rooms
            rooms.Add(new Room(0, "Grand Entrance", "Grand Entrance", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, false, true, false, false }));
            rooms.Add(new Room(1, "Foyer", "Foyer", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, true, true, false, false }));
            rooms.Add(new Room(2, "Downstairs Landing", "Downstairs Landing", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { false, true, true, true, true, false }));
            rooms.Add(new Room(3, "Upper Landing", "Upper Landing", CardType.None, new Room.Floors[] { Room.Floors.Upper }, true, new bool[] { true, true, true, true, true, false, false, true }));
            rooms.Add(new Room(4, "Basement Landing", "Basement Landing", CardType.None, new Room.Floors[] { Room.Floors.Basement }, true, new bool[] { true, true, true, true, false, false }));
            // Non-starting Rooms
            rooms.Add(new Room(5, "Hallway", "Hallway", CardType.None, new Room.Floors[] { Room.Floors.Basement, Room.Floors.Ground, Room.Floors.Upper }, false, new bool[] { true, true, true, true }));
            rooms.Add(new Room(6, "Creaky Hallway", "Hallway", CardType.None, new Room.Floors[] { Room.Floors.Basement, Room.Floors.Ground, Room.Floors.Upper }, false, new bool[] { true, true, true, true }));
            rooms.Add(new SpecialRoom(7, "Coal Chute", "Coal Chute", new Room.Floors[] { Room.Floors.Ground, Room.Floors.Upper }, false, new bool[] { false, false, true, false, false, false }, new MovePlayerRoomAction(rooms[4]), new MovePlayerFloorAction(Room.Floors.Basement)));
            rooms.Add(new Room(8, "Master Bedroom", "Master Bedroom", CardType.Event, new Room.Floors[] { Room.Floors.Ground }, false, new bool[] { false, true, true, true }));
            rooms.Add(new Room(9, "Furnace Room", "Furnace Room", CardType.Omen, new Room.Floors[] { Room.Floors.Basement }, false, new bool[] { false, true, true, true, false, false }));

            rooms[0].AdjacentRooms[(int)Room.Directions.North] = rooms[1];
            rooms[1].AdjacentRooms[(int)Room.Directions.South] = rooms[0];
            rooms[1].AdjacentRooms[(int)Room.Directions.North] = rooms[2];
            rooms[2].AdjacentRooms[(int)Room.Directions.South] = rooms[1];
            rooms[2].AdjacentRooms[(int)Room.Directions.Upstairs] = rooms[3];
            rooms[3].AdjacentRooms[(int)Room.Directions.Downstairs] = rooms[2];

            session = new Session(rooms,
            new List<Player>()
            {
                new Player(0, "ThinkLink99"),
                new Player(1, "MegaDillyBarr")
            },
            new List<Character>()
            {
                new Character(0, "Trey", "Mar 1 1999", "blue", new int[] { }, new int[] { }, new int[] { }, new int[] { 4 }, 0, 0, 0, 0),
                new Character(1, "Dylan", "Jun 4 2002", "purple", new int[] { }, new int[] { }, new int[] { }, new int[] { }, 0, 0, 0, 0)
            });

            session.AssignCharacterToPlayer(0, 0);
            session.AssignCharacterToPlayer(1, 1);

            session.Players[0].SetCurrentRoom(session.StartingRooms[0]);
            session.Players[1].SetCurrentRoom(session.StartingRooms[0]);

            session.StartTurn();
            session.LogTurns();
            Console.WriteLine($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();

            session.MoveCurrentPlayer(Room.Directions.North);

            Console.WriteLine($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();

            session.MoveCurrentPlayer(Room.Directions.North);

            Console.WriteLine($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();

            session.MoveCurrentPlayer(Room.Directions.Upstairs);

            Console.WriteLine($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();

            session.MoveCurrentPlayer(Room.Directions.North);

            Console.WriteLine($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();

            session.EndTurn();
            session.LogTurns();

            Console.ReadKey();
        }
    }
}
