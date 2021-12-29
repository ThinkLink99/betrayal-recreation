using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_shared.CardActions
{
    public class BuffCardAction : CardAction
    {
        int buff;
        PlayerStats stat;
        public BuffCardAction(PlayerStats stat, int buff)
            : base(0, "Player Buff", "Buff a player stat by a determined amount", CardEventTriggers.PICKUP)
        {
            this.stat = stat;
            this.buff = buff;
        }

        public override void Run(Player player)
        {
            player.AddBuff(stat, buff);
        }
    }
}
