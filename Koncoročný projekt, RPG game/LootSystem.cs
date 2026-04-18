using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class LootSystem
    {
        private static Random random = new Random();

        private List<string> DroppableItems = new List<string>()
            {
                "Sword of Valor",
                "Shield of Resilience",
                "Potion of Healing",
                "Ring of Strength",
                "Amulet of Wisdom"
            };
        public bool RollChance(int percent)
        {
            return random.Next(0, 100) < percent;  // cela loopka je nachystana na drop system z Mobov
        }
    }
}
