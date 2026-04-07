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
    internal class Inventory_Slots : Grid
    {
        private Grid Slot;
        private int box_position = 5;
        public List<Grid> slots = [];

        public Inventory_Slots()
        {
            Height = 100;
            Width = 1265;
            Margin = new Thickness(2);
            Background = Brushes.Gray;
           // HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;


            for (int i = 0; i < 12; i++)
            {
                var tempSlot = new Grid()
                {
                    Height = 100,
                    Width = 100,
                    Margin = new Thickness(box_position, 5, 5, 5), /// change somethin' here
                    Background = Brushes.DarkGray
                };

                tempSlot.HorizontalAlignment = HorizontalAlignment.Left;
                Children.Add(tempSlot);
                box_position += 100 + 5;
                slots.Add(tempSlot);
            }
        }
    }
}
