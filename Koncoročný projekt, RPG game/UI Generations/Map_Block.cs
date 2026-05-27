using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    public class Map_Block : Grid
    {
        private Grid Box;
        private int box_position = 5;
        public List<MapBlocks_Insides> blocks = [];

        public Map_Block(int xMap, int yMap, bool fromLeft)
        {
            /*Box = new Grid()
            {
                Height = 100,
                Width = 100,
                Margin = new Thickness(5),
                Background = Brushes.DarkGray
            };
            */

            Height = 100;
            Width = (105 * xMap) + 5; // 1265, 1475
            Margin = new Thickness(2);
            Background = Brushes.Gray;
            
            VerticalAlignment = VerticalAlignment.Top;

            if (fromLeft)
            {
               
                HorizontalAlignment = HorizontalAlignment.Left;
                box_position = 5;
            }
            else
            {
                //box_position = (105 * (14 - xMap)) + 5;
                box_position = 5;
                HorizontalAlignment = HorizontalAlignment.Right;
            }

            for (int i = 0; i < xMap; i++)
            {
                MapBlocks_Insides tempBox = new MapBlocks_Insides(box_position);

                tempBox.HorizontalAlignment = HorizontalAlignment.Left;
                Children.Add(tempBox);
                box_position += 100 + 5;
                blocks.Add(tempBox);
            }
        }
    }
}
