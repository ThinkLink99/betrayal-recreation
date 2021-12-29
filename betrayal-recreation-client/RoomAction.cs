namespace betrayal_recreation_shared
{
    public abstract class RoomAction : BasicObjectInformation
    {
        public enum RunTime { ROOM_ENTER, ROOM_LEAVE }
        RunTime _runTime;

        public RunTime RunTime1 { get => _runTime; set => _runTime = value; }

        protected RoomAction(int id, string name, string description, RunTime runTime = RunTime.ROOM_ENTER)
            : base(id, name, description)
        {
            _runTime = runTime;
        }

        public abstract void Run(Player player);
    }
}