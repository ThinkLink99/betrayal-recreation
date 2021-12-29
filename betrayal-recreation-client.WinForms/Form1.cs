using betrayal_recreation_shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace betrayal_recreation_client.WinForms
{
    public partial class Form1 : Form
    {
        Session session;

        public Form1()
        {
            InitializeComponent();

            NewGame();
        }


        public void NewGame ()
        {
            Logger.Initialize(ConsoleColor.Green);

            GameEvents.onRoomDrawn += delegate (Room r) { Console.WriteLine($"Room drawn was: {r.Name}"); };
            List<Room> rooms = new List<Room>();
            // Starting Rooms
            rooms.Add(new Room(0, "Grand Entrance", "Grand Entrance", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, false, true, false, false }));
            rooms.Add(new Room(1, "Foyer", "Foyer", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { true, true, true, true, false, false }));
            rooms.Add(new Room(2, "Downstairs Landing", "Downstairs Landing", CardType.None, new Room.Floors[] { Room.Floors.Ground }, true, new bool[] { false, true, true, true, true, false }));
            rooms.Add(new Room(3, "Upper Landing", "Upper Landing", CardType.None, new Room.Floors[] { Room.Floors.Upper }, true, new bool[] { true, true, true, true, false, true }));
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
            lblCurrentTurn.Text = "Current Turn: " + session.TurnOrder.CurrentPlayer.Name;

            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void UpdateStats ()
        {
            string stats = "";
            stats += "Current Floor: " + session.TurnOrder.CurrentPlayer.CurrentFloor.ToString() + Environment.NewLine;
            stats += "Current Room: " + session.TurnOrder.CurrentPlayer.GetCurrentRoom().Name + Environment.NewLine;
            stats += "Speed Remaining: " + session.TurnOrder.CurrentPlayer.SpeedRemaining.ToString() + Environment.NewLine;
            lblStats.Text = stats;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void btnMoveNorth_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.North);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnMoveWest_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.West);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnMoveSouth_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.South);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnMoveEast_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.East);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnMoveUpstairs_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.Upstairs);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnMoveDownstairs_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.MoveCurrentPlayer(Room.Directions.Downstairs);
            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            Console.Clear();
            session.EndTurn();
            session.LogTurns();

            lblCurrentTurn.Text = "Current Turn: " + session.TurnOrder.CurrentPlayer.Name;

            Logger.LogInfo($"Speed Remaining: {session.TurnOrder.CurrentPlayer.SpeedRemaining}");
            session.LogFloors();
            UpdateStats();
        }
    }
}
