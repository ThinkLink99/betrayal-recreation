using Newtonsoft.Json;
using System.Drawing;

namespace betrayal_recreation_shared
{
    public class Character : BasicObjectInformation
    {
        int[] _knowledgeArray;
        int[] _sanityArray;
        int[] _mightArray;
        int[] _speedArray;

        int _starting_knowledge_index = 0;
        int _starting_sanity_index = 0;
        int _starting_might_index = 0;
        int _starting_speed_index = 0;

        int _current_knowledge_index = 0;
        int _current_sanity_index = 0;
        int _current_might_index = 0;
        int _current_speed_index = 0;


        public enum Colors
        {
            White = 0,
            Blue = 1,
            Red = 2,
            Green = 3,
            Purple = 4,
            Yellow = 5
        }
        public int Side { get; set; }
        
        public string Color { get; set; }
        public string Birthday { get; set; }

        public int Knowledge => _knowledgeArray[_current_knowledge_index];
        public int Sanity => _sanityArray[_current_sanity_index];
        public int Might => _mightArray[_current_might_index];
        public int Speed => _speedArray[_current_speed_index];

        public Character(int id, string name, string birthday, string color,
                         int[] knowledge, int[] sanity, int[] might, int[] speed,
                         int knowledgeIndex, int sanityIndex, int mightIndex, int speedIndex)
            :base (id, name, "")
        {
            Color = color;

            _starting_knowledge_index = knowledgeIndex;
            _starting_sanity_index = sanityIndex;
            _starting_might_index = mightIndex;
            _starting_speed_index = speedIndex;

            _current_knowledge_index = _starting_knowledge_index;
            _current_sanity_index = _starting_sanity_index;
            _current_might_index = _starting_might_index;
            _current_speed_index = _starting_speed_index;

            _knowledgeArray = knowledge;
            _sanityArray = sanity;
            _mightArray = might;
            _speedArray = speed;

            Birthday = birthday;
        }
        public Character(int id, int side)
            :base (id, "", "")
        {
            Side = side;
        }
        public Character(Character character)
            :base (character.ID, character.Name, "")
        {
            Color = character.Color;
            Birthday = character.Birthday;
        }
    }
}
