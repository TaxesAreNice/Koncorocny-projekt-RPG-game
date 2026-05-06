using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    internal class PlayerMovementClass // made by Mahutik.. tho changed by ai.. like 20% AI
    {
        public int MAX_x = 11;
        public int MAX_y = 5;
        public int PlayerX = 0;
        public int PlayerY = 0;

        public int Player_Pixel_X = 30;
        public int Player_Pixel_Y = 30;

        public int LastPlayerX = 0;
        public int LastPlayerY = 0;

        private void PlayerMovement(string key)
        {
            LastPlayerX = PlayerX;
            LastPlayerY = PlayerY;

            switch (key)
            {
                case "W":
                    PlayerY -= 1;
                    Player_Pixel_Y -= 100; 
                    break;
                case "A":
                    PlayerX -= 1;
                    Player_Pixel_X -= 105;
                    break;
                case "S":
                    PlayerY += 1;
                    Player_Pixel_Y += 100;
                    break;
                case "D":
                    PlayerX += 1;
                    Player_Pixel_X += 105;
                    break;
            }
        }

        public bool CheckingForWalls(string key, MapBlocks_Insides current, MapBlocks_Insides neighbor) 
        {
            switch (key)
            {
                case "W":
                    if (PlayerY <= 0) return false; 
                    if (current.upper_wall == MapBlocks_Insides.UpperWallType.Wall) return false;
                    if (neighbor != null && neighbor.downer_wall == MapBlocks_Insides.DownerWallType.Wall) return false;
                    break;

                case "A":
                    if (PlayerX <= 0) return false;
                    if (current.left_wall == MapBlocks_Insides.LeftWallType.Wall) return false;
                    if (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.Wall) return false;
                    break;

                case "S":
                    if (PlayerY >= MAX_y) return false;
                    if (current.downer_wall == MapBlocks_Insides.DownerWallType.Wall) return false;
                    if (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.Wall) return false;
                    break;

                case "D":
                    if (PlayerX >= MAX_x) return false;
                    if (current.right_wall == MapBlocks_Insides.RightWallType.Wall) return false;
                    if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.Wall) return false;
                    break;
            }

            PlayerMovement(key);
            return true;
        }
        public void CheckingForEPrompts(List<MapBlocks_Insides> the_area)
        {
            //check if in any pos of this there is a prompt, if yes, put it in a list... 
            //then if theres more, just put some extra inputs on the prompts.. 
            // then have this methode return the list of extra inputs.. or just one and the positions of the prompts..
        }
    }
}
