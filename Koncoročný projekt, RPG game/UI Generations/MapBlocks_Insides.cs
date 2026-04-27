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

      

       public Image Middle = new Image()
        {
            Height = 60,
            Width = 60,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
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


        public MapBlocks_Insides(int box_position)
            {
            Height = 100;
            Width = 100;
            Margin = new Thickness(box_position, 5, 5, 5); /// change somethin' here
            Background = Brushes.DarkGray;

            Children.Add(Left_wall);
            Children.Add(Right_wall);
            Children.Add(Upper_wall);
            Children.Add(Downer_wall);
            Children.Add(Middle);
        }
    }
}
