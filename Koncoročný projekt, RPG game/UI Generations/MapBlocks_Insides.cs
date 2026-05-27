using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    public class MapBlocks_Insides : Grid
    {
 
        public enum BlockType { Empty, Item, Chest, NPC }
        public enum LeftWallType { None, Wall, DoorOpen, DoorClosed, RoomDoor }
        public enum RightWallType { None, Wall, DoorOpen, DoorClosed, RoomDoor }
        public enum UpperWallType { None, Wall, DoorOpen, DoorClosed, RoomDoor }
        public enum DownerWallType { None, Wall, DoorOpen, DoorClosed, RoomDoor }


        public BlockData Data { get; set; } = new BlockData();

        // All these short cuts were made by AI... though ONLY the short cuts...
        public string current_Left_Wall_Texture
        {
            get => Data.current_Left_Wall_Texture;
            set => Data.current_Left_Wall_Texture = value;
        }
        public string current_Right_Wall_Texture
        {
            get => Data.current_Right_Wall_Texture;
            set => Data.current_Right_Wall_Texture = value;
        }
        public string current_Upper_Wall_Texture
        {
            get => Data.current_Upper_Wall_Texture;
            set => Data.current_Upper_Wall_Texture = value;
        }
        public string current_Downer_Wall_Texture
        {
            get => Data.current_Downer_Wall_Texture;
            set => Data.current_Downer_Wall_Texture = value;
        }
        public string current_Flore_Texture
        {
            get => Data.current_Flore_Texture;
            set => Data.current_Flore_Texture = value;
        }
        public string current_item_Texture
        {
            get => Data.current_item_Texture;
            set => Data.current_item_Texture = value;
        }
        public string current_Enemy_Texture
        {
            get => Data.current_Enemy_Texture;
            set => Data.current_Enemy_Texture = value;
        }
        public string current_NPC_Texture
        {
            get => Data.current_NPC_Texture;
            set => Data.current_NPC_Texture = value;
        }
        public string current_NPC_Name
        {
            get => Data.current_NPC_Name;
            set => Data.current_NPC_Name = value;
        }
        public string current_Chest_Texture
        {
            get => Data.current_Chest_Texture;
            set => Data.current_Chest_Texture = value;
        }


        public List<string> current_NPC_Lines
        {
            get => Data.current_NPC_Lines;
            set => Data.current_NPC_Lines = value;
        }
        public List<string> current_Chest_Items
        {
            get => Data.current_Chest_Items;
            set => Data.current_Chest_Items = value;
        }
        public List<string> NPC_Enemies
        {
            get => Data.NPC_Enemies;
            set => Data.NPC_Enemies = value;
        }
        public int NPC_Aura
        {
            get => Data.NPC_Aura;
            set => Data.NPC_Aura = value;
        }
        public int NextRoomTeleporter_X
        {
            get => Data.NextRoomTeleporter_X;
            set => Data.NextRoomTeleporter_X = value;
        }
        public int NextRoomTeleporter_Y
        {
            get => Data.NextRoomTeleporter_Y;
            set => Data.NextRoomTeleporter_Y = value;
        }
        public int NextRoomTeleporter_Room
        {
            get => Data.NextRoomTeleporter_Room;
            set => Data.NextRoomTeleporter_Room = value;
        }


        public BlockType block_type
        {
            get => Data.block_type;
            set => Data.block_type = value;
        }
        public LeftWallType left_wall
        {
            get => Data.left_wall;
            set => Data.left_wall = value;
        }
        public RightWallType right_wall
        {
            get => Data.right_wall;
            set => Data.right_wall = value;
        }
        public UpperWallType upper_wall
        {
            get => Data.upper_wall;
            set => Data.upper_wall = value;
        }
        public DownerWallType downer_wall
        {
            get => Data.downer_wall;
            set => Data.downer_wall = value;
        }


        public bool OutofArrayChecker() { return true; }


        public Image Left_wall = new Image()
        {
            Height = 100,
            Width = 20,
            HorizontalAlignment = HorizontalAlignment.Left,
        };
        public Image Right_wall = new Image()
        {
            Height = 100,
            Width = 20,
            HorizontalAlignment = HorizontalAlignment.Right,
        };
        public Image Upper_wall = new Image()
        {
            Height = 20,
            Width = 100,
            VerticalAlignment = VerticalAlignment.Top,
        };
        public Image Downer_wall = new Image()
        {
            Height = 20,
            Width = 100,
            VerticalAlignment = VerticalAlignment.Bottom,
            Margin = new Thickness(0, 0, 0, 10)
        };
        public Image Flore = new Image()
        {
            Height = 100,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        public Image Item = new Image()
        {
            Height = 30,
            Width = 30,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        public Image NPC = new Image()
        {
            Height = 75,
            Width = 75,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        public Image Chest = new Image()
        {
            Height = 70,
            Width = 70,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        public MapBlocks_Insides(int box_position)
        {
            Height = 100;
            Width = 100;
            Margin = new Thickness(box_position, 5, 5, 5);
            Background = Brushes.DarkGray;

            Children.Add(Flore);
            Children.Add(Left_wall);
            Children.Add(Right_wall);
            Children.Add(Upper_wall);
            Children.Add(Downer_wall);
            Children.Add(Item);
            Children.Add(NPC);
            Children.Add(Chest);
        }
    }
}
