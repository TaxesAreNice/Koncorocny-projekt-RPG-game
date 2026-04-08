using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Inventory_Slots : Grid
    {
        private int box_position = 5;
        public List<Label> slots = new List<Label>(); //here too
        public List<string> names = new List<string>();

        private InventoryInputs inventoryMovementClass;

        public Inventory_Slots(int y, InventoryInputs inputs)
        {
            this.inventoryMovementClass = inputs;

            Height = 100;
            Width = 530;
            Margin = new Thickness(2);
            Background = Brushes.Gray;
            VerticalAlignment = VerticalAlignment.Top;

            for (int i = 0; i < 5; i++)
            {
                int xX = i;

                var tempSlot = new Label() // change to grid, please
                {
                    Height = 100,
                    Width = 100,
                    FontSize = 30,
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
                names.Add("");

                box_position += 105;
            }
        }
    }
}
