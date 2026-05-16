using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class mhhhINVButtons_insides : Grid
    {
        public Image image = new Image()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Stretch = Stretch.Uniform
        };
        public mhhhINVButtons_insides(int box_position, int y, int ff)
        {
            Height = 95;
            Width = 100;
            Background = Brushes.DarkGray;
            Margin = new Thickness(5 + ff, box_position, 5, 5);
            Name = "_" + y.ToString();
            HorizontalAlignment = HorizontalAlignment.Left;

            image.Margin = new Thickness(5);

            Children.Add(image);
        }
    }
}
