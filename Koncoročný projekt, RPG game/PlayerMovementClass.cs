using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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
        public void SetGameImage(Image targetControl, string folder, string insiderFolder, string fileName)
        {
            try
            {
                string path = $"pack://application:,,,/Images/{folder}/{insiderFolder}/{fileName}.png";
                targetControl.Source = new BitmapImage(new Uri(path));
            }
            catch (Exception ex)
            {
                // Helpful if you forget to set an image to "Resource"
                MessageBox.Show($"Failed to load: {fileName}. Error: {ex.Message}");
            }
        }

        public BlockType blockType = BlockType.Empty_T;
        public DoorPosition doorPos = DoorPosition.None_D;
        public NeighborDoorPosition neighborDoorPos = NeighborDoorPosition.None_N;
       
        public void DoorOpen_Close(string type, MapBlocks_Insides currentBlock, List<(MapBlocks_Insides door, bool OpenedORClosed, string id)> neighbors, string neighbor_type)
        {
            if (type == "Up")
            {
                if (currentBlock.upper_wall == MapBlocks_Insides.UpperWallType.DoorOpen)
                {
                    currentBlock.upper_wall = MapBlocks_Insides.UpperWallType.DoorClosed;
                    currentBlock.current_Upper_Wall_Texture = "TopDoor_closed";
                    SetGameImage(currentBlock.Upper_wall, "Blocks", "Top_Walls", "TopDoor_closed");
                }
                else if (currentBlock.upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed)
                {
                    currentBlock.upper_wall = MapBlocks_Insides.UpperWallType.DoorOpen;
                    currentBlock.current_Upper_Wall_Texture = "TopDoor_opened";
                    SetGameImage(currentBlock.Upper_wall, "Blocks", "Top_Walls", "TopDoor_opened");
                }
            }
            else if (type == "Down")
            {
                if (currentBlock.downer_wall == MapBlocks_Insides.DownerWallType.DoorOpen)
                {
                    currentBlock.downer_wall = MapBlocks_Insides.DownerWallType.DoorClosed;
                    currentBlock.current_Downer_Wall_Texture = "DownDoor_closed";
                    SetGameImage(currentBlock.Downer_wall, "Blocks", "Buttom_Walls", "ButtomDoor_closed");
                }
                else if (currentBlock.downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed)
                {
                    currentBlock.downer_wall = MapBlocks_Insides.DownerWallType.DoorOpen;
                    currentBlock.current_Downer_Wall_Texture = "DownDoor_opened";

                    SetGameImage(currentBlock.Downer_wall, "Blocks", "Buttom_Walls", "ButtomDoor_opened");


                }
            }
            else if (type == "Left")
            {
                if (currentBlock.left_wall == MapBlocks_Insides.LeftWallType.DoorOpen)
                {
                    currentBlock.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed;
                    currentBlock.current_Left_Wall_Texture = "LeftDoor_closed";
                    SetGameImage(currentBlock.Left_wall, "Blocks", "Left_Walls", "LeftDoor_closed");
                }
                else if (currentBlock.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed)
                {
                    currentBlock.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen;
                    currentBlock.current_Left_Wall_Texture = "LeftDoor_opened";
                    SetGameImage(currentBlock.Left_wall, "Blocks", "Left_Walls", "LeftDoor_opened");
                }
            }
            else if (type == "Right")
            {
                if (currentBlock.right_wall == MapBlocks_Insides.RightWallType.DoorOpen)
                {
                    currentBlock.right_wall = MapBlocks_Insides.RightWallType.DoorClosed;
                    currentBlock.current_Right_Wall_Texture = "RightDoor_closed";
                    SetGameImage(currentBlock.Right_wall, "Blocks", "Right_Walls", "RightDoor_closed");
                }
                else if (currentBlock.right_wall == MapBlocks_Insides.RightWallType.DoorClosed)
                {
                    currentBlock.right_wall = MapBlocks_Insides.RightWallType.DoorOpen;
                    currentBlock.current_Right_Wall_Texture = "RightDoor_opened";
                    SetGameImage(currentBlock.Right_wall, "Blocks", "Right_Walls", "RightDoor_opened");
                }
            }

            foreach (var neighbor in neighbors)
            {
                MapBlocks_Insides theDude = neighbor.door;
                bool isOpen = neighbor.OpenedORClosed;
                string id = neighbor.id;

                if (isOpen)
                {
                    if (id == "up")
                    {
                        theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorClosed;
                        theDude.current_Upper_Wall_Texture = "TopDoor_closed";
                        SetGameImage(theDude.Upper_wall, "Blocks", "Top_Walls", "TopDoor_closed");
                    }
                    else if (id == "down")
                    {
                        theDude.downer_wall = MapBlocks_Insides.DownerWallType.DoorClosed;
                        theDude.current_Downer_Wall_Texture = "DownDoor_closed";

                        SetGameImage(theDude.Downer_wall, "Blocks", "Buttom_Walls", "ButtomDoor_closed");

                        SetGameImage(theDude.Downer_wall, "Blocks", "Down_Walls", "DownDoor_closed");

                    }
                    else if (id == "left")
                    {
                        theDude.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed;
                        theDude.current_Left_Wall_Texture = "LeftDoor_closed";
                        SetGameImage(theDude.Left_wall, "Blocks", "Left_Walls", "LeftDoor_closed");
                    }
                    else if (id == "right")
                    {
                        theDude.right_wall = MapBlocks_Insides.RightWallType.DoorClosed;
                        theDude.current_Right_Wall_Texture = "RightDoor_closed";
                        SetGameImage(theDude.Right_wall, "Blocks", "Right_Walls", "RightDoor_closed");
                    }
                }
                else
                {
                    if (id == "up")
                    {
                        theDude.upper_wall = MapBlocks_Insides.UpperWallType.DoorOpen;
                        theDude.current_Upper_Wall_Texture = "TopDoor_opened";
                        SetGameImage(theDude.Upper_wall, "Blocks", "Top_Walls", "TopDoor_opened");
                    }
                    else if (id == "down")
                    {
                        theDude.downer_wall = MapBlocks_Insides.DownerWallType.DoorOpen;
                        theDude.current_Downer_Wall_Texture = "DownDoor_opened";

                        SetGameImage(theDude.Downer_wall, "Blocks", "Buttom_Walls", "ButtomDoor_opened");

                        SetGameImage(theDude.Downer_wall, "Blocks", "Down_Walls", "DownDoor_opened");

                    }
                    else if (id == "left")
                    {
                        theDude.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen;
                        theDude.current_Left_Wall_Texture = "LeftDoor_opened";
                        SetGameImage(theDude.Left_wall, "Blocks", "Left_Walls", "LeftDoor_opened");
                    }
                    else if (id == "right")
                    {
                        theDude.right_wall = MapBlocks_Insides.RightWallType.DoorOpen;
                        theDude.current_Right_Wall_Texture = "RightDoor_opened";
                        SetGameImage(theDude.Right_wall, "Blocks", "Right_Walls", "RightDoor_opened");
                    }
                }
            }
        }



            //if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.DoorOpen) { neighbor.left_wall = MapBlocks_Insides.LeftWallType.DoorClosed; }
           // else if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) { neighbor.left_wall = MapBlocks_Insides.LeftWallType.DoorOpen; }
        

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
                    if (current.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) return false;
                    if (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.DoorClosed) return false;
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
                    if (current.right_wall == MapBlocks_Insides.RightWallType.DoorClosed) return false;
                    if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) return false;
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

        internal string CheckingForRoomDoors(string key, MapBlocks_Insides current, MapBlocks_Insides neighbor)
        {
            switch (key)
            {
                case "W":
                    if (PlayerY <= 0) return "nope";
                    if (current.upper_wall == MapBlocks_Insides.UpperWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.downer_wall == MapBlocks_Insides.DownerWallType.RoomDoor) return "N";
                    if (current.upper_wall == MapBlocks_Insides.UpperWallType.RoomDoor) return "C";
                    if (neighbor != null && (neighbor.downer_wall == MapBlocks_Insides.DownerWallType.RoomDoor)) return "N";

                    break;

                case "A":
                    if (PlayerX <= 0) return "nope"; 
                    if (current.left_wall == MapBlocks_Insides.LeftWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.RoomDoor) return "N";
                    if (current.left_wall == MapBlocks_Insides.LeftWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.right_wall == MapBlocks_Insides.RightWallType.RoomDoor) return "N";
                    break;

                case "S":
                    if (PlayerY >= MAX_y) return "nope";
                    if (current.downer_wall == MapBlocks_Insides.DownerWallType.RoomDoor) return "C";
                    if (current.downer_wall == MapBlocks_Insides.DownerWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.RoomDoor) return "N";
                    if (neighbor != null && neighbor.upper_wall == MapBlocks_Insides.UpperWallType.RoomDoor) return "N";
                    break;

                case "D":
                    if (PlayerX >= MAX_x) return "nope";
                    if (current.right_wall == MapBlocks_Insides.RightWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.RoomDoor) return "N";
                    if (current.right_wall == MapBlocks_Insides.RightWallType.RoomDoor) return "C";
                    if (neighbor != null && neighbor.left_wall == MapBlocks_Insides.LeftWallType.RoomDoor) return "N";
                    break;
            }

            return "nope";

        }
    }
    }

