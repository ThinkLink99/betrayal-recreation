using Newtonsoft.Json;
using System.Drawing;

namespace betrayal_recreation_client
{
    public class Character : Writable<Character>
    {
        int _starting_knowledge_index = 0;
        int _starting_sanity_index = 0;
        int _starting_might_index = 0;
        int _starting_speed_index = 0;
        public int Starting_knowledge_index { get => _starting_knowledge_index; }
        public int Starting_sanity_index { get => _starting_sanity_index; }
        public int Starting_might_index { get => _starting_might_index; }
        public int Starting_speed_index { get => _starting_speed_index; }

        public enum Colors
        {
            White = 0,
            Blue = 1,
            Red = 2,
            Green = 3,
            Purple = 4,
            Yellow = 5
        }
        public int ID { get; set; }
        public int Side { get; set; }
        public string Name { get; set; }
        
        public string Color { get; set; }
        public string Birthday { get; set; }

        /// <summary>
        /// A range of values the character has for knowledge
        /// </summary>
        public int[] Knowledge { get; set; }
        /// <summary>
        /// A range of values the character has for sanity
        /// </summary>
        public int[] Sanity { get; set; }
        /// <summary>
        /// A range of values the character has for might
        /// </summary>
        public int[] Might { get; set; }
        /// <summary>
        /// A range of values the character has for speed
        /// </summary>
        public int[] Speed { get; set; }


        /// <summary>
        /// Creates a new Character
        /// </summary>
        /// <param name="id">The ID of which Character side you are using</param>
        /// <param name="name">The Name of the Character</param>
        /// <param name="birthday">The Birthday of the Character, used when determining who starts the game</param>
        /// <param name="color">The token color of the Character</param>
        /// <param name="knowA">The Starting Knowledge set of the Character</param>
        /// <param name="sanity">The Starting Sanity set of the Character</param>
        /// <param name="might">The Starting Might set of the Character</param>
        /// <param name="speed">The Starting Speed set of the Character</param>
        [JsonConstructor]
        public Character(int id, string name, string birthday, string color,
                         int[] knowledge, int[] sanity, int[] might, int[] speed,
                         int knowledgeIndex, int sanityIndex, int mightIndex, int speedIndex)
        {
            ID = id;
            Name = name;
            Color = color;

            _starting_knowledge_index = knowledgeIndex;
            _starting_sanity_index = sanityIndex;
            _starting_might_index = mightIndex;
            _starting_speed_index = speedIndex;

            Knowledge = knowledge;
            Sanity = sanity;
            Might = might;
            Speed = speed;

            Birthday = birthday;
        }
        /// <summary>
        /// THe JSON Deserialization constructor
        /// </summary>
        /// <param name="id">The ID of which Character side you are using</param>
        /// <param name="side">The side of the chosen character pair</param>
        public Character(int id, int side)
        {
            ID = id;
            Side = side;
        }

        /// <summary>
        /// Creates a copy of a game character and places it on the Front Entrance Tile
        /// </summary>
        /// <param name="character">The Character information to be copied</param>
        public Character(Character character)
        {
            ID = character.ID;
            Name = character.Name;
            Color = character.Color;
            Birthday = character.Birthday;
        }
    }
}
