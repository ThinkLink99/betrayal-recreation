using System;
using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public class DebuffCardAction : ICardAction
    {
        public DebuffCardAction(PlayerStats stat, int buff)
        {
            Triggers.Add(CardEventTriggers.LOST,
                (player) =>
                {
                    player.RemoveBuff(stat, buff);
                    return CardEventTriggers.LOST;
                });
        }

        public Dictionary<CardEventTriggers, Func<Player, CardEventTriggers>> Triggers { get; set; }
        public bool ContainsTrigger(CardEventTriggers trigger)
        {
            throw new NotImplementedException();
        }
        public void Run(Player player, CardEventTriggers trigger)
        {
            Triggers[trigger].Invoke(player);
        }
    }
}
