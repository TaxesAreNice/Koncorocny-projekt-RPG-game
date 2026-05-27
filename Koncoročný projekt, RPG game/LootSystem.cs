using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Koncoročný_projekt__RPG_game
{
    internal class LootSystem
    {
        Fighting fighting = new Fighting();

        private static Random random = new Random();

        List<string> knightItems = new List<string>
        {
            "Knight Helmet",
            "Knight Chestplate",
            "Knight Leggins",
            "Knight Boots",
        };
        List<string> dragonItems = new List<string>
        {
            "Dragon Helmet",
            "Dragon Chestplate",
            "Dragon Leggins",
            "Dragon Boots",
            "Fire Breath Orb",
            "Dragon Sword",
        };
        List<string> batItems = new List<string>
        {
            "Rusty Helmet",
            "Rusty Chestplate",
            "Rusty Leggins",
            "Rusty Boots",
            "Dull Blade",
        };

        public bool RollChance(int percent)
        {
            return random.Next(0, 100) < percent;
        }

        void DropLoot(string enemyName)
        {
            if (fighting.enemyDead())
            {
                if (enemyName == "Knight")
                {
                    if (RollChance(20))
                    {

                        string droppedItem = knightItems[random.Next(knightItems.Count)];
                    }
                }
                else if (enemyName == "Dragon")
                {
                    if (RollChance(50))
                    {
                        string droppedItem = dragonItems[random.Next(dragonItems.Count)];
                    }
                }
                else if (enemyName == "Mythical Pig")
                {
                    if (RollChance(100))
                    {
                        string droppedItem = "Mythical Meal";
                    }
                }
                else if (enemyName == "Phoenix")
                {
                    if (RollChance(30))
                    {
                        string droppedItem = "Phoenix Feather";
                    }
                }
                else if (enemyName == "Bat")
                {
                    if (RollChance(50))
                    {
                        string droppedItem = batItems[random.Next(batItems.Count)];
                    }
                }
            }
        }
    }
}