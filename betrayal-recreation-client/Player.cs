using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSOTH_API
{
    [JsonObject]
    public class Player : Writable<Player>
    {
        Tile _tile = null;
        bool[] buff_rooms = new bool[4];

        [JsonProperty("Name")]
        public string Name { get; set; }
        public bool TurnIsOver { get; set; }
        [JsonProperty("IsHuman")]
        public bool IsHuman { get; set; }
        [JsonProperty("Details")]
        public Character Details { get; set; }
        public int SpeedRemaining { get; set; }
        [JsonProperty("CurrentLevel")]
        public Levels CurrentLevel { get; set; }
        [JsonProperty("CurrentTile")]
        public Tile CurrentTile
        {
            get
            {
                return _tile;
            }
            set
            {
                _tile = value;
            }
        }

        /// <summary>
        /// The current knowledge index of the player
        /// </summary>
        [JsonProperty("CurrKnowledge")]
        public int CurrKnowledge { get; set; }
        /// <summary>
        /// The current sanity index of the player
        /// </summary>
        [JsonProperty("CurrSanity")]
        public int CurrSanity { get; set; }
        /// <summary>
        /// The current might index of the player
        /// </summary>
        [JsonProperty("CurrMight")]
        public int CurrMight { get; set; }
        /// <summary>
        /// The current speed index of the player
        /// </summary>
        [JsonProperty("CurrSpeed")]
        public int CurrSpeed { get; set; }

        /// <summary>
        /// Checks if any stat indexes dropped below 0 and returns true if so.
        /// </summary>
        public bool IsDead
        {
            get
            {
                if (CurrKnowledge < 0)
                    return true;
                if (CurrSanity < 0)
                    return true;
                if (CurrSpeed < 0)
                    return true;
                if (CurrMight < 0)
                    return true;
                return false;
            }
        }

        public int Slot { get; set; }

        [JsonProperty("ChapelBuff")]
        public bool ChapelBuff { get { return buff_rooms[0]; } set { buff_rooms[0] = value; } }
        [JsonProperty("LibraryBuff")]
        public bool LibraryBuff { get { return buff_rooms[1]; } set { buff_rooms[1] = value; } }
        [JsonProperty("LarderBuff")]
        public bool LarderBuff { get { return buff_rooms[2]; } set { buff_rooms[2] = value; } }
        [JsonProperty("GymBuff")]
        public bool GymBuff { get { return buff_rooms[3]; } set { buff_rooms[3] = value; } }
        [JsonProperty("Cards")]
        public List<Card> CollectedCards { get; set; }
        [JsonProperty("Tokens")]
        public List<Token> CollectedTokens { get; set; }

        public int GetTrait (string trait)
        {
            switch (trait)
            {
                case "Knowledge":
                    return Details.Knowledge[CurrKnowledge];
                    
                case "Sanity":
                    return Details.Sanity[CurrSanity];

                case "Might":
                    return Details.Might[CurrMight];

                case "Speed":
                    return Details.Speed[CurrSpeed];
            }
            return 0;
        }

        /// <summary>
        /// Creates a new Player object that will control where the Character goes and how they act
        /// </summary>
        /// <param name="Details"> The Character the Player will be controlling</param>
        /// <param name="Name">The name of the Player</param>
        [JsonConstructor]
        public Player(Character details, string name, bool ishuman, 
                      int currKnowledge, int currSanity, int currMight, int currSpeed)
        {
            Name = name;
            Details = details;
            IsHuman = ishuman;

            CurrKnowledge = currKnowledge;
            CurrSanity = currSanity;
            CurrMight = currMight;
            CurrSpeed = currSpeed;

            SpeedRemaining = Details.Speed[CurrSpeed];

            CollectedCards = new List<Card>();
        }

        /// <summary>
        /// Moves the character to the selected Tile
        /// </summary>
        /// <param name="tile">The tile to move the Character to</param>
        public void Move(Tile tile)
        {
            if (CurrentTile != null)
                CurrentTile.CharactersInRoom.Remove(Details);
            if (tile != null)
                CurrentTile = tile;

            CurrentTile.CharactersInRoom.Add(Details);
        }

        /// <summary>
        /// Uses the selected Item Card from the Players CollectedItems
        /// </summary>
        /// <param name="item">The Item to be used</param>
        public void UseItem(Item item)
        {

        }
    }
}
