using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CSOTH_API
{
    public enum Directions
    {
        North=0,
        South=2,
        East=1,
        West=3
    }
    public enum Levels
    {
        Basement=0,
        Ground=1,
        Upper=2
    }

    [JsonObject]
    public class Tile : Writable<Tile>
    {
        private int northID = 0;
        private int southID = 0;
        private int eastID = 0;
        private int westID = 0;

        private bool[] _doorways = { false, false, false, false };
        private bool[] _stairs = { false, false };
        private Levels[] _levels = { CSOTH_API.Levels.Basement, CSOTH_API.Levels.Ground, CSOTH_API.Levels.Upper };
        private Tile[] _adjacent_tiles = { null, null, null, null };

        [JsonProperty("ID")]
        public int ID { get; set; }
        /// <summary>
        /// The name of the room
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }
        /// <summary>
        /// The image to display on the game board.
        /// </summary>
        public Texture2D Image { get; set; }
        [JsonProperty("Levels")]
        public Levels[] Levels { get { return _levels; } }
        /// <summary>
        /// A description of what the room tile is.
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; set; }

        /// <summary>
        /// A boolean array with 4 slots indicating whether a door exists on the north, south, east or west sides.
        /// </summary>
        [JsonProperty("Doors")]
        public bool[] Doorways { get { return _doorways; } }

        public bool NorthDoor { get { return Doorways[(int)Directions.North]; } set { _doorways[(int)Directions.North] = value; } }
        public bool EastDoor { get { return Doorways[(int)Directions.East]; } set { _doorways[(int)Directions.East] = value; } }
        public bool SouthDoor { get { return Doorways[(int)Directions.South]; } set { _doorways[(int)Directions.South] = value; } }
        public bool WestDoor { get { return Doorways[(int)Directions.West]; } set { _doorways[(int)Directions.West] = value; } }

        [JsonProperty("Stairs")]
        public bool[] Stairs { get { return _stairs; } }
        public bool DownStairs { get { return Stairs[0]; } set { _stairs[0] = value; } }
        public bool UpStairs { get { return Stairs[1]; } set { _stairs[1] = value; } }

        [JsonProperty("Starting")]
        public bool Starting { get; set; }
        [JsonProperty("Special")]
        public string Special { get; set; } 

        [JsonProperty("BoardPosition")]
        public Vector2 BoardPosition { get; set; }
        public float X { get { return BoardPosition.X; } set { BoardPosition = new Vector2(value, BoardPosition.Y); } }
        public float Y { get { return BoardPosition.Y; } set { BoardPosition = new Vector2(BoardPosition.X, value); } }
        public List<Character> CharactersInRoom { get; set; }
        [JsonProperty("Card")]
        public string CardInRoom { get; set; }

        [JsonProperty("North")]
        public Tile TileToNorth { get { return _adjacent_tiles[0]; } set { _adjacent_tiles[0] = value; } }
        [JsonProperty("South")]
        public Tile TileToSouth { get { return _adjacent_tiles[2]; } set { _adjacent_tiles[2] = value; } }
        [JsonProperty("East")]
        public Tile TileToEast { get { return _adjacent_tiles[1]; } set { _adjacent_tiles[1] = value; } }
        [JsonProperty("West")]
        public Tile TileToWest { get { return _adjacent_tiles[3]; } set { _adjacent_tiles[3] = value; } }

        public List<PictureBox> Zones { get; set; }

        [JsonConstructor]
        public Tile(int id, string name, string description,
                    Levels[] levels, Vector2 boardPosition,
                    bool[] doors, bool[] stairs,
                    string special, bool starting,
                    int north, int south, int east, int west,
                    string card)
        {
            ID = id;
            Name = name;
            Description = description;
            _levels = levels;
            if (boardPosition == null)
                boardPosition = Vector2.Zero;
            BoardPosition = boardPosition;

            _doorways = doors;
            _stairs = stairs;

            Special = special;
            Starting = starting;

            northID = north;
            southID = south;
            eastID = east;
            westID = west;

            CharactersInRoom = new List<Character>();

            CardInRoom = card;
            Zones = new List<PictureBox>();
        }
        public Tile(Tile copy)
        {
            ID = copy.ID;
            Name = copy.Name;
            _levels = copy.Levels;

            CardInRoom = copy.CardInRoom;
        }

        public void Place (Player currentPlayer, Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    BoardPosition = new Vector2(currentPlayer.CurrentTile.BoardPosition.X, 
                                                currentPlayer.CurrentTile.BoardPosition.Y - 1);
                    // rotate the tile until the south facing door is true
                    while (!SouthDoor)
                        RotateRight();

                    AssignAdjacentTile(Directions.South, currentPlayer.CurrentTile);
                    break;
                case Directions.South:
                    BoardPosition = new Vector2(currentPlayer.CurrentTile.BoardPosition.X,
                                                currentPlayer.CurrentTile.BoardPosition.Y + 1);
                    // rotate the tile until the south facing door is true
                    while (!NorthDoor)
                        RotateRight();

                    AssignAdjacentTile(Directions.North, currentPlayer.CurrentTile);
                    break;
                case Directions.East:
                    BoardPosition = new Vector2(currentPlayer.CurrentTile.BoardPosition.X + 1,
                                                currentPlayer.CurrentTile.BoardPosition.Y);
                    // rotate the tile until the south facing door is true
                    while (!WestDoor)
                        RotateRight();

                    AssignAdjacentTile(Directions.West, currentPlayer.CurrentTile);
                    break;
                case Directions.West:
                    BoardPosition = new Vector2(currentPlayer.CurrentTile.BoardPosition.X - 1,
                                                currentPlayer.CurrentTile.BoardPosition.Y);
                    // rotate the tile until the south facing door is true
                    while (!EastDoor)
                        RotateRight();

                    AssignAdjacentTile(Directions.East, currentPlayer.CurrentTile);
                    break;
            }
        }

        public void RotateRight()
        {
            // rotate right
            bool temp;
            temp = _doorways[0]; // save north door
            _doorways[0] = _doorways[3]; // move west door on to north door
            _doorways[3] = _doorways[2]; // move south door on to west door
            _doorways[2] = _doorways[1]; // move east door on to south door
            _doorways[1] = temp; // place the temp copy of north door on to east door
        }
        public void RotateLeft()
        {
            // rotate Left
            bool temp;
            temp = _doorways[0]; // save north door
            _doorways[0] = _doorways[1]; // move west door on to north door
            _doorways[1] = _doorways[2]; // move south door on to west door
            _doorways[2] = _doorways[3]; // move east door on to south door
            _doorways[3] = temp; // place the temp copy of north door on to east door
        }

        public void AssignAdjacentTile(Directions direction, Tile tile)
        {
            switch (direction)
            {
                case Directions.North:
                    tile.TileToSouth = this;
                    TileToNorth = tile;
                    break;
                case Directions.South:
                    tile.TileToNorth = this;
                    TileToSouth = tile;
                    break;
                case Directions.East:
                    tile.TileToWest = this;
                    TileToEast = tile;
                    break;
                case Directions.West:
                    tile.TileToEast = this;
                    TileToWest = tile;
                    break;
            }
        }
        public int GetAdjacentRoomID(Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return northID;
                case Directions.South:
                    return southID;
                case Directions.East:
                    return eastID;
                case Directions.West:
                    return westID;
            }
            return 0;
        }
    }
}
