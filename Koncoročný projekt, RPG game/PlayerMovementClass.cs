using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class PlayerMovementClass
    {
        public int PlayerX = 0;
        public int PlayerY = 0;

        public int LastPlayerX = 0;
        public int LastPlayerY = 0;

        private void PlayerMovement(string key)
        {
            switch (key)
            {
                case "W":
                    PlayerY -= 1;
                    break;
                case "A":
                    PlayerX -= 1;
                    break;
                case "S":
                    PlayerY += 1;
                    break;
                case "D":
                    PlayerX += 1;
                    break;
            }

        }
        public bool CheckingForWalls(string key)
        {


            LastPlayerX = PlayerX;
            LastPlayerY = PlayerY;
            PlayerMovement(key);
            return true;
        }
    }
}
