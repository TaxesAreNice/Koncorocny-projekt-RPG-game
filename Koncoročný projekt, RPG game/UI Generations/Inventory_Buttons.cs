using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Reflection.Metadata.BlobBuilder;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{
    internal class Inventory_Buttons : Canvas
    {
        private int box_position = 5;
        public List<mhhhINVButtons_insides> slots = new List<mhhhINVButtons_insides>();
        private InventoryInputs inventoryMovementClass;
        public List<string> Names = new List<string>();
        private List<string> names = new List<string>() { "Helmet:", "Chestplate:", "Leggins:", "Boots:", "Sword:", "Ring:", "2nd hand:", "Accessory:" };

        public Inventory_Buttons(InventoryInputs inventoryMovementClass, int list_pos)
        {
            Height = 400;
            Width = 215;
            Margin = new Thickness(2);
            Background = Brushes.Gray;

            for (int i = 0; i < 4; i++)
            {
                int buttonIndex = list_pos; 

                mhhhINVButtons_insides tempButton = new mhhhINVButtons_insides(0, buttonIndex, 0);

                var tempLabel = new Label()
                {
                    Height = 95,
                    Width = 100,
                    FontSize = 15,
                    Background = Brushes.DarkGray,
                    Content = names[buttonIndex]
                };

                tempButton.MouseDown += (s, e) =>
                {
                    inventoryMovementClass.Equip_Pressed(names[buttonIndex], buttonIndex);
                };

               
              
                Canvas.SetLeft(tempLabel, 5);
                Canvas.SetTop(tempLabel, box_position);

             
                Canvas.SetLeft(tempButton, 110);
                Canvas.SetTop(tempButton, box_position);

                Children.Add(tempLabel);
                Children.Add(tempButton);

                slots.Add(tempButton);
                Names.Add("");

                box_position += 100;
                list_pos++;
            }
        }
    }
}
