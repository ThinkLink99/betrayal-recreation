using System;

namespace betrayal_recreation_shared
{
    public static class GameEvents
    {
        public static event Action onDeckReshuffled;
        public static void DeckReshuffled ()
        {
            onDeckReshuffled?.Invoke();
        }

        public static event Action<Room> onRoomDrawn;
        public static void RoomDrawn (Room room)
        {
            onRoomDrawn?.Invoke(room);
        }

        public static event Action<Room, Player> onRoomEnter;
        public static void RoomEnter (Room room, Player player)
        {
            onRoomEnter?.Invoke(room, player);
        }

        public static event Action<Room, Player> onRoomLeave;
        public static void RoomLeave(Room room, Player player)
        {
            onRoomLeave?.Invoke(room, player);
        }

        public static event Action<Omen, Player> onOmenPickup;
        public static void OmenPickup(Omen omen, Player player)
        {
            onOmenPickup?.Invoke(omen, player);
        }
        public static event Action<Omen, Player> onOmenLost;
        public static void OmenLost(Omen omen, Player player)
        {
            onOmenLost?.Invoke(omen, player);
        }
    }
}   
