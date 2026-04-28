using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace Koncoročný_projekt__RPG_game
{
    internal class InventoryInputs
    {
        private ItemTypes itemTypes = new ItemTypes();


        public int chosed_x = 0;
        public int chosed_y = 0;

        public int chosed_But_x = 0;
        public int backup_chosed_But_x = 0;

        public int backup_chosed_x = 0;
        public int backup_chosed_y = 0;

        public int ender_x = 0;
        public int ender_y = 0;

        public int backup_ender_x = 0;
        public int backup_ender_y = 0;

        private List<(int x, int y)> inventory_holes = new List<(int x, int y)>() ;

        private bool first_ender = true;

        public bool slot_pressed = false;
        public bool q_pressed = false;

        private List<List<string>> inventory = new List<List<string>>();

        public int tester_i = 0;
        public string tester = "";

        public InventoryInputs()
        {
            // Pre-fill the 7x5 grid with empty strings
            for (int y = 0; y < 7; y++)
            {
                inventory.Add(new List<string>());
                for (int x = 0; x < 5; x++)
                {
                    inventory[y].Add("");
                }
            }
        }
        public void Q_Pressed() // removes items
        {
         


        }
        public void E_Pressed(string name) // uses items
        {
            checkingItemType(name);


        }

        private string checkingItemType(string name)
        {
            string returner = "";




            return returner;
        }
        public void Equip_Pressed(string name, int pos) // equips items
        {
            chosed_But_x = pos;
            q_pressed = true;
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

        public void PressedTick_Q()
        {
            backup_chosed_But_x = chosed_But_x;
        }
        public void FixingHolesXandYs()
        {
            ender_x = backup_ender_x;
            ender_y = backup_ender_y;
        }
        public string CheckingForYs(string item_name)
        {
            // 1. Check for holes first
            if (CheckingForHoles())
            {
                inventory[ender_y][ender_x] = item_name;
                return "hole";
            }

            // 2. Standard placement: 
            // We do NOT increment ender_x here anymore. 
            // We increment it AFTER the UI has placed the item.
            if (ender_y >= 7) return "inventory_full";

            inventory[ender_y][ender_x] = item_name;
            return "normal";
        }

        // Add this new helper to move the pointer forward ONLY when we are NOT filling a hole
        public void MovePointerForward()
        {
            ender_x++;
            if (ender_x > 4)
            {
                ender_x = 0;
                ender_y++;
            }
        }
        private bool CheckingForHoles()
        {
            if (inventory_holes.Count > 0)
            {
                // Get the earliest hole
                var lowest = inventory_holes.MinBy(p => (p.y, p.x));

                // ONLY fill the hole if it is actually behind the current "ender" position
                // This prevents the "jumping" bug at the end of the list
                if (lowest.y < ender_y || (lowest.y == ender_y && lowest.x < ender_x))
                {
                    backup_ender_x = ender_x;
                    backup_ender_y = ender_y;

                    ender_x = lowest.x;
                    ender_y = lowest.y;

                    inventory_holes.Remove(lowest);
                    return true;
                }
                else
                {
                    // If the "hole" is actually at or past the ender, it's not a hole anymore
                    inventory_holes.Remove(lowest);
                }
            }
            return false;
        }
        public void ClearSlot(int x, int y)
        {
            inventory[y][x] = "";

            // Convert coordinates to a single number to check if it's the last item
            int deletedIdx = (y * 5) + x;
            int enderIdx = (ender_y * 5) + ender_x;

            // If we deleted the item right behind the 'next' pointer
            if (deletedIdx == enderIdx - 1)
            {
                ender_x--;
                if (ender_x < 0)
                {
                    if (ender_y > 0) { ender_y--; ender_x = 4; }
                    else { ender_x = 0; }
                }
                // Clean up any hole that might have been registered at this spot
                inventory_holes.RemoveAll(h => h.x == ender_x && h.y == ender_y);
            }
            else
            {
                // It's a middle item, add it to holes
                if (!inventory_holes.Any(h => h.x == x && h.y == y))
                {
                    inventory_holes.Add((x, y));
                }
            }
        }
    }
}