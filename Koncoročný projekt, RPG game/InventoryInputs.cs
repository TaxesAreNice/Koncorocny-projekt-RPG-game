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

        private bool first_ender = true;

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

            // notes: You gotta check a list of holes that can be form from E/Q inputs. And check if your last position is farther on the array then the farthest hole. If yes, then ...
            // you will add items to the lowest holes.
            if (ender_x == 0 && ender_y == 0 && first_ender)
            {
                first_ender = false;
                return "first_item";
            }
            else if (ender_y == 6 && ender_x == 4)
            {
                return "inventory_full";
            }
            else if (ender_x == 4)
            {
                ender_x = 0;
                ender_y += 1;
                return "next_y";
            }
            else
            {
                ender_x++;
                return "normal";
            }
        }
    }
}
