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

        // Constructor: This runs when you create the inventory (e.g., var myInv = new Inventory();)
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
                Name = "Scythe",
                Type = ItemType.Wearable,
                Description = "You will attack enemy from behind and ignore his defense",
                Attack = 25,
            });

            Items.Add(new Item
            {
                Name = "Phoenix Feather",
                Type = ItemType.Support,
                Description = "Automatically revives user upon dying",
                Revive = true,
            });

            Items.Add(new Item
            {
                Name = "BloodThorn Sword",
                Type = ItemType.Wearable,
                Description = "Every attack will give user a 30% of users damage",
                LifeLeech = 30,
            });

            Items.Add(new Item
            {
                Name = "Breaker Ring",
                Type = ItemType.Wearable,
                Description = "Instantly breaks through enemy defenses",
                EnemyDefense = 0,
            });

            Items.Add(new Item { Name = "Knight Helmet", Type = ItemType.Wearable, Description = "+4 defense", Defense = 4 });
            Items.Add(new Item { Name = "Knight Chestplate", Type = ItemType.Wearable, Description = "+6 defense", Defense = 6 });
            Items.Add(new Item { Name = "Knight Legplates", Type = ItemType.Wearable, Description = "+5 defense", Defense = 5 });
            Items.Add(new Item { Name = "Knight Boots", Type = ItemType.Wearable, Description = "+4 defense", Defense = 4 });
            Items.Add(new Item { Name = "Knight Sword", Type = ItemType.Wearable, Description = "+15 attack", Attack = 13 });

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
            Items.Add(new Item { Name = "Mana Potion", Type = ItemType.Support, Description = "Restores 25 mana", Mana = 25 });
            Items.Add(new Item { Name = "Big Mana Potion", Type = ItemType.Support, Description = "Restores 50 mana", Mana = 50 });
            Items.Add(new Item { Name = "Damage Potion", Type = ItemType.Support, Description = "Increases damage by 15", Attack = 15 });

            Items.Add(new Item { Name = "Fireball Scroll", Type = ItemType.FightOnly, Description = "Deals 25 damage to a single enemy", Attack = 25 });
            Items.Add(new Item { Name = "Cursed Scroll", Type = ItemType.FightOnly, Description = "Used on opponent causes him to lose 50% of his attack", Weaken = true });
            Items.Add(new Item { Name = "Lightning Scroll", Type = ItemType.AoE, Description = "Deals 20 damage to all enemies", AoEDamage = 20 });
            Items.Add(new Item { Name = "Healing Scroll", Type = ItemType.FightOnly, Description = "Heals 50HP", Heal = 50 });
            Items.Add(new Item { Name = "Shield Scroll", Type = ItemType.FightOnly, Description = "Gives user 20 defense", Defense = 20 });
            Items.Add(new Item { Name = "Revive Scroll", Type = ItemType.FightOnly, Description = "Automatically revives user upon dying", Revive = true });

            Items.Add(new Item { Name = "Dragon Helmet", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Chestplate", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Leggins", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Dragon Boots", Type = ItemType.Wearable, Description = "+10 defense", Defense = 10 });
            Items.Add(new Item { Name = "Fire Breath Orb", Type = ItemType.AoE, Description = "Does 25 AoE damage", AoEDamage = 25 });
            Items.Add(new Item { Name = "Guardian Amulet", Type = ItemType.Wearable, Description = "+30 defense", Defense = 30 });

            Items.Add(new Item { Name = "Rusty Helmet", Type = ItemType.Wearable, Description = " +1 defense", Defense = 1 });
            Items.Add(new Item { Name = "Rusty Chestplate", Type = ItemType.Wearable, Description = " +2 defense", Defense = 2 });
            Items.Add(new Item { Name = "Rusty Legplates", Type = ItemType.Wearable, Description = " +2 defense", Defense = 2 });
            Items.Add(new Item { Name = "Rusty Boots", Type = ItemType.Wearable, Description = " +1 defense", Defense = 1 });

            Items.Add(new Item { Name = "Dull Blade", Type = ItemType.Wearable, Description = "Just some dull blade", Attack = 5 });
            Items.Add(new Item { Name = "Iron Sword", Type = ItemType.Wearable, Description = "A sturdy iron sword", Attack = 10 });
            Items.Add(new Item { Name = "Steel Sword", Type = ItemType.Wearable, Description = "A sharp steel sword", Attack = 15 });
            Items.Add(new Item { Name = "Diamond Sword", Type = ItemType.Wearable, Description = "A powerful diamond sword", Attack = 20 });
            Items.Add(new Item { Name = "Excalibur", Type = ItemType.Wearable, Description = "The legendary sword of King Arthur", Attack = 50 });

            Items.Add(new Item { Name = "Steel Helmet", Type = ItemType.Wearable, Description = " +5 defense", Defense = 5 });
            Items.Add(new Item { Name = "Steel Chestplate", Type = ItemType.Wearable, Description = " +8 defense", Defense = 8 });
            Items.Add(new Item { Name = "Steel Legplates", Type = ItemType.Wearable, Description = " +7 defense", Defense = 7 });
            Items.Add(new Item { Name = "Steel Boots", Type = ItemType.Wearable, Description = " +5 defense", Defense = 5 });

            Items.Add(new Item { Name = "Iron Helmet", Type = ItemType.Wearable, Description = " +3 defense", Defense = 3 });
            Items.Add(new Item { Name = "Iron Chestplate", Type = ItemType.Wearable, Description = " +5 defense", Defense = 5 });
            Items.Add(new Item { Name = "Iron Legplates", Type = ItemType.Wearable, Description = " +4 defense", Defense = 4 });
            Items.Add(new Item { Name = "Iron Boots", Type = ItemType.Wearable, Description = " +3 defense", Defense = 3 });
        }
    }
}