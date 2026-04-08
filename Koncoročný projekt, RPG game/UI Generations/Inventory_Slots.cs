using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Inventory_Slots : Grid
    {
        private int box_position = 5;
        public List<Grid> slots = new List<Grid>();

        private InventoryInputs inventoryMovementClass;

        public Inventory_Slots(int y, InventoryInputs inputs)
        {
            this.inventoryMovementClass = inputs;

            Height = 100;
            Width = 1055;
            Margin = new Thickness(2);
            Background = Brushes.Gray;
            VerticalAlignment = VerticalAlignment.Top;

            for (int i = 0; i < 10; i++)
            {
                int xX = i;

                var tempSlot = new Grid()
                {
                    Height = 100,
                    Width = 100,
                    Margin = new Thickness(box_position, 5, 5, 5),
                    Background = Brushes.DarkGray,
                    Name = "_" + xX.ToString() + "_" + y.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                tempSlot.MouseDown += (s, e) =>
                {
                    inventoryMovementClass.Pressed(xX, y);
                };

                Children.Add(tempSlot);
                slots.Add(tempSlot);

                box_position += 105;
            }
        }
    }
}
