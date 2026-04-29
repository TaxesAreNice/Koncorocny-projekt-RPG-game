using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class MapBlocks_Insides : Grid
    {

      public enum BlockType
        {
            Empty,
            Item,
            Items,
            Enemy
        }
        public enum LeftWallType
        {
            None,
            Wall,
            Door
        }
        public enum RightWallType
        {
            None,
            Wall,
            Door
        }
        public enum UpperWallType
        {
            None,
            Wall,
            Door
        }
        public enum DownerWallType
        {
            None,
            Wall,
            Door
        }

        public string current_Left_Wall_Texture = "";
        public string current_Right_Wall_Texture = "";
        public string current_Upper_Wall_Texture = "";
        public string current_Downer_Wall_Texture = "";
        public string current_Flore_Texture = "";
        public string current_item_Texture = "";
        public string current_Enemy_Texture = "";

        public BlockType block_type = BlockType.Empty;
        public LeftWallType left_wall = LeftWallType.None;
        public RightWallType right_wall = RightWallType.None;
        public UpperWallType upper_wall = UpperWallType.None;
        public DownerWallType downer_wall = DownerWallType.None;


        
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
        };
        public Image Flore = new Image()
        {
            Height = 100,
            Width = 100,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        public MapBlocks_Insides(int box_position)
            {
            Height = 100;
            Width = 100;
            Margin = new Thickness(box_position, 5, 5, 5); /// change somethin' here
            Background = Brushes.DarkGray;

            Children.Add(Flore);
            Children.Add(Left_wall);
            Children.Add(Right_wall);
            Children.Add(Upper_wall);
            Children.Add(Downer_wall);
          
        }
    }
}
