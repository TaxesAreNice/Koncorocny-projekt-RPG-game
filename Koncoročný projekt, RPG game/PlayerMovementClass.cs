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

        public int Player_Pixel_X = 30;
        public int Player_Pixel_Y = 30;
        public int player_pixel_cap_X = 1;
        public int player_pixel_cap_Y = 1;

        public int LastPlayerX = 0;
        public int LastPlayerY = 0;

        private void PlayerMovement(string key )
        {
            switch (key)
            {
                case "W":
                    Player_Pixel_Y -= 5; //100
                    if (Player_Pixel_Y > -105 * player_pixel_cap_Y)
                    {
                        player_pixel_cap_Y -= 1;
                        PlayerY -= 1;

                    }
                    
                    break;
                case "A":
                   
                    Player_Pixel_X -= 5; //105
                    if (Player_Pixel_X > -105 * player_pixel_cap_X)
                    {
                        player_pixel_cap_X -= 1;
                        PlayerX -= 1;
                    }
                    break;
                case "S":
                   
                    Player_Pixel_Y += 5; //100
                    if (Player_Pixel_Y > 105 * player_pixel_cap_Y)
                    {
                        player_pixel_cap_Y += 1;
                        PlayerY += 1;
                    }
                    break;
                case "D":
                    
                    Player_Pixel_X += 5; //105
                    if (Player_Pixel_X > 105 * player_pixel_cap_X)
                    {
                        player_pixel_cap_X += 1;
                        PlayerX += 1;
                    }
                    break;
            }

        }
        public bool CheckingForWalls(string key, MapBlocks_Insides bloxy, MapBlocks_Insides neighbor)
        {
            switch (key)
            {
                case "W":
                    // Check my top wall OR neighbor's bottom wall (if neighbor exists)
                    if (bloxy.upper_wall == MapBlocks_Insides.UpperWallType.Wall ||
                       (neighbor != null && neighbor.downer_wall == MapBlocks_Insides.DownerWallType.Wall))
                        return false;
                    // Prevent walking off the map
                    if (PlayerY <= 0) return false;
                    break;

                case "A":
                    if (bloxy.left_wall == MapBlocks_Insides.LeftWallType.Wall ||
                       (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.Wall))
                        return false;
                    if (PlayerX <= 0) return false;
                    break;

                case "S":
                    if (bloxy.downer_wall == MapBlocks_Insides.DownerWallType.Wall ||
                       (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.Wall))
                        return false;
                    if (PlayerY >= MAX_y) return false;
                    break;

                case "D":
                    if (bloxy.right_wall == MapBlocks_Insides.RightWallType.Wall ||
                       (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.Wall))
                        return false;
                    if (PlayerX >= MAX_x) return false;
                    break;
            }

            LastPlayerX = PlayerX;
            LastPlayerY = PlayerY;
            PlayerMovement(key);
            return true;
        }
    }
}
