using System;
using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public class BuffCardAction : ICardAction
    {
        int buff;
        PlayerStats stat;
        public BuffCardAction(PlayerStats stat, int buff)
        {
            Triggers.Add(CardEventTriggers.PICKUP,
                (player) =>
                {
                    player.AddBuff(stat, buff);
                    return CardEventTriggers.PICKUP;
                });

            this.stat = stat;
            this.buff = buff;
        }

        public Dictionary<CardEventTriggers, Func<Player, CardEventTriggers>> Triggers { get; set; }
        public bool ContainsTrigger(CardEventTriggers trigger)
        {
            throw new System.NotImplementedException();
        }
        public void Run(Player player, CardEventTriggers trigger)
        {
            Triggers[trigger].Invoke(player);
        }
    }
}
