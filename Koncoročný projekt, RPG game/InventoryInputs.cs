using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

        public int backup_ender_x = 0;
        public int backup_ender_y = 0;

        private List<(int x, int y)> inventory_holes = new List<(int x, int y)>();

        private bool first_ender = true;

        public bool slot_pressed = false;

        private List<List<string>> inventory = new List<List<string>>();

        public int tester_i = 0;
        public string tester = "";

        public void Q_Pressed() // removes items
        {
            try
            {
                if (inventory[chosed_y][chosed_x] == "") { return; }
                inventory_holes.Add((chosed_x, chosed_y));
            }
            catch { }
            //if (inventory[chosed_y][chosed_x] == "") { return; }




        }
        public void E_Pressed(string name) // uses items
        {
            try
            {
                if (inventory[chosed_y][chosed_x] == "") { return; }
                inventory_holes.Add((chosed_x, chosed_y));
            }
            catch { }

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
        public void FixingHolesXandYs()
        {
            ender_x = backup_ender_x;
            ender_y = backup_ender_y;
        }
        public string CheckingForYs(string item_name)
        {
            bool success = false;
            // notes: You gotta check a list of holes that can be form from E/Q inputs. And check if your last position is farther on the array then the farthest hole. If yes, then ...
            // you will add items to the lowest holes.
            success = CheckingForHoles();


            if (success)
            {
                inventory[ender_y][ender_x] = item_name;
                return "hole";
            }
            else if (ender_x == 0 && ender_y == 0 && first_ender)
            {
                first_ender = false;
                inventory.Add(new List<string>());
                inventory[ender_y].Add(item_name);
                return "first_item";
            }
            else if (ender_y == 6 && ender_x == 4)
            {
                return "inventory_full";
            }
            else if (ender_x == 4)
            {
                inventory.Add(new List<string>());
                inventory[ender_y].Add(item_name);
                ender_x = 0;
                ender_y += 1;
                return "next_y";
            }
            else
            {
                inventory[ender_y].Add(item_name);
                ender_x++;
                return "normal";
            }
        }
        private bool CheckingForHoles()
        {
            bool success = false;

            int H_x = 0;
            int H_y = 0;

            int L_x = 0;
            int L_y = 0;

            int indexHighest = 0;
            int indexLowest = 0;

            if (inventory_holes.Count > 0) // this if statement is made by AI
            {
                var highest = inventory_holes.MaxBy(p => (p.y, p.x));
                var lowest = inventory_holes.MinBy(p => (p.y, p.x));

                //indexHighest = inventory_holes.IndexOf(highest);
                indexLowest = inventory_holes.IndexOf(lowest);
                H_x = highest.x;
                H_y = highest.y;

                L_x = lowest.x;
                L_y = lowest.y;

                if (ender_y == H_y && ender_x == H_x)
                {
                    backup_ender_x = ender_x;
                    backup_ender_y = ender_y;

                    ender_x = L_x;
                    ender_y = L_y;

                    inventory_holes.RemoveAt(indexLowest);

                    success = true;
                }
                else if (ender_y > H_y || (ender_y == H_y && ender_x > H_x))
                {

                    backup_ender_x = ender_x;
                    backup_ender_y = ender_y;

                    ender_x = L_x;
                    ender_y = L_y;

                    inventory_holes.RemoveAt(indexLowest);

                    success = true;
                }
            }
            return success;
        }
    }
}