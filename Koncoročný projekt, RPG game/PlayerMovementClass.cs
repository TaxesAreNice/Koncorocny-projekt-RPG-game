using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    internal class PlayerMovementClass
    {
        public int MAX_x = 11;
        public int MAX_y = 5;
        public int PlayerX = 0;
        public int PlayerY = 0;

        public int LastPlayerX = 0;
        public int LastPlayerY = 0;

        private void PlayerMovement(string key )
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
        public bool CheckingForWalls(string key, MapBlocks_Insides bloxy, MapBlocks_Insides bloxy2, bool edge)
        {
            if (edge)
            {
                switch (key)
                {
                    case "W":
                        if (bloxy.upper_wall == MapBlocks_Insides.UpperWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "A":
                        if (bloxy.left_wall == MapBlocks_Insides.LeftWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "S":
                        if (bloxy.downer_wall == MapBlocks_Insides.DownerWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "D":
                        if (bloxy.right_wall == MapBlocks_Insides.RightWallType.Wall)
                        {
                            return false;
                        }
                        break;
                }
            }
            else
            {
                switch (key)
                {
                    case "W":
                        if (bloxy.upper_wall == MapBlocks_Insides.UpperWallType.Wall || bloxy2.downer_wall == MapBlocks_Insides.DownerWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "A":
                        if (bloxy.left_wall == MapBlocks_Insides.LeftWallType.Wall || bloxy2.right_wall == MapBlocks_Insides.RightWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "S":
                        if (bloxy.downer_wall == MapBlocks_Insides.DownerWallType.Wall || bloxy2.upper_wall == MapBlocks_Insides.UpperWallType.Wall)
                        {
                            return false;
                        }
                        break;
                    case "D":
                        if (bloxy.right_wall == MapBlocks_Insides.RightWallType.Wall || bloxy2.left_wall == MapBlocks_Insides.LeftWallType.Wall)
                        {
                            return false;
                        }
                        break;
                }
            }

            LastPlayerX = PlayerX;
            LastPlayerY = PlayerY;
            PlayerMovement(key);
            return true;
        }
    }
}
