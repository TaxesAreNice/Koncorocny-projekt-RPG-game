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
            "Knight Sword",
        };
        List<string> dragonItems = new List<string>
        {
            "Dragon Helmet",
            "Dragon Chestplate",
            "Dragon Leggins",
            "Dragon Boots",
        };
        List<string> Scrolls = new List<string>
        {
            "Fire scroll",
            "Lightning scroll",
            "Healing scroll",
            "Shield scroll",
            "Revive scroll"

        };
        List<string> traderItems = new List<string>
        {
            "Health Potion",
            "Big Health Potion",
            "2x Damage Potion",
            "Mana Potion",
            "Big Mana Potion",
            "Fire Scroll",
            "Cursed Scroll",
            "Lightning Scroll",
            "Healing Scroll",
            "Shield Scroll",
            "Revive Scroll",
        };


        public bool RollChance(int percent)
        {
            return random.Next(0, 100) < percent;
        }

        void DropLoot(string enemyName)
        {
            if (fighting.enemyDead())
            {

                if (enemyName == "Vampire")
                {
                    if (RollChance(5))
                    {
                        string droppedItem = "Breaker Ring";
                    }
                }
                else if (enemyName == "Knight")
                {
                    if (RollChance(20))
                    {

                        string droppedItem = knightItems[random.Next(knightItems.Count)];
                    }
                }
                else if (enemyName == "Possessed King")
                {
                    if (RollChance(100))
                    {
                        string droppedItem = "BloodThorn Sword";
                    }
                }
                else if (enemyName == "Dragon")
                {
                    if (RollChance(10))
                    {
                        string droppedItem = dragonItems[random.Next(dragonItems.Count)];
                    }
                }
                else if (enemyName == "Mythical Pig")
                {
                    if (RollChance(50))
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
                else if (enemyName == "mage")
                {
                    if (RollChance(20))
                    {
                        string droppedItem = Scrolls[random.Next(Scrolls.Count)];
                    }
                }
                else if (enemyName == "Cursed Trader")
                {
                    if (RollChance(100))
                    {
                        string droppedItem = "Cursed scroll";
                    }
                }
                else if (enemyName == "Trader")
                {
                    if (RollChance(40))
                    {
                        int itemCount = random.Next(1, 5);

                        for (int i = 0; i < itemCount; i++)
                        {
                            string droppedItem = traderItems[random.Next(traderItems.Count)];
                            string droppedSecondItem = traderItems[random.Next(traderItems.Count)];
                        }
                    }
                }
            }
        }
    }
}