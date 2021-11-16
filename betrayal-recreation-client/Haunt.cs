using System.Collections.Generic;

namespace CSOTH_API
{
    public enum TraitorVictoryTypes
    {
        TraitorKillAll,
        CompleteSkillChecks
    }
    public enum ExplorerVictoryTypes
    {
        ExplorersKillTrator,
        ExplorersExcorciseMonster,
        ExplorersEscapeHouse,
        CompleteSkillChecks
    }

    /// <summary>
    /// 
    /// </summary>
    public class Haunt
    {
        // Things that start this haunt
        public string StartingOmen { get; set; }
        public string[] StartingRooms { get; set; }

        // Basic Haunt info
        public int ID { get; set; }
        public string Name { get; set; }
        public Player[] Traitors { get; set; }
        public Player[] Explorers { get; set; }
        public ExplorerVictoryTypes ExplorerWin { get; set; }
        public TraitorVictoryTypes TraitorWin { get; set; }

        // Traitors Tome
        public string TraitorStory { get; set; }
        public string TraitorVictory { get; set; }
        public string TraitorLoss { get; set; }
        public string WhatTraitorKnows { get; set; }
        public string TraitorHowToWin { get; set; }
        public string TraitorRightNow { get; set; }

        // Secrets of Survival
        public string ExplorerStory { get; set; }
        public string ExplorerVictory { get; set; }
        public string ExplorerLoss { get; set; }
        public string WhatExplorersKnow { get; set; }
        public string ExplorerRightNow { get; set; }
        public string ExplorerHowToWin { get; set; }

        // Haunt gameplay
        public bool HauntCompleted { get { return ExplorersCompletedHaunt() || TraitorsCompletedHaunt(); } }
        public List<Token> TokensSetAside { get; set; }
        public List<Tile> RequiredExplorerRooms { get; set; }
        public List<Tile> RequiredTraitorRooms { get; set; }

        // Haunt Victory
        public bool ExplorersCompletedHaunt()
        {
            bool win = false;

            // check to see if all players have left the house
            if (ExplorerWin == ExplorerVictoryTypes.ExplorersEscapeHouse)
            {
                foreach (Player player in Explorers)
                {
                    if (player.CurrentTile.Name == "Outside")
                        win = true;
                    else
                        win = false;
                }
            }

            // Check to see that the players have all the required items and tokens
            if (ExplorerWin == ExplorerVictoryTypes.ExplorersExcorciseMonster)
            {

            }

            // Check to see if the players have to kill the traitors
            if (ExplorerWin == ExplorerVictoryTypes.ExplorersKillTrator)
            {
                foreach (Player player in Traitors)
                {
                    if (player.IsDead)
                        win = true;
                    else
                        win = false;
                }
            }

            return win;
        }
        public bool TraitorsCompletedHaunt()
        {
            if (TraitorWin == TraitorVictoryTypes.TraitorKillAll)
            {
                bool killall = false;
                foreach (Player explorer in Explorers)
                {
                    killall = explorer.IsDead;
                }
                return killall;
            }
            return false;
        }

        public bool ExplorersHaveRequiredItems ()
        {
            return false;
        }
        public bool ExplorersHaveRequiredTokens ()
        {
            return false;
        }
        public bool ExplorerIsInRequiredRoom (Player explorer)
        {
            foreach (Tile room in RequiredExplorerRooms)
            {
                if (room == explorer.CurrentTile)
                    return true;
            }
            return false;
        }

        public bool TraitorsHaveRequiredItems ()
        {
            return false;
        }
        public bool TraitorsHaveRequiredTokens()
        {
            return false;
        }
        public bool TraitorIsInRequiredRoom (Player traitor)
        {
            foreach (Tile room in RequiredTraitorRooms)
            {
                if (room == traitor.CurrentTile)
                    return true;
            }
            return false;
        }

        // Haunt Setup
        public void SetupHaunt ()
        {
            // This function should take the required tokens (if any) and place them into the rooms they belong in (if any)
            // This function should also check to make sure no rooms needs to be changed. if so, changed those rooms
        }

        /// <summary>
        /// This function allows the player to make a skill roll when in a required room.
        /// If the Roll is good, the player will recieve a token towards completion
        /// </summary>
        /// <param name="roll">The current number rolled by the player</param>
        /// <param name="player">The player currently doing a skill check</param>
        /// <param name="token">The token to give the player upon completion</param>
        /// <returns></returns>
        public string TokenRollinRoom (int roll, Player player, Token token)
        {
            if (token.Required_Room != "Any")
                if ((roll >= token.Required_Roll) && (player.CurrentTile.Name == token.Required_Room))
                {
                    CollectItem(player, token);
                    return "[Successful Roll]: \n";
                }
                else
                {
                    return "[Unsuccessful Roll]: \n";
                }
            else
            {
                if (roll >= token.Required_Roll)
                {
                    CollectItem(player, token);
                    return "[Successful Roll]: \n";
                }
                else
                {
                    return "[Unsuccessful Roll]: \n";
                }
            }
        }

        /// <summary>
        /// This function allows the player to make a skill roll on an item card as long as that item card is in the player's inventory.
        /// </summary>
        /// <param name="roll">The current number rolled by the player</param>
        /// <param name="player">The player currently doing a skill check</param>
        /// <param name="token">The token to give the player upon completion</param>
        /// <returns></returns>
        public string TokenRollOnItem (int roll, Player player, Token token)
        {
            if (token.Required_Item != "Any")
            {
                foreach (Card item in player.CollectedCards)
                {
                    if (item.Name == token.Required_Item)
                    {
                        if (roll >= token.Required_Roll)
                        {
                            CollectItem(player, token);
                            return "[Successful Roll]: " + player.Details.Name + "(" + player.Name + ") successfully rolled a " + token.Required_Roll + " or higher to get the " + token.Name + "\n";
                        }
                    }
                }
                return "[Unsuccessful Roll]: " + player.Details.Name + "(" + player.Name + ") did not roll a " + token.Required_Roll + " or higher to get the " + token.Name + "\n";
            }
            else
            {
                if (roll >= token.Required_Roll)
                {
                    CollectItem(player, token);
                    return "[Successful Roll]: " + player.Details.Name + "(" + player.Name + ") successfully rolled a " + token.Required_Roll + " or higher to get the " + token.Name + "\n";
                }
                else
                {
                    return "[Unsuccessful Roll]: " + player.Details.Name + "(" + player.Name + ") did not roll a " + token.Required_Roll + " or higher to get the " + token.Name + "\n";
                }
            }
        }

        /// <summary>
        /// CollectItem will handle picking up an Item Card. This function will check to see if a roll is required for the item.
        /// If the player beats the skill check, it will award the item.
        /// </summary>
        public void CollectItem (Player player, Card itemToCollect)
        {
            player.CollectedCards.Add(itemToCollect);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemToCollect"></param>
        public void CollectItem (Player player, Token itemToCollect)
        {
            // add the token to the player inventory
            player.CollectedTokens.Add(itemToCollect);
        }
    }
}
