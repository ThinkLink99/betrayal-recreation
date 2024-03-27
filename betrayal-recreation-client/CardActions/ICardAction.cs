using System;
using System.Collections.Generic;

namespace betrayal_recreation_shared
{
    public enum CardEventTriggers { PICKUP, LOST, USE }
    public interface ICardAction
    {
        Dictionary<CardEventTriggers, Func<Player, CardEventTriggers>> Triggers { get; }
        void Run (Player player, CardEventTriggers trigger);
        bool ContainsTrigger(CardEventTriggers trigger);
    }
}