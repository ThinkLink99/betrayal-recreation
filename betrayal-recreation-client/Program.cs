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

            session = new Session(new List<Room>()
            {
                // Starting Rooms
                new Room(1, "Grand Entrance", "Grand Entrance", new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, false, true }),
                new Room(2, "Foyer", "Foyer", new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, true, true }),
                new Room(3, "Downstairs Landing", "Downstairs Landing", new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { false, true, true, true }),
                new Room(4, "Upper Landing", "Upper Landing", new Room.Floors[] { Room.Floors.Upper }, true, new bool[] { true, true, true, true }),
                new Room(5, "Basement Landing", "Basement Landing", new Room.Floors[] { Room.Floors.Basement }, true, new bool[] { true, true, true, true }),

                // Non-starting Rooms
                new Room(6, "Hallway", "Hallway", new Room.Floors[] { Room.Floors.Basement, Room.Floors.Ground, Room.Floors.Upper }, false),
                new Room(7, "Creaky Hallway", "Hallway", new Room.Floors[] { Room.Floors.Basement, Room.Floors.Ground, Room.Floors.Upper }, false),
                new Room(8, "Coal Chute", "Coal Chute", new Room.Floors[] { Room.Floors.Ground, Room.Floors.Upper }, false),
                new Room(9, "Master Bedroom", "Master Bedroom", new Room.Floors[] { Room.Floors.Upper }, false),
            },
            new List<Player>()
            {
                new Player(0, "ThinkLink99"),
                new Player(1, "MegaDillyBarr")
            },
            new List<Character>()
            {
                new Character(0, "Trey", "Mar 1 1999", "blue", new int[] { }, new int[] { }, new int[] { }, new int[] { }, 0, 0, 0, 0),
                new Character(1, "Dylan", "Jun 4 2002", "purple", new int[] { }, new int[] { }, new int[] { }, new int[] { }, 0, 0, 0, 0)
            });

            session.AssignCharacterToPlayer(0, 0);
            session.AssignCharacterToPlayer(1, 1);

            session.Players[0].SetCurrentRoom(session.StartingRooms[0]);
            session.Players[1].SetCurrentRoom(session.StartingRooms[0]);

            Console.WriteLine($"Current Turn: {session.TurnOrder.CurrentPlayer.Name}");
            Console.WriteLine($"Next Turn: {session.TurnOrder.NextPlayer.Name}");

            //var room = session.DrawRoom(session.TurnOrder.CurrentPlayer.CurrentFloor);
            //session.PlaceRoomOnFloor(session.TurnOrder.CurrentPlayer.CurrentFloor, room, 4, 5);

            session.LogFloors();

            Console.ReadKey();
        }
    }
}
