using System.Collections.Generic;
using Newtonsoft.Json;

namespace betrayal_recreation_client
{
    [JsonObject]
    public class Player : Writable<Player>
    {
        bool[] buff_rooms = new bool[4];

        public string Name { get; set; }
        public bool TurnIsOver { get; set; }
        public bool IsHuman { get; set; }
        public Character Details { get; set; }
        public int SpeedRemaining { get; set; }

        /// <summary>
        /// The current knowledge index of the player
        /// </summary>
        public int CurrKnowledge { get; set; }
        /// <summary>
        /// The current sanity index of the player
        /// </summary>
        public int CurrSanity { get; set; }
        /// <summary>
        /// The current might index of the player
        /// </summary>
        public int CurrMight { get; set; }
        /// <summary>
        /// The current speed index of the player
        /// </summary>
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

        public bool ChapelBuff { get { return buff_rooms[0]; } set { buff_rooms[0] = value; } }
        public bool LibraryBuff { get { return buff_rooms[1]; } set { buff_rooms[1] = value; } }
        public bool LarderBuff { get { return buff_rooms[2]; } set { buff_rooms[2] = value; } }
        public bool GymBuff { get { return buff_rooms[3]; } set { buff_rooms[3] = value; } }
        public List<Card> CollectedCards { get; set; }

        /// <summary>
        /// Creates a new Player object that will control where the Character goes and how they act
        /// </summary>
        /// <param name="Details"> The Character the Player will be controlling</param>
        /// <param name="Name">The name of the Player</param>
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
        /// Uses the selected Item Card from the Players CollectedItems
        /// </summary>
        /// <param name="item">The Item to be used</param>
        public void UseItem(Item item)
        {

        }
    }
}
