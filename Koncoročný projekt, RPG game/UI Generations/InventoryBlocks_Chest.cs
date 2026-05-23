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
    internal class InventoryBlocks_Chest : Grid
    {
        private int box_position = 5;
        public List<InventoryBlocks_Insides_Chest> slots = new List<InventoryBlocks_Insides_Chest>(); //here too
        public List<string> names = new List<string>();

        private ChestMovementClass chestMovementClass;


        public InventoryBlocks_Chest(int y, ChestMovementClass inputs)
        {
            this.chestMovementClass = inputs;

            Height = 100;
            Width = 950;
            Margin = new Thickness(2);
            Background = Brushes.Gray;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;

            for (int i = 0; i < 11; i++)
            {
                int xX = i;

                InventoryBlocks_Insides_Chest tempSlot = new InventoryBlocks_Insides_Chest(box_position, xX, y); // change to grid, please


                tempSlot.MouseDown += (s, e) =>
                {
                    chestMovementClass.Pressed(xX, y);
                    tempSlot.Background = Brushes.Gray;
                };

                Children.Add(tempSlot);
                slots.Add(tempSlot);
                names.Add("");

                box_position += 85;
            }
        }
    }
}
