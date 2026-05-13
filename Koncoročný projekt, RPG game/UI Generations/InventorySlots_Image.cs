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
    internal class InventorySlots_Image : Grid
    {
        public Image image = new Image()
        {
            Height = 90,
            Width = 90,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        public InventorySlots_Image(int box_position, int xX, int y)
        {
            Height = 100;
            Width = 100;
            Background = Brushes.DarkGray;
            Margin = new Thickness(box_position, 5, 5, 5);
            Name = "_" + xX.ToString() + "_" + y.ToString();
            HorizontalAlignment = HorizontalAlignment.Left;

            Children.Add(image);
        }
    }
}
