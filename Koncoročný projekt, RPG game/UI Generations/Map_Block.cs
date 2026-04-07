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
    internal class Map_Block : Grid
    {
        private Grid Box;
        private int box_position = 5;
        public List<Grid> blocks = [];
        public Map_Block()
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
            Width = 1265;
            Margin = new Thickness(2);
            Background = Brushes.Gray;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;


            for (int i = 0; i < 12; i++) 
            {
                var tempBox = new Grid()
                {
                    Height = 100,
                    Width = 100,
                    Margin = new Thickness(box_position, 5,5,5), /// change somethin' here
                    Background = Brushes.DarkGray
                };

                tempBox.HorizontalAlignment = HorizontalAlignment.Left;
                Children.Add(tempBox);
                box_position += 100 + 5;
                blocks.Add(tempBox);
            }
        }
    }
}
