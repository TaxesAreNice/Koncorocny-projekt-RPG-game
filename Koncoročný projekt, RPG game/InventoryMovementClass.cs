using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class InventoryMovementClass
    {
        public int InventoryX = 0;
        public int InventoryY = 0;

        public int LastInventoryX = 0;
        public int LastInventoryY = 0;

        private void PlayerMovement(string key)
        {
            switch (key)
            {
                case "W":
                    InventoryY -= 1;
                    break;
                case "A":
                    InventoryX -= 1;
                    break;
                case "S":
                    InventoryY += 1;
                    break;
                case "D":
                    InventoryX += 1;
                    break;
            }

        }
        public bool CheckingForWalls(string key)
        {


            LastInventoryX = InventoryX;
            LastInventoryY = InventoryY;
            PlayerMovement(key);
            return true;
        }
    }
}
