using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CSOTH_API
{
    public enum RuleSets
    {
        vanilla=0, // the base game
        widows=1, // Adds Attic level and Dumbwaiter transportation on top of base game
        baldurs=2 // Changes Haunt roll rules, adds special player abilities
    }
    public partial class Game
    {
        
        Random rand = new Random();
        private readonly string[] MONTHS = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        #region protected members
        protected int world_size = 10;
        protected int max_omens = 13;
        protected int max_events = 100;
        protected int max_items = 100;
        protected int max_characters = 12;
        protected int max_rooms = 100;
        protected int max_haunts = 70;
        protected int max_players = 6;
        protected int token_size = 32;
        protected int tile_size = 128;
        protected string token_ext = ".png";
        protected string tile_ext = ".png";
        protected string character_card_ext = ".png";
        #endregion
        #region public accessors
        public int MAX_OMENS { get { return max_omens; } set { max_omens = value; } }
        public int MAX_EVENTS { get { return max_events; } set { max_events = value; } }
        public int MAX_ITEMS { get { return max_items; } set { max_items = value; } }
        public int MAX_CHARACTERS { get { return max_characters; } set { max_characters = value; } }
        public int MAX_ROOMS { get { return max_rooms; } set { max_rooms = value; } }
        public int MAX_HAUNTS { get { return max_haunts; } set { max_haunts = value; } }
        public int MAX_PLAYERS { get { return max_players; } set { max_players = value; } }

        public int WORLD_SIZE { get { return world_size; } set { world_size = value; } }
        public int TOKEN_SIZE { get { return token_size; } set { token_size = value; } }
        public int TILE_SIZE { get { return tile_size; } set { tile_size = value; } }

        public string TOKEN_EXTENSION { get { return token_ext; } set { token_ext = value; } }
        public string TILE_EXTENSION { get { return tile_ext ; } set { tile_ext = value; } }
        public string CHARACTER_CARD_EXTENSION { get { return character_card_ext; } set { character_card_ext = value; } }
        #endregion

        string packName = "";
        int CurrentPlayerIndex = 0;
        int TotalOmens = 0;

        public string Output = "";
        public List<Character> AllCharacters;
        public List<Tile> AllTiles;
        List<Tile> DrawableTiles;
        List<Tile> Discard;

        List<Item> ItemDeck { get; set; }
        List<Omen> OmenDeck { get; set; }
        List<Event> EventDeck { get; set; }

        public List<Player> Players;
        public List<string> AvailibleColors;

        public RuleSets RuleSet { get; set; }
        public  List<Tile> BasementTiles { get; set; }
        public List<Tile> GroundTiles { get; set; }
        public List<Tile> UpperTiles { get; set; }

        public bool IsMapChanged { get; set; }
        public bool HauntIsStarted { get; set; }

        public List<Haunt> Haunts { get; set; }
        public Haunt CurrentHaunt { get; set; }
        /// <summary>
        /// Sets up all the required items to play the game.
        /// </summary>
        /// <param name="ResourcePath">
        /// The location of the chosen content pack.
        /// </param>
        public Game(string ResourcePath)
        {
            packName = ResourcePath.Split('\\')[ResourcePath.Split('\\').Length - 1];
            Setup(ResourcePath);
        }

        #region SetupFunctions
        void Setup(string ResourcePath)
        {
            ReadSettings(ResourcePath + "\\Settings.txt");
            try
            {
                PopulateCharacters(ResourcePath + "\\Json\\Characters.json");
                FindCharacterImage(ResourcePath);
            }
            catch (Exception ex) { MessageBox.Show("Characters" + Environment.NewLine + ex.Message); }
            try
            {
                PopulateTiles(ResourcePath + "\\Json\\Rooms.json");
                FindRoomImage(ResourcePath);
                SpawnStartingTiles();
                SetupDrawPile();
            }
            catch (Exception ex) { MessageBox.Show("Rooms" + Environment.NewLine + ex.Message); }
            try
            {
                PopulateCards(ResourcePath + "\\Json\\Items.json", ResourcePath + "\\Json\\Omens.json", ResourcePath + "\\Json\\Events.json");
            }
            catch (Exception ex) { MessageBox.Show("Items" + Environment.NewLine + ex.Message); }
            try
            {
                PopulateHaunts(ResourcePath + "\\Json\\Haunts.json");
            }
            catch (Exception ex) { MessageBox.Show("Haunts" + Environment.NewLine + ex.Message); }
        }

        void ReadSettings (string ResourcePath)
        {
            StreamReader reader = File.OpenText(ResourcePath);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!(line == "" || line.Contains("\n") || line[0] == '#'))
                {
                    string property = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    switch (property)
                    {
                        case "RULESET":
                            try
                            {
                                RuleSet = (RuleSets)Enum.Parse(typeof(RuleSets), value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_OMENS":
                            try
                            {
                                MAX_OMENS = int.Parse(value);
                            } catch(Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_ITEMS":
                            try
                            {
                                MAX_ITEMS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_EVENTS":
                            try
                            {
                                MAX_EVENTS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_CHARACTERS":
                            try
                            {
                                MAX_CHARACTERS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_ROOMS":
                            try
                            {
                                MAX_ROOMS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_HAUNTS":
                            try
                            {
                                MAX_HAUNTS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "MAX_PLAYERS":
                            try
                            {
                                MAX_PLAYERS = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "WORLD_SIZE":
                            try
                            {
                                WORLD_SIZE = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "TOKEN_SIZE":
                            try
                            {
                                TOKEN_SIZE = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                        case "TILE_SIZE":
                            try
                            {
                                TILE_SIZE = int.Parse(value);
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace); }
                            break;
                    }
                }
            }
        }
        void PopulateTiles(string file)
        {
            AllTiles = new List<Tile>();
            AllTiles = LoadTileJson(File.OpenRead(file));

            AllTiles.ForEach(t =>
            {
                if (t.GetAdjacentRoomID(Directions.North) != 0)
                    t.TileToNorth = GetTileByID(t.GetAdjacentRoomID(Directions.North));
                if (t.GetAdjacentRoomID(Directions.South) != 0)
                    t.TileToSouth = GetTileByID(t.GetAdjacentRoomID(Directions.South));
                if (t.GetAdjacentRoomID(Directions.East) != 0)
                    t.TileToEast = GetTileByID(t.GetAdjacentRoomID(Directions.East));
                if (t.GetAdjacentRoomID(Directions.West) != 0)
                    t.TileToWest = GetTileByID(t.GetAdjacentRoomID(Directions.West));
            });

            GroundTiles = new List<Tile>();
            UpperTiles = new List<Tile>();
            BasementTiles = new List<Tile>();
        }
        void SpawnStartingTiles()
        {
            var timesThrough = 0;
            var starting = (from t in AllTiles where t.Starting == true orderby t.ID ascending select t).ToList();
            foreach(var tile in starting)
            {
                tile.X = (WORLD_SIZE / 2) - 1;
                tile.Y = (WORLD_SIZE - 2) - 1;

                if (timesThrough > 0)
                {
                    if (tile.TileToSouth != null)
                        tile.Y = tile.TileToSouth.Y - 1;
                    else if (tile.TileToNorth != null)
                        tile.Y = tile.TileToNorth.Y + 1;
                    else if (tile.TileToEast != null)
                        tile.Y = tile.TileToEast.Y + 1;
                    else if (tile.TileToWest != null)
                        tile.Y = tile.TileToWest.Y - 1;
                }

                if (tile.Levels.Contains(Levels.Ground))
                    GroundTiles.Add(tile);
                else if (tile.Levels.Contains(Levels.Basement))
                    BasementTiles.Add(tile);
                else if (tile.Levels.Contains(Levels.Upper))
                    UpperTiles.Add(tile);

                timesThrough++;
            }
        }
        void SetupDrawPile()
        {
            DrawableTiles = new List<Tile>();
            Discard = new List<Tile>();

            var tiles = (from t in AllTiles where t.Starting == false orderby t.ID ascending select t).ToList();
            DrawableTiles.AddRange(tiles);
        }
        void PopulateCharacters(string file)
        {
            AllCharacters = new List<Character>();
            AllCharacters = LoadCharacterJson(File.OpenRead(file));
        }
        void PopulateCards(string ItemFile, string OmenFile, string EventFile)
        {
            OmenDeck = new List<Omen>();
            ItemDeck = new List<Item>();
            EventDeck = new List<Event>();

            OmenDeck = LoadOmenJson(File.OpenRead(OmenFile));
            ItemDeck = LoadItemJson(File.OpenRead(ItemFile));
            EventDeck = LoadEventJson(File.OpenRead(EventFile));
        }
        void PopulateHaunts(string file)
        {
            Haunts = new List<Haunt>();

            Haunts = LoadHauntJson(File.OpenRead(file));
        }

        /// <summary>
        /// Decides which Player will go first based on their character information
        /// </summary>
        /// <returns>The Player that will go first, if no one is found it will return null</returns>
        public Player SelectFirstPlayer()
        {
            int i = 0;
            foreach (Player player in Players)
            {
                // get the month and day as seperate variables
                var month = player.Details.Birthday.Split('/')[0];
                var day = player.Details.Birthday.Split('/')[1];

                string today = MONTHS[DateTime.Today.Month-1] + " " + DateTime.Today.Day;
                if (player.Details.Birthday.CompareTo(today) < 0) // if Character Birthday is close to or is the current day
                {
                    // Use this character
                    CurrentPlayerIndex = i; // Assign PlayerIndex var the value of i so that the game can go in player order
                    return Players[i];
                }
                else
                {
                    // Go to next player in List
                    i++;
                }
            }
            return null;
        }
        public Player GetCurrentPlayer()
        {
            return Players[CurrentPlayerIndex];
        }
        #endregion

        #region ItemDrawing
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Item DrawItem()
        {
            Item item = null;
            if (ItemDeck.Count != 0)
            {
                item = ItemDeck[rand.Next(ItemDeck.Count)];
                ItemDeck.Remove(item);
            }
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Omen DrawOmen()
        {
            Omen omen = null;
            if (OmenDeck.Count != 0)
            {
                omen = OmenDeck[rand.Next(OmenDeck.Count)];
                OmenDeck.Remove(omen);
            }
            return omen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Event DrawEvent()
        {
            Event evnt = EventDeck[rand.Next(EventDeck.Count)];
            EventDeck.Remove(evnt);
            return evnt;
        }

        /// <summary>
        /// Draws a random tile from DrawableTiles
        /// </summary>
        /// <returns>Tile from DrawableTiles</returns>
        public Tile DrawTile()
        {
            Tile tile;
            Random rand = new Random();

            while (true)
            {
                if (DrawableTiles.Count != 0)
                {
                    int index = rand.Next(DrawableTiles.Count); // Create an index var and assign a random number between 0 and he count of the total tiles
                    tile = DrawableTiles[index]; // Assign the not null tile to a new object
                    if (tile.Levels.Contains(Players[CurrentPlayerIndex].CurrentLevel))
                    {
                        DrawableTiles.Remove(DrawableTiles[index]); // remove tile from list
                        return tile; // return the tile object

                    }
                    else Discard.Add(DrawableTiles[index]);
                }
                else
                {
                    // refill the deck
                    DrawableTiles = Discard;
                    Discard.Clear();
                }
            }
        }

        #endregion

        #region Turns
        public void NextTurn()
        {
            if (CurrentPlayerIndex < Players.Count - 1)
            {
                CurrentPlayerIndex++;
            }
            else CurrentPlayerIndex = 0;

            Players[CurrentPlayerIndex].TurnIsOver = false;
            Players[CurrentPlayerIndex].SpeedRemaining = Players[CurrentPlayerIndex].Details.Speed[Players[CurrentPlayerIndex].CurrSpeed];
        }

        public void AITurn()
        {
            if (!Players[CurrentPlayerIndex].IsHuman)
            {
                // proceed with ai turn
                switch (rand.Next(1))
                {
                    case 0:
                        if (Players[CurrentPlayerIndex].SpeedRemaining > 0)
                        {
                            Directions direction = 0;
                            Enum.TryParse(rand.Next(4).ToString(), out direction);

                            // move forward
                            Output += MovePlayer(Players[CurrentPlayerIndex], direction);
                        }
                        else
                        {
                            Players[CurrentPlayerIndex].TurnIsOver = true;
                        }
                        break;

                    case 1:

                        break;

                    case 2:

                        break;

                    case 3:

                        break;
                }
            }
        }

        public void EndTurn(ref Player currentPlayer)
        {
            currentPlayer.TurnIsOver = true;

            SpecialEndTurnRooms(currentPlayer);
            NextTurn();

            currentPlayer = Players[CurrentPlayerIndex];
        }
        public void SpecialEndTurnRooms(Player currentPlayer)
        {
            switch (currentPlayer.CurrentTile.Special)
            {
                // check if the player ended his turn in any of the four buff rooms
                // Assign the player a bool for all four rooms to keep track of what was recieved
                case "Chapel":
                    GainMental("Sanity", 1);
                    currentPlayer.ChapelBuff = true;
                    break;
                case "Larder":
                    GainPhysical("Might", 1);
                    currentPlayer.LarderBuff = true;
                    break;
                case "Library":
                    GainMental("Knowledge", 1);
                    currentPlayer.LibraryBuff = true;
                    break;
                case "Gymnasium":
                    GainPhysical("Speed", 1);
                    currentPlayer.GymBuff = true;
                    break;

                // Check for the rooms that damage every turn the player ends in that room
                case "Pentagram Chamber":
                    LoseMental("Sanity", 1);
                    break;
            }
        }
        #endregion

        #region Rolls
        /// <summary>
        /// Roll will select a random number between 1 and a Maximum amount determined
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Roll(int max)
        {
            return rand.Next(1, max);
        }
        #endregion
        #region Haunt
        public string HauntRoll(Player currentPlayer, string item, string room)
        {
            string Output = "";
            int r = Roll(12);
            Output = "[Haunt Roll]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") rolled a " + r.ToString() + ". \n";
            if (r < TotalOmens)
            {
                StartHaunt(item, room);
                Output += "[HAUNT]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") HAS STARTED THE HAUNT. \n";
            }
            else
            {
                Output += "[Haunt Roll]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") did not start the Haunt. \n";
            }
            return Output;
        }

        public Haunt FindHaunt (string room, string omen)
        {
            foreach(Haunt haunt in Haunts)
            {
                if (haunt.StartingOmen == omen)
                    foreach (string haunt_room in haunt.StartingRooms)
                        if (haunt_room == room)
                            return haunt;
            }
            return null;
        }
        /// <summary>
        /// StartHaunt will search all available haunts for the one with the designated starting room and starting omen
        /// </summary>
        /// <param name="room"></param>
        /// <param name="omen"></param>
        public string StartHaunt(string room, string omen)
        {
            CurrentHaunt = FindHaunt(room, omen);
            if (CurrentHaunt != null)
            {
                return "[Haunt]: Haunt #" + CurrentHaunt.ID + " has been started! \n";
            }
            else
            {
                return "[ERROR]: THE CORRECT HAUNT COULD NOT BE FOUND. PLEASE REPORT THIS BUG WITH THE ITEM AND ROOM USED TO START HAUNT.\n";
            }
        }

        public string EndHaunt()
        {
            if (CurrentHaunt.HauntCompleted)
            {
                if (CurrentHaunt.ExplorersCompletedHaunt())
                {
                    return "[Explorer Victory]: " + CurrentHaunt.ExplorerVictory + "\n";
                }
                if (CurrentHaunt.TraitorsCompletedHaunt())
                {
                    return "[Explorer Victory]: " + CurrentHaunt.TraitorVictory + "\n";
                }
            }
            return "\n";
        }
        public string PlayersDead()
        {
            string output = "";
            foreach (Player player in CurrentHaunt.Explorers)
            {
                if (player.IsDead)
                {
                    output += "[Death]: Explorer " + player.Details.Name + " is dead. \n";
                }
            }
            foreach (Player player in CurrentHaunt.Traitors)
            {
                if (player.IsDead)
                {
                    output += "[Death]: Traitor " + player.Details.Name + " is dead. \n";
                }
            }
            return output;
        }

        public string AttemptTokenRoll(Player currentPlayer, string choice)
        {
            string output = "";
            switch (choice)
            {
                case "room":
                    foreach (Token token in CurrentHaunt.TokensSetAside)
                    {
                        if (currentPlayer.CurrentTile.Name == token.Required_Room)
                        {
                            // attempt a roll
                            int roll = Roll (currentPlayer.GetTrait(token.Name) * 2);
                            output = CurrentHaunt.TokenRollinRoom(roll, currentPlayer, token);
                        }
                    }
                    break;

                case "item":
                    foreach (Token token in CurrentHaunt.TokensSetAside)
                    {
                        foreach (Card item in currentPlayer.CollectedCards)
                        {
                            if (item.Name == token.Required_Item)
                            {
                                // attempt a roll
                                int roll = Roll(currentPlayer.GetTrait(token.Name) * 2);
                                output = CurrentHaunt.TokenRollOnItem(roll, currentPlayer, token);
                            }
                        }
                    }
                    break;
            }

            return output;
        }
        #endregion

        #region Movement
        /// <summary>
        /// Moves the CurrentPlayer to the selected Tile
        /// </summary>
        /// <param name="currentPlayer">The player who is currently taking their turn</param>
        /// <param name="direction">The direction to move the player</param>
        public string MovePlayer(Player currentPlayer, Directions direction)
        {
            bool TileDrew = false;
            IsMapChanged = true;
            string Output = "";

            if (currentPlayer.SpeedRemaining != 0)
            {
                if (direction == Directions.North)
                {
                    // if there is no room in this direction
                    if (currentPlayer.CurrentTile.TileToNorth == null)
                    {
                        var tile = DrawTile();
                        tile.Place(currentPlayer, direction);
                        TileDrew = true;
                    }

                    if (currentPlayer.CurrentTile.NorthDoor == true && 
                        currentPlayer.CurrentTile.TileToNorth.SouthDoor == true)
                    {
                        currentPlayer.Move(currentPlayer.CurrentTile.TileToNorth);
                        currentPlayer.SpeedRemaining--;
                    }
                }
                else if (direction == Directions.East)
                {
                    if (currentPlayer.CurrentTile.TileToEast == null)
                    {
                        var tile = DrawTile();
                        tile.Place(currentPlayer, direction);
                        TileDrew = true;
                    }
                    if (currentPlayer.CurrentTile.EastDoor == true && 
                        currentPlayer.CurrentTile.TileToEast.WestDoor == true)
                    {
                        currentPlayer.Move(currentPlayer.CurrentTile.TileToEast);
                        currentPlayer.SpeedRemaining--;
                    }
                }
                else if (direction == Directions.South)
                {
                    if (currentPlayer.CurrentTile.TileToSouth == null)
                    {
                        var tile = DrawTile();
                        tile.Place(currentPlayer, direction);
                        TileDrew = true;
                    }
                    if (currentPlayer.CurrentTile.SouthDoor == true && 
                        currentPlayer.CurrentTile.TileToSouth.NorthDoor == true)
                    {
                        currentPlayer.Move(currentPlayer.CurrentTile.TileToSouth);
                        currentPlayer.SpeedRemaining--;
                    }
                }
                else if (direction == Directions.West)
                {
                    if (currentPlayer.CurrentTile.TileToWest == null)
                    {
                        var tile = DrawTile();
                        tile.Place(currentPlayer, direction);
                        TileDrew = true;
                    }
                    if (currentPlayer.CurrentTile.WestDoor == true && 
                        currentPlayer.CurrentTile.TileToWest.EastDoor == true)
                    {
                        currentPlayer.Move(currentPlayer.CurrentTile.TileToWest);
                        currentPlayer.SpeedRemaining--;
                    }
                }
            }
            else
            {
                Output += "[Out of Speed]: " + currentPlayer.Details.Name + " can no longer run. \n";
            }

            // The [Movement] text should not be displayed when a room is first discovered. Discovery implies movement into the room
            if (TileDrew)
            {
                // Give the player the cards or buffs given on discovery
                Output += RoomDiscovery(TileDrew, currentPlayer);

                switch (currentPlayer.CurrentLevel)
                {
                    case Levels.Basement:
                        BasementTiles.Add(currentPlayer.CurrentTile);
                        break;
                    case Levels.Ground:
                        GroundTiles.Add(currentPlayer.CurrentTile);
                        break;
                    case Levels.Upper:
                        UpperTiles.Add(currentPlayer.CurrentTile);
                        break;
                }
            }
            else
                // output the message
                Output += "[Movement]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") moved into the " + currentPlayer.CurrentTile.Name + ". \n";

            // Check for special movement
            Output += SpecialRoomMovement(currentPlayer.CurrentTile.Special);

            // if the player has entered a room that ended his turn
            // complete the room checks for EOT and then advance the player
            if (currentPlayer.TurnIsOver)
                EndTurn(ref currentPlayer);
            return Output;
        }
        string SpecialRoomMovement(string room)
        {
            string Output = "";
            switch (room)
            {
                case "Coal Chute":
                    Output += "[" + Players[CurrentPlayerIndex].CurrentTile.Name + "]: " + Players[CurrentPlayerIndex].Details.Name + " fell to the Basement Landing \n";
                    Players[CurrentPlayerIndex].CurrentLevel = Levels.Basement;
                    Players[CurrentPlayerIndex].CurrentTile = ((from b in BasementTiles where b.Special == "Basement Landing" select b).FirstOrDefault());
                    return Output;

                case "Charred Room":
                    return Output;

                case "Chapel":
                    if (Players[CurrentPlayerIndex].TurnIsOver)
                    {
                        Output += "[" + Players[CurrentPlayerIndex].CurrentTile.Name + "]: " + Players[CurrentPlayerIndex].Details.Name + " ended turn here and gained 1 Sanity. \n";
                        // give the player sanity bonus
                        GainMental("Sanity", 1);
                    }
                    return Output;
                case "Collapsed Room":
                    //if (MessageBox.Show("Would you like to try to cross?", "Collapsed Room", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    //{
                    //    // calculate the speed roll
                    //    int roll = Roll(Players[CurrentPlayerIndex].Details.Speed[Players[CurrentPlayerIndex].CurrSpeed] * 2);

                    //    if (roll < 5)
                    //    {
                    //        // Didn't cross, fall to basement
                    //        // draw new tile

                    //    }
                    //}
                    return Output;
            }
            return Output;
        }
        string RoomDiscovery(bool TileDrew, Player currentPlayer)
        {
            string Output = "";
            if (TileDrew)
            {
                Output += "[Discovery]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") discovered the " + currentPlayer.CurrentTile.Name + ". \n";
                if (currentPlayer.CurrentTile.CardInRoom != "None")
                {
                    currentPlayer.TurnIsOver = true;
                    // give them the card in the room
                    Output += "[New Card]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") gained an " + currentPlayer.CurrentTile.CardInRoom + ". \n";

                    if (currentPlayer.CurrentTile.CardInRoom == "Event")
                    {
                        // read the event card and do as it says
                        Output += "[Event]: ";
                        //Event evnt = DrawEvent();
                        //Output.Text += evnt.Name + " - " + evnt.Description;
                    }
                    if (currentPlayer.CurrentTile.CardInRoom == "Item")
                    {
                        CollectCard(DrawItem(), currentPlayer);
                        if (currentPlayer.CollectedCards[currentPlayer.CollectedCards.Count - 1] != null) { Output += "[Item]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") gained " + currentPlayer.CollectedCards[currentPlayer.CollectedCards.Count - 1].Name + ". \n"; }
                    }
                    if (currentPlayer.CurrentTile.CardInRoom == "Omen")
                    {
                        CollectCard(DrawOmen(), currentPlayer);
                        if (currentPlayer.CollectedCards[currentPlayer.CollectedCards.Count - 1] != null) { Output += "[Omen]: " + currentPlayer.Details.Name + "(" + currentPlayer.Name + ") gained " + currentPlayer.CollectedCards[currentPlayer.CollectedCards.Count - 1].Name + ". \n"; }
                        TotalOmens++;

                        // do a haunt roll now
                        HauntRoll(currentPlayer, currentPlayer.CurrentTile.Name, currentPlayer.CollectedCards[currentPlayer.CollectedCards.Count - 1].Name);
                    }

                    // end turn
                    currentPlayer.TurnIsOver = true;
                }
            }
            return Output;
        }
        #endregion
        #region ItemUsage
        public void CollectCard(Card cardToCollect, Player currentPlayer)
        {
            // put the card into the player inventory
            currentPlayer.CollectedCards.Add(cardToCollect);

            // if the card is an omen
            if (cardToCollect.GetType() == typeof(Omen))
            {
                // parse card to an omen
                // run omen_collection
                Omen_collection((Omen)cardToCollect);
            }

        }
        void Omen_collection (Omen omen)
        {
            // check if the card has a collect type
            if (omen.CollectType != "None")
            {
                // if the card buffs the player
                if (omen.CollectType == "Buff")
                {
                    // check for the target to buff
                    switch (omen.CollectTarget)
                    {
                        case "Might":
                            GainPhysical("Might", omen.CollectAmount);
                            break;
                        case "Speed":
                            GainPhysical("Speed", omen.CollectAmount);
                            break;
                        case "Sanity":
                            GainMental("Sanity", omen.CollectAmount);
                            break;
                        case "Knowledge":
                            GainMental("Knowledge", omen.CollectAmount);
                            break;
                    }
                }

                // if the card debuffs the player
                if (omen.CollectType == "Debuff")
                {
                    // check for the target to debuff
                    switch (omen.CollectTarget)
                    {
                        case "Might":
                            LosePhysical("Might", omen.CollectAmount);
                            break;
                        case "Speed":
                            LosePhysical("Speed", omen.CollectAmount);
                            break;
                        case "Sanity":
                            LoseMental("Sanity", omen.CollectAmount);
                            break;
                        case "Knowledge":
                            LoseMental("Knowledge", omen.CollectAmount);
                            break;
                    }
                }
            }
        }

        public void UseOmen (Omen omenToUse)
        {
            switch (omenToUse.UseType)
            {
                case "Buff": case "Debuff":
                    switch (omenToUse.UseTarget)
                    {
                        case "Might":
                            if (omenToUse.UseType == "Buff") { GainPhysical("Might", omenToUse.UseAmount); }
                            else if (omenToUse.UseType == "Debuff") { }
                            break;
                        case "Speed":
                            if (omenToUse.UseType == "Buff") { GainPhysical("Speed", omenToUse.UseAmount); }
                            else if (omenToUse.UseType == "Debuff") { }
                            break;
                        case "Sanity":
                            if (omenToUse.UseType == "Buff") { GainMental("Sanity", omenToUse.UseAmount); }
                            else if (omenToUse.UseType == "Debuff") { }
                            break;
                        case "Knowledge":
                            if (omenToUse.UseType == "Buff") { GainMental("Knowledge", omenToUse.UseAmount); }
                            else if (omenToUse.UseType == "Debuff") { }
                            break;
                    }
                    break;
            }
        }
        public void UseItem (Item itemToUse)
        {
            
        }
        #endregion

        #region Traits
        /// <summary>
        /// LosePhysical will take a name of a physical trait and an amount to subtract then subtract that amount from the players current trait index
        /// if Players trait value is already 0 and the haunt has not been initiated, the player will not lose anymore of that trait.
        /// </summary>
        /// <param name="trait"></param>
        /// <param name="amount"></param>
        public void LosePhysical(string trait, int amount)
        {
            if (trait == "Might")
            {
                Players[CurrentPlayerIndex].CurrMight -= amount;

                if (!HauntIsStarted)
                    // player cant die until the haunt starts
                    Players[CurrentPlayerIndex].CurrMight = 0;
            }
            if (trait == "Speed")
            {
                Players[CurrentPlayerIndex].CurrSpeed -= amount;

                if (!HauntIsStarted)
                    // player cant die until the haunt starts
                    Players[CurrentPlayerIndex].CurrSpeed = 0;
            }
        }

        public void LoseMental(string trait, int amount)
        {
            if (trait == "Knowledge")
            {
                Players[CurrentPlayerIndex].CurrKnowledge -= amount;
                if (!HauntIsStarted)
                    // player cant die until the haunt starts
                    Players[CurrentPlayerIndex].CurrKnowledge = 0;
            }
            if (trait == "Sanity")
            {
                Players[CurrentPlayerIndex].CurrSanity -= amount;
                if (!HauntIsStarted)
                    // player cant die until the haunt starts
                    Players[CurrentPlayerIndex].CurrMight = 0;
            }
        }

        public void GainPhysical(string trait, int amount)
        {
            if (trait == "Might")
            {
                Players[CurrentPlayerIndex].CurrMight += amount;
            }
            if (trait == "Speed")
            {
                Players[CurrentPlayerIndex].CurrSpeed += amount;
            }
        }

        public void GainMental(string trait, int amount)
        {
            if (trait == "Knowledge")
            {
                Players[CurrentPlayerIndex].CurrKnowledge += amount;
            }
            if (trait == "Sanity")
            {
                Players[CurrentPlayerIndex].CurrSanity += amount;
            }
        }
        #endregion

        #region JsonReaders
        List<Character> LoadCharacterJson(Stream file)
        {
            List<Character> temp = new List<Character>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                    temp = Character.ImportJSONAsList(reader.ReadToEnd());
                return temp;
            }
            catch
            {
                return null;
            }
        }
        List<Omen> LoadOmenJson(Stream file)
        {
            List<Omen> temp = new List<Omen>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string json = reader.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(json);

                    for (int i = 0; i < max_omens; i++)
                    {
                        int id = int.Parse(data[i]["ID"].ToString());
                        string name = data[i]["Name"].ToString();
                        string desc = data[i]["Description"].ToString();
                        bool dropped = bool.Parse(data[i]["Dropped"].ToString());
                        string coltype = data[i]["ColType"].ToString();
                        string coltrgt = data[i]["ColTarget"].ToString();
                        int colamnt = int.Parse (data[i]["ColAmount"].ToString());

                        string usetype = data[i]["UseType"].ToString();
                        string usetrgt = data[i]["UseTarget"].ToString();
                        int useamnt = int.Parse(data[i]["UseAmount"].ToString());

                        Omen om = new Omen(id, name, desc, dropped, coltype, coltrgt, colamnt, usetype, usetrgt, useamnt);
                        temp.Add(om);
                    }
                }
                return temp;
            }
            catch
            {
                return null;
            }
        }
        List<Event> LoadEventJson(Stream file)
        {
            List<Event> temp = new List<Event>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string json = reader.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(json);

                    for (int i = 0; i < max_events; i++)
                    {
                        int id = int.Parse(data[i]["ID"].ToString());
                        string name = data[i]["Name"].ToString();
                        string desc = data[i]["Description"].ToString();

                        Event ev = new Event(id, name, desc);
                        temp.Add(ev);
                    }
                }
                return temp;
            }
            catch
            {
                return null;
            }
        }
        List<Item> LoadItemJson(Stream file)
        {
            List<Item> temp = new List<Item>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string json = reader.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(json);

                    for (int i = 0; i < max_items; i++)
                    {
                        int id = int.Parse(data[i]["ID"].ToString());
                        string name = data[i]["Name"].ToString();
                        string desc = data[i]["Description"].ToString();

                        Item it = new Item(id, name, desc);
                        temp.Add(it);
                    }
                }
                return temp;
            }
            catch
            {
                return null;
            }
        }
        List<Tile> LoadTileJson(Stream file) 
        {
            List<Tile> temp = new List<Tile>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                    temp = Tile.ImportJSONAsList(reader.ReadToEnd());

                
                return temp;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        List<Haunt> LoadHauntJson(Stream file)
        {
            List<Haunt> temp = new List<Haunt>();
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string json = reader.ReadToEnd();
                    JsonData data = JsonMapper.ToObject(json);

                    for (int i = 0; i < max_haunts; i++)
                    {
                        Haunt haunt = new Haunt
                        {
                            ID = int.Parse(data[i]["ID"].ToString()),
                            Name = data[i]["Name"].ToString(),
                            TraitorWin = (TraitorVictoryTypes)Enum.Parse(typeof(TraitorVictoryTypes), data[i]["TWin"].ToString()),
                            ExplorerWin = (ExplorerVictoryTypes)Enum.Parse(typeof(ExplorerVictoryTypes), data[i]["EWin"].ToString()),

                            // Traitor's Tome
                            TraitorStory = data[i]["TStory"].ToString(),
                            TraitorVictory = data[i]["TVictory"].ToString(),
                            TraitorLoss = data[i]["TLoss"].ToString(),
                            WhatTraitorKnows = data[i]["TKnows"].ToString(),
                            TraitorHowToWin = data[i]["THowToWin"].ToString(),
                            TraitorRightNow = data[i]["TRightNow"].ToString(),

                            // Secret's of Survival
                            ExplorerStory = data[i]["EStory"].ToString(),
                            ExplorerVictory = data[i]["EVictory"].ToString(),
                            ExplorerLoss = data[i]["ELoss"].ToString(),
                            WhatExplorersKnow = data[i]["EKnows"].ToString(),
                            ExplorerHowToWin = data[i]["EHowToWin"].ToString(),
                            ExplorerRightNow = data[i]["ERightNow"].ToString()
                        };

                        try
                        {
                            int j = 0;
                            while (true)
                            {
                                haunt.TokensSetAside.Add(new Token(data[i]["Tokens"][j]["Name"].ToString(), int.Parse(data[i]["Tokens"][j]["Roll"].ToString()), data[i]["Tokens"][j]["Item"].ToString(), data[i]["Tokens"][j]["Room"].ToString()));
                                j++;
                            };
                        }
                        catch
                        {
                            // not enough rows in array will cause break. will look for a better solution to filling variable json arrays
                        }
                        temp.Add(haunt);
                    }
                    return temp;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region ImageFinders
        void FindCharacterImage(string location)
        {
            foreach (Character cha in AllCharacters)
            {
                string str = cha.Name.Replace(' ', '_').Replace("\'","");
                //cha.CharacterCard = new Bitmap(location + "\\Characters\\" + str + CHARACTER_CARD_EXTENSION);
                //cha.Token = new Bitmap(location + "\\Tokens\\" + cha.Color + TOKEN_EXTENSION);
            }
        }
        void FindRoomImage(string location)
        {
            foreach (Tile tile in AllTiles)
            {
                string str = tile.Name.Replace(' ', '_').Replace('"', '_');
                //tile.Image = new Bitmap(location + "\\Tiles\\" + str + TILE_EXTENSION);
            }
        }
        #endregion
        protected Tile GetTileByID(int ID)
        {
            return AllTiles.Where(t => t.ID == ID).FirstOrDefault();
        }
    }
}
