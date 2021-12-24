using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_client
{
     public class Deck<T>
    {
        private Queue<T> _drawPile;
        private Stack<T> _discardPile;

        public Deck (ICollection<T> deck)
        {
            _drawPile = new Queue<T>(deck);
            _discardPile = new Stack<T>();

            Reshuffle();
        }

        /// <summary>
        /// Add discard pile into draw pile then reshuffle
        /// </summary>
        public void Reshuffle ()
        {
            GameEvents.DeckReshuffled();

            List<T> temp = new List<T>();
            List<T> shuffled = new List<T>();
            temp.AddRange(_discardPile);
            temp.AddRange(_drawPile);

            var count = temp.Count;
            for (int i = 0; i < count; i++)
            {
                var rand = new Random();
                var r = rand.Next(0, count);

                shuffled.Add(temp[r]);
                temp.RemoveAt(r);

                count--;
            }

            _drawPile = new Queue<T>(shuffled);
        }


        public T Draw ()
        {
            if (_drawPile.Count() == 0) Reshuffle();
            return _drawPile.Dequeue();
        }
        public void Discard (T discard)
        {
            _discardPile.Push(discard);
        }
    }
}
