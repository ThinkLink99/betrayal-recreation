namespace betrayal_recreation_shared
{
    public abstract class PlayerItem : BasicObjectInformation
    {
        private bool _canDrop = false;
        private bool _canTrade = false;
        private bool _canSteal = false;

        protected PlayerItem(int id, string name, string description, bool canDrop = true, bool canTrade = true, bool canSteal = true)
            : base(id, name, description)
        {
            _canDrop = canDrop;
            _canTrade = canTrade;
            _canSteal = canSteal;
        }
    }
}
