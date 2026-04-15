using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Inventory_Buttons : Grid
    {
        private int box_position = 5;
        private InventoryInputs inventoryMovementClass;
        private List<string> names = new List<string>() { "Hemlet:", "Chestplate:", "Leggins:", "Boots:", "Sword:", "Ring:", "2nd hand:", "Accesory:" };

        public Inventory_Buttons(InventoryInputs inventoryMovementClass, int list_pos)
        {
            Height = 400;
            Width = 215;
            Margin = new System.Windows.Thickness(2);
            Background = System.Windows.Media.Brushes.Gray;
            VerticalAlignment = System.Windows.VerticalAlignment.Top;

            /*
            var tempButton1 = new Button()
            {
                Height = 100,
                Width = 100,
                FontSize = 30,
                Margin = new System.Windows.Thickness(5),
                Background = System.Windows.Media.Brushes.DarkGray,
                Content = "Use",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Top
                
            };
            */
            
            for (int i = 0; i < 4; i++)
            {

                var tempLabel = new Label()
                {
                    Height = 95,
                    Width = 100,
                    FontSize = 20,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(5, box_position, 5, 5),
                    Background = System.Windows.Media.Brushes.DarkGray,
                    Content = names[list_pos],
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left

                };
                var tempButton = new Label()
                {
                    Height = 95,
                    Width = 100,
                    FontSize = 30,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new System.Windows.Thickness(110, box_position, 5, 5),
                    Background = System.Windows.Media.Brushes.DarkGray,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                };

                tempButton.MouseDown += (s, e) =>
                {
                    inventoryMovementClass.Equip_Pressed("faf");
                };

                //Children.Add(tempButton1);
                Children.Add(tempButton);
                Children.Add(tempLabel);
                box_position += 100;
                list_pos++;
            }
        }
    }
}
