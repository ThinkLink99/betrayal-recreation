namespace betrayal_recreation_shared.CardActions
{
    public class DebuffCardAction : CardAction
    {
        int buff;
        PlayerStats stat;
        public DebuffCardAction(PlayerStats stat, int buff)
            : base(0, "Player Debuff", "Debuff a player stat by a determined amount", CardEventTriggers.LOST)
        {
            this.stat = stat;
            this.buff = buff;
        }

        public override void Run(Player player)
        {
            player.RemoveBuff(stat, buff);
        }
    }
}
