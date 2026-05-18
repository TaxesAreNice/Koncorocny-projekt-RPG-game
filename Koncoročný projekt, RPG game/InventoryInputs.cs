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
       // private Player player = new Player();
        private ItemTypes itemTypes = new ItemTypes();
        private Inventoryy Items = new Inventoryy();

        public int chosed_x = 0;
        public int chosed_y = 0;

        private int PlayerBackUpSwordAttack = 0;
        private int PlayerBackUpHelmetDefence = 0;
        private int PlayerBackUpChestplateDefence = 0;
        private int PlayerBackUpLegginsDefence = 0;
        private int PlayerBackUpBootsDefence = 0;
        private int PlayerBackUpAccessoryDefence = 0;
        private int EnemyBackUpRingDefense = 0;


        ///Helmet", "Chestplate", "Leggins", "Boots

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
       

        public void SettingWearablesBack(Player player, string category)
        {
            //player.PlayerDefense = PlayerBackUpDefense;
            // player.PlayerAttack = PlayerBackUpAttack;
            if (!category.Contains("_"))
            {
                foreach (Item item in Items.Items)
                {
                    string justInCaseEHEMWEARABLES = "";
                    string ItemName = item.Name;
                    if (item.Name.Contains("_"))
                    {
                        ItemName = item.Name.Split("_")[0];
                        justInCaseEHEMWEARABLES = "_" + item.Name.Split("_")[1];

                    }
                    if (ItemName == category)
                    {
                        category = justInCaseEHEMWEARABLES;
                    }
                }
                if (!category.Contains("_"))
                {
                    return;
                }
            }

                if (category == "_Sword")
                {
                    player.PlayerAttack -= PlayerBackUpSwordAttack;
                }
                else if (category == "_Helmet")
                {
                    player.PlayerDefense -= PlayerBackUpHelmetDefence;
                }
                else if (category == "_Chestplate")
                {
                    player.PlayerDefense -= PlayerBackUpChestplateDefence;
                }
                else if (category == "_Leggins")
                {
                    player.PlayerDefense -= PlayerBackUpLegginsDefence;
                }
                else if (category == "_Boots")
                {
                    player.PlayerDefense -= PlayerBackUpBootsDefence;
                }
                else if (category == "_Accessory")
                {
                    player.PlayerDefense -= PlayerBackUpAccessoryDefence;
                }
            }
              
            
        

        // Update the signature to accept the live game objects
        public string E_Pressed(string name, Player realPlayer, Monster realMonster, Fighting realFight)
        {
            if (q_pressed)
            {



                return "AddItem_" + name;
            }
            else
            {
                return CheckingItemType(name, realPlayer, realMonster, realFight);
            }
        }
            

        private string CheckingItemType(string name, Player p, Monster m, Fighting f)
        {
            string returner = "Item not found";

            foreach (Item item in Items.Items)
            {
                if (item.Name == "Rusty Helmet_Helmet") // for testing, yup
                {
                    // fahhh
                }
                string justInCaseEHEMWEARABLES = "";
                string ItemName = item.Name;
                if (item.Name.Contains("_"))
                {
                    ItemName = item.Name.Split("_")[0];
                    justInCaseEHEMWEARABLES = "_" + item.Name.Split("_")[1];
                }
                if (ItemName == name)
                {
                    
                    if (item.Type == ItemTypes.ItemType.Wearable)
                    {
                        // PlayerBackUpDefense = p.PlayerDefense;
                        //PlayerBackUpAttack = p.PlayerAttack;
                        SavingBackUpStats(p,m, justInCaseEHEMWEARABLES, item);

                        returner =  name + justInCaseEHEMWEARABLES;  
                        item.UseItem(p, m, f);
                        break;
                    }

                    returner = item.Type.ToString();

                    
                    item.UseItem(p, m, f);
                    break;
                }
            }
            return returner;
        }

        private void SavingBackUpStats(Player p, Monster m, string type, Item item)
        {
            List<string> categories = new List<string>() { "Helmet", "Chestplate", "Leggins", "Boots", "Sword", "Ring", "2nd hand", "Accessory" };

            foreach (string category in categories)
            {
                if (type == "_" + category)
                {
                    switch (category)
                    {
                        case "Helmet":
                            PlayerBackUpHelmetDefence = item.Defense;
                            break;
                        case "Chestplate":
                            PlayerBackUpChestplateDefence = item.Defense; 
                            break;
                        case "Leggins":
                            PlayerBackUpLegginsDefence = item.Defense; 
                            break;
                        case "Boots":
                            PlayerBackUpBootsDefence = item.Defense; 
                            break;
                        case "Sword":
                            PlayerBackUpSwordAttack = item.Attack;
                            break;
                       case "Ring":
                            EnemyBackUpRingDefense = item.EnemyDefense;
                            break;
                        case "2nd hand":
                            //uhh pshhhh
                            break;
                        case "Accessory":
                            PlayerBackUpAccessoryDefence = item.Defense;
                            break;
                    }
                }
            }
        }
        public void Equip_Pressed(string name, int pos) // equips items
        {
            chosed_But_x = pos;
            q_pressed = true;

            slot_pressed = false;

            
        }

        public void Pressed(int x, int y)
        {
            chosed_x = x;
            chosed_y = y;
            slot_pressed = true;

            q_pressed = false;
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