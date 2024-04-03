using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_shared
{
    public static class Debug
    {
        public static void LogCharacters(List<Character> characters)
        {
            Console.WriteLine("-- Characters --");
            foreach (Character c in characters)
            {
                Console.WriteLine($"{c.Name} ({c.Color} {c.Side})");
            }
        }
        public static void LogPlayers(List<Player> players)
        {
            Console.WriteLine("-- Players --");
            foreach (Player p in players)
            {
                Console.WriteLine($"{p.objectInformation.Name} [Character: {p.Character.Name}]");
            }
        }
        public static void LogRoomDeck (Deck<Room> deck)
        {
            Console.WriteLine("-- Draw --");
            foreach (Room r in deck.DrawPile.ToList())
            {
                Console.WriteLine($"{r.Name}");
            }
            Console.WriteLine("-- Discard --");
            foreach (Room r in deck.DiscardPile.ToList())
            {
                Console.WriteLine($"{r.Name}");
            }
        }
    }
}
