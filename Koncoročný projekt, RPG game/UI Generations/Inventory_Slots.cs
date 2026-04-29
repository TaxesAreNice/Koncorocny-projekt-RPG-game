using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Inventory_Slots : Grid
    {
        private int box_position = 5;
        public List<InventorySlots_Image> slots = new List<InventorySlots_Image>(); //here too
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

                InventorySlots_Image tempSlot = new InventorySlots_Image(box_position, xX, y); // change to grid, please
              

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
