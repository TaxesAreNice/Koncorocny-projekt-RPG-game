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
    internal class Inventorry
    {

        public List<Item> Items { get; set; } = new List<Item>();
        public void AddItem(Item item)
        {
            Items.Add(item);
            var Scythe = new Item
            {
                Name = "Scythe",
                Type = ItemType.Wearable,
                Description = "You will attack enemy from behind and ignore his defense",
                Attack = 25,
            };
            var PhoenixFeather = new Item
            {
                Name = "Phoenix Feather",
                Type = ItemType.Support,
                Description = ""
            };
            var BloodthornSword = new Item
            {
                Name = "BloodThorn Sword",
                Type = ItemType.Wearable,
                Description = ""
            };
            var BreakerRing = new Item
            {
                Name = "Breaker Ring",
                Type = ItemType.Wearable,
                Description = "Instantly breaks through enemy defenses",
                EnemyDefense = 0,
            };
            var KnightHelmet = new Item
            {
                Name = "Knight Helmet",
                Type = ItemType.Wearable,
                Description = "+4 defense",
                Defense = 4,
            };
            var KnightChestplate = new Item
            {
                Name = "Knight Chestplate",
                Type = ItemType.Wearable,
                Description = "+6 defense",
                Defense = 6,
            };
            var KnightLeggins = new Item
            {
                Name = "Knight Legplates",
                Type = ItemType.Wearable,
                Description = "+5 defense",
                Defense = 5,
            };
            var KnightBoots = new Item
            {
                Name = "Knight Boots",
                Type = ItemType.Wearable,
                Description = "+3 defense",
                Defense = 3,
            };
            var KnightSword = new Item
            {
                Name = "Knight Sword",
                Type = ItemType.Wearable,
                Description = "+15 attack",
                Attack = 15,
            };
            var MythicalMeal = new Item
            {
                Name = "Mythical Meal",
                Type = ItemType.Support,
                Description = "Heals 100% of health and mana",
                Mana = 100,
                Heal = 100,
            };
            var HealthPotion = new Item
            {
                Name = "Health Potion",
                Type = ItemType.Support,
                Description = "heals 25HP",
                Heal = 25,
            };
            var BigHealthPotion = new Item
            {
                Name = "Big Health Potion",
                Type = ItemType.Support,
                Description = "heals 50HP",
                Heal = 50,
            };
            var ManaPotion = new Item
            {
                Name = "Mana Potion",
                Type = ItemType.Support,
                Description = "Restores 25 mana",
                Mana = 25,
            };
            var BigManaPotion = new Item
            {
                Name = "Big Mana Potion",
                Type = ItemType.Support,
                Description = "Restores 50 mana",
                Mana = 50,
            };
            var DamagePotion = new Item
            {
                Name = "Damage Potion",
                Type = ItemType.Support,
                Description = "Increases damage by 15",
                Attack = 15,
            };
            var FireballScroll = new Item
            {
                Name = "Fireball Scroll",
                Type = ItemType.FightOnly,
                Description = "",
                Attack = 25,
            };
            var CursedScroll = new Item
            {
                Name = "Cursed Scroll",
                Type = ItemType.FightOnly,
                Description = "If used on opponent causes confused effect and opponent will have 100% chance to miss next attack",
            };
            var LightningScroll = new Item
            {
                Name = "Lightning Scroll",
                Type = ItemType.FightOnly,
                Description = "Deals 20 damage to all enemies",
                Attack = 20,
            };
            var HealingScroll = new Item
            {
                Name = "Healing Scroll",
                Type = ItemType.FightOnly,
                Description = "",
            };
            var ShieldScroll = new Item
            {
                Name = "Shield Scroll",
                Type = ItemType.FightOnly,
                Description = ""
            };
            var ReviveScroll = new Item
            {
                Name = "Revive Scroll",
                Type = ItemType.FightOnly,
                Description = ""
            };
            var DragonHelmet = new Item
            {
                Name = "Dragon Helmet",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var DragonChestplate = new Item
            {
                Name = "Dragon Chestplate",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var DragonLeggins = new Item
            {
                Name = "Dragon Legplates",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var DragonBoots = new Item
            {
                Name = "Dragon Boots",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var FireBreathOrb = new Item
            {
                Name = "Fire Breath Orb",
                Type = ItemType.FightOnly,
                Description = "Does 25 AoE damage"
            };
            var GuardianAmulet = new Item
            {
                Name = "Guardian Amulet",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var RustyHelmet = new Item
            {
                Name = "Rusty Helmet",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var RustyRustyChestplate = new Item
            {
                Name = "Rusty Chestplate",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var RustyLeggins = new Item
            {
                Name = "Rusty Legplates",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var RustyBoots = new Item
            {
                Name = "Rusty Boots",
                Type = ItemType.Wearable,
                Description = "Defense += idk"
            };
            var DullBlade = new Item
            {
                Name = "Dull Blade",
                Type = ItemType.Wearable,
                Description = ""
            };
            var IronSword = new Item
            {
                Name = "Iron Sword",
                Type = ItemType.Wearable,
                Description = "",
            };
            var SteelSword = new Item
            {
                Name = "Steel Sword",
                Type = ItemType.Wearable,
                Description = "",
            };
            var DiamondSword = new Item
            {
                Name = "Diamond Sword",
                Type = ItemType.Wearable,
                Description = "",
            };
            var Excalibur = new Item
            {
                Name = "Excalibur",
                Type = ItemType.Wearable,
                Description = "",
            };
            var SteelHelmet = new Item
            {
                Name = "Steel Helmet",
                Type = ItemType.Wearable,
                Description = "Defense += idk",
            };
            var SteelChestplate = new Item
            {
                Name = "Steel Chestplate",
                Type = ItemType.Wearable,
                Description = "Defense += idk",
            };
            var SteelLeggins = new Item
            {
                Name = "Steel Legplates",
                Type = ItemType.Wearable,
                Description = "Defense += idk",
            };
            var SteelBoots = new Item
            {
                Name = "Steel Boots",
                Type = ItemType.Wearable,
                Description = "Defense += idk",
            };
        }
    }
}
