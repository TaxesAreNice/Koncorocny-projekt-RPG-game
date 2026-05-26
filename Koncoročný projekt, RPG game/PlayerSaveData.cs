using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    public class PlayerSaveData
    {
        public int HP { get; set; } = 100;
        public int Attack { get; set; } = 15;
        public int Defense { get; set; } = 0;

        public int CurrentRoom { get; set; } = 0;
        public int PlayerX { get; set; } = 0;
        public int PlayerY { get; set; } = 0;
        public List<List<string>> Inventory { get; set; } = [];
        public Dictionary<string, string> Equipped { get; set; } = [];
    }
}
