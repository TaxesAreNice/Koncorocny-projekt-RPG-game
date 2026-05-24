using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{



    internal class InventoryBlocks_Insides_Chest : Grid
    {
   
        public Image image = new Image()
            {
                Height = 70,
                Width = 70,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            public InventoryBlocks_Insides_Chest(int box_position, int xX, int y)
            {
            
                Height = 80;
                Width = 80;
                Background = Brushes.DarkGray;
                Margin = new Thickness(box_position, 5, 5, 5);
                Name = "_" + xX.ToString() + "_" + y.ToString();
                HorizontalAlignment = HorizontalAlignment.Left;


            Children.Add(image);

        }
       
    }
}
