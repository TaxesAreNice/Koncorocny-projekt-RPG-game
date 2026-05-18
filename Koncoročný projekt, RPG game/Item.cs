using System;
using System.Collections.Generic;
using static Koncoročný_projekt__RPG_game.ItemTypes;

namespace Koncoročný_projekt__RPG_game
{
    public class Item
    {
        // Properties
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Heal { get; set; }
        public int EnemyDefense { get; set; }
        public bool Weaken { get; set; }
        public bool Revive { get; set; }
        public int AoEDamage { get; set; }
        public int LifeLeech { get; set; }

        // FIX: We pass the ACTIVE player, monster, and fighting instance into the method
        public void UseItem(Player activePlayer, Monster activeMonster, Fighting activeFight)
        {
            // Support Items (Potions/Meals)
            if (Type == ItemType.Support)
            {
                activePlayer.PlayerHP += Heal;
                activePlayer.PlayerMana += Mana;
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
            }

            // Combat Scrolls / Throwables
            else if (Type == ItemType.FightOnly)
            {
                // Note: We use TakeDamage to ensure logic is consistent
                activeMonster.TakeDamage(Attack - activeMonster.MonsterDefenceStatus);
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
            }

            // Gear (Scythe, Swords, Armor)
            else if (Type == ItemType.Wearable)
            {
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
                // If it's a breaker item, it sets the monster's current defense
                if (EnemyDefense == 0 && Name == "Breaker Ring")
                    activeMonster.MonsterDefenceStatus = 0;
            }

            // Special Effects
            if (LifeLeech > 0)
            {
                activePlayer.PlayerHP += (activePlayer.PlayerDamage * LifeLeech / 100);
            }

            if (Weaken && activeMonster != null)
            {
                activeMonster.MonsterAttack /= 2;
            }

            // Area of Effect Logic
            if (Type == ItemType.AoE && activeFight != null)
            {
                foreach (var enemy in activeFight.currentEnemies)
                {
                    enemy.EnemyHP -= AoEDamage;
                }
            }

            // Global Clamp: Prevent HP from exceeding 100
            if (activePlayer.PlayerHP > 100)
            {
                activePlayer.PlayerHP = 100;
            }
        }
    }
}