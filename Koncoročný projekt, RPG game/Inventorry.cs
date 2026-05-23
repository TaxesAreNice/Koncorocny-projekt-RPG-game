using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Koncoročný_projekt__RPG_game.ItemTypes;

namespace Koncoročný_projekt__RPG_game
{
    internal class Inventoryy
    {
        public List<Item> Items { get; set; } = new List<Item>();

        
        public Inventoryy()
        {
            LoadDefaultItems();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        private void LoadDefaultItems()
        {
            Items.Add(new Item
            {
                Name = "Phoenix Feather",
                Type = ItemType.Support,
                Description = "Automatically revives user upon dying",
                Revive = true,
            });

            Items.Add(new Item
            {
                Name = "Breaker Ring_Ring",
                Type = ItemType.Wearable,
                Description = "Instantly breaks through enemy defenses",
                EnemyDefense = 0,
            });

            Items.Add(new Item { Name = "Knight Helmet_Helmet", Type = ItemType.Wearable, Description = "+4 defense", Defense = 4 });
            Items.Add(new Item { Name = "Knight Chestplate_Chestplate", Type = ItemType.Wearable, Description = "+6 defense", Defense = 6 });
            Items.Add(new Item { Name = "Knight Leggins_Leggins", Type = ItemType.Wearable, Description = "+5 defense", Defense = 5 });
            Items.Add(new Item { Name = "Knight Boots_Boots", Type = ItemType.Wearable, Description = "+4 defense", Defense = 4 });
            Items.Add(new Item { Name = "Knight Sword_Sword", Type = ItemType.Wearable, Description = "+15 attack", Attack = 13 });

            Items.Add(new Item
            {
                Name = "Mythical Meal",
                Type = ItemType.Support,
                Description = "Heals 100% of health and mana",
                Mana = 100,
                Heal = 100,
            });

            Items.Add(new Item { Name = "Health Potion", Type = ItemType.Support, Description = "Heals 25HP", Heal = 25 });
            Items.Add(new Item { Name = "Big Health Potion", Type = ItemType.Support, Description = "Heals 50HP", Heal = 50 });
            Items.Add(new Item { Name = "Damage Potion", Type = ItemType.Support, Description = "Increases damage by 15", Attack = 15 });

            Items.Add(new Item { Name = "Dragon Helmet_Helmet", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Chestplate_Chestplate", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Leggins_Leggins", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Boots_Boots", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Fire Breath Orb", Type = ItemType.AoE, Description = "Does 25 AoE damage", AoEDamage = 25 });
            Items.Add(new Item { Name = "Guardian Amulet_Accessory", Type = ItemType.Wearable, Description = "+30 defense", Defense = 30 });

            Items.Add(new Item { Name = "Rusty Helmet_Helmet", Type = ItemType.Wearable, Description = " +1 defense", Defense = 1 });
            Items.Add(new Item { Name = "Rusty Chestplate_Chestplate", Type = ItemType.Wearable, Description = " +2 defense", Defense = 2 });
            Items.Add(new Item { Name = "Rusty Leggins_Leggins", Type = ItemType.Wearable, Description = " +2 defense", Defense = 2 });
            Items.Add(new Item { Name = "Rusty Boots_Boots", Type = ItemType.Wearable, Description = " +1 defense", Defense = 1 });

            Items.Add(new Item { Name = "Dull Blade_Sword", Type = ItemType.Wearable, Description = "Just some dull blade", Attack = 5 });
            Items.Add(new Item { Name = "Steel Sword_Sword", Type = ItemType.Wearable, Description = "A sharp steel sword", Attack = 15 });
            Items.Add(new Item { Name = "Dragon Sword_Sword", Type = ItemType.Wearable, Description = "A powerful dragon sword", Attack = 20 });
            Items.Add(new Item { Name = "Excalibur_Sword", Type = ItemType.Wearable, Description = "The legendary sword of King Arthur", Attack = 50 });

            Items.Add(new Item { Name = "Steel Helmet_Helmet", Type = ItemType.Wearable, Description = " +5 defense", Defense = 5 });
            Items.Add(new Item { Name = "Steel Chestplate_Chestplate", Type = ItemType.Wearable, Description = " +8 defense", Defense = 8 });
            Items.Add(new Item { Name = "Steel Leggins_Leggins", Type = ItemType.Wearable, Description = " +7 defense", Defense = 7 });
            Items.Add(new Item { Name = "Steel Boots_Boots", Type = ItemType.Wearable, Description = " +5 defense", Defense = 5 });
        }
    }
}