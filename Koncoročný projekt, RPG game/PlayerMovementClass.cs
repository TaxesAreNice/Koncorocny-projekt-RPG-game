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

        public enum DoorPosition
        {
            Up_D,
            Left_D,
            Down_D,
            Right_D,
            None_D
        }
       

        public enum NeighborDoorPosition
        {
            Up_N,
            Left_N,
            Down_N,
            Right_N,
            None_N
        }
        public enum BlockType
        {
            Empty_T,
            Item_T,
            Chest_T,
            NPC_T
        }

        public BlockType blockType = BlockType.Empty_T;
        public DoorPosition doorPos = DoorPosition.None_D;
        public NeighborDoorPosition neighborDoorPos = NeighborDoorPosition.None_N;
        public void DoorOpen_Close(string type, MapBlocks_Insides currentBlock, List<(MapBlocks_Insides door, bool OpenedORClosed, string id)> neighbors, string neighbor_type)
        {
            if (type == "Up")
            {
                if (currentBlock.upper_wall == MapBlocks_Insides.UpperWallType.DoorOpen) { currentBlock.upper_wall = MapBlocks_Insides.UpperWallType.DoorClosed; }
                else if (currentBlock.upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed) { currentBlock.upper_wall = MapBlocks_Insides.UpperWallType.DoorOpen; }
            }
            else if (type == "Down")
            {
                    if (currentBlock.downer_wall == MapBlocks_Insides.DownerWallType.DoorOpen) { currentBlock.downer_wall = MapBlocks_Insides.DownerWallType.DoorClosed; }
                else if (currentBlock.downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed) { currentBlock.downer_wall = MapBlocks_Insides.DownerWallType.DoorOpen; }
            }
            else if (type == "Left")
            {
                if (currentBlock.left_wall == MapBlocks_Insides.LeftWallType.DoorOpen) { currentBlock.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed; }
                else if (currentBlock.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) { currentBlock.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen; }
            }
            else if (type == "Right")
            {
                if (currentBlock.right_wall == MapBlocks_Insides.RightWallType.DoorOpen) { currentBlock.right_wall = MapBlocks_Insides.RightWallType.DoorClosed; }
                else if (currentBlock.right_wall == MapBlocks_Insides.RightWallType.DoorClosed) { currentBlock.right_wall = MapBlocks_Insides.RightWallType.DoorOpen; }           
            }

            foreach (var neighbor in neighbors)
            {
                MapBlocks_Insides theDude = neighbor.door;
                bool isOpen = neighbor.OpenedORClosed;
                string id = neighbor.id;

                if (isOpen)
                {
                    if (id == "up" ) { theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorClosed; }
                    else if (id == "down") { theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorClosed; }
                    else if (id == "left") { theDude.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed; }
                    else if (id == "right") { theDude.right_wall = MapBlocks_Insides.RightWallType.DoorClosed; }
                }
                else
                {
                    if (id == "up") { theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorOpen; }
                    else if (id == "down") { theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorOpen; }
                    else if (id == "left") { theDude.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen; }
                    else if (id == "right") { theDude.right_wall = MapBlocks_Insides.RightWallType.DoorOpen; }
                }
            }
            //if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.DoorOpen) { neighbor.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed; }
           // else if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) { neighbor.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen; }
        }
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
                    if (current.upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed) return false;
                    if (neighbor != null && (neighbor.downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed) ) return false;

                    break;

                case "A":
                    if (PlayerX <= 0) return false;
                    if (current.left_wall == MapBlocks_Insides.LeftWallType.Wall) return false;
                    if (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.Wall) return false;
                    break;

                case "S":
                    if (PlayerY >= MAX_y) return false;
                    if (current.downer_wall == MapBlocks_Insides.DownerWallType.Wall  ) return false;
                    if (current.downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed) return false;
                    if (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.Wall ) return false;
                    if (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed) return false;
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
        public void CheckingForEPrompts(MapBlocks_Insides the_area, MapBlocks_Insides neighbor)
        {
            if (the_area.block_type != MapBlocks_Insides.BlockType.Empty)
            {
                blockType = the_area.block_type switch
                {
                    MapBlocks_Insides.BlockType.Item => BlockType.Item_T,
                    MapBlocks_Insides.BlockType.Chest => BlockType.Chest_T,
                    MapBlocks_Insides.BlockType.NPC => BlockType.NPC_T,
                    _ => BlockType.Empty_T
                };
            }
            if (the_area.left_wall != MapBlocks_Insides.LeftWallType.None)
            {
                doorPos = DoorPosition.Left_D;
            }
            else if (the_area.upper_wall != MapBlocks_Insides.UpperWallType.None)
            {
                doorPos = DoorPosition.Up_D;
            }
            else if (the_area.right_wall != MapBlocks_Insides.RightWallType.None)
            {
                doorPos = DoorPosition.Right_D;
            }
            else if (the_area.downer_wall != MapBlocks_Insides.DownerWallType.None)
            {
                doorPos = DoorPosition.Down_D;
            }

            if (neighbor != null)
            {
                if (neighbor.left_wall != MapBlocks_Insides.LeftWallType.None)
                {
                    doorPos = DoorPosition.Left_D;
                }
                else if (neighbor.upper_wall != MapBlocks_Insides.UpperWallType.None)
                {
                    doorPos = DoorPosition.Up_D;
                }
                else if (neighbor.right_wall != MapBlocks_Insides.RightWallType.None)
                {
                    doorPos = DoorPosition.Right_D;
                }
                else if (neighbor.downer_wall != MapBlocks_Insides.DownerWallType.None)
                {
                    doorPos = DoorPosition.Down_D;
                }
            }
            //check if in any pos of this there is a prompt, if yes, put it in a list... 
            //then if theres more, just put some extra inputs on the prompts.. 
            // then have this methode return the list of extra inputs.. or just one and the positions of the prompts..
        }

 
        }
    }

