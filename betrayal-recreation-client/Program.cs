﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace betrayal_recreation_client
{
    static class Program
    {
        static Session session;

        public static void Main ()
        {
            session = new Session(new List<Room>()
            {
                new Room(1, "Grand Entrance", "Grand Entrance", new Room.LevelsEnum[1] { Room.LevelsEnum.Ground }, true),
                new Room(2, "Foyer", "Foyer", new Room.LevelsEnum[1] { Room.LevelsEnum.Ground }, true),
                new Room(3, "Downstairs Landing", "Downstairs Landing", new Room.LevelsEnum[1] { Room.LevelsEnum.Ground }, true),
                new Room(4, "Upper Landing", "Upper Landing", new Room.LevelsEnum[1] { Room.LevelsEnum.Upper }, true),
                new Room(5, "Basement Landing", "Basement Landing", new Room.LevelsEnum[1] { Room.LevelsEnum.Basement }, true),
            });

            session.AddCharacter(new Character(0, "Trey", "Mar 1 1999", "blue", new int[] { }, new int[] { }, new int[] { }, new int[] { }, 0, 0, 0, 0));

            session.LogCharacters();

            session.Floors[1].Print();

            Console.ReadKey();
        }
    }
}
