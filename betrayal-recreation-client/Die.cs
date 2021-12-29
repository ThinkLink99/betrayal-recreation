using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_shared
{
    public class Die
    {
        Random rand = new Random();
        int[] sides = new int[6];

        public Die(params int[] sides)
        {
            this.sides = sides;
        }

        public int Roll ()
        {
            int i = rand.Next(sides.Length);
            return sides[i];
        }
    }
}
