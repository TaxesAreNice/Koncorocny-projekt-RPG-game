using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class InventoryInputs
    {
        public int chosed_x = 0;
        public int chosed_y = 0;

        public int backup_chosed_x = 0;
        public int backup_chosed_y = 0;

        public int ender_x = 0;
        public int ender_y = 0;

        public bool slot_pressed = false;

        private List<List<string>> inventory = new List<List<string>>();

        public void Q_Pressed() // removes items
        {


     
  
        }
        public void E_Pressed(string name) // uses items
        {




        }

        public void Pressed(int x, int y)
        {
            chosed_x = x;
            chosed_y = y;
            slot_pressed = true;
        }

        public void PressedTick()
        {
            backup_chosed_x = chosed_x;
            backup_chosed_y = chosed_y;
        }
        public string CheckingForYs()
        {
            if (ender_x == 0 && ender_y == 0)
            {
                //raawwhhh
                return "first_item";
            }
            else if (ender_x == 5)
            {
                ender_x = 0;
                ender_y += 1;
                return "next_y";
            }
            else if (ender_y == 7)
            {
                return "inventory_full";
            }
            else
            {
                ender_x++;
                return "normal";
            }
        }
    }
}
