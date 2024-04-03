using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_shared
{
    public class TurnOrder
    {
        private Queue<Player> _turnOrder;

        public TurnOrder(List<Player> players)
        {
            _turnOrder = new Queue<Player>(players);
        }

        public Queue<Player> Order { get => _turnOrder; set => _turnOrder = value; }
        public Player CurrentPlayer
        {
            get
            {
                return _turnOrder.First();
            }
        }
        public Player LastPlayer
        {
            get
            {
                return _turnOrder.Last();
            }
        }
        public Player NextPlayer
        {
            get
            {
                return _turnOrder.ElementAt(_turnOrder.Count - 1);
            }
        }

        public void AddPlayer (Player player)
        {
            if (player == null) return;
            _turnOrder.Enqueue(player);
        }
        public void EndTurn ()
        {
            _turnOrder.Enqueue(_turnOrder.Dequeue());
        }
    }
}
