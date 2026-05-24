using System;
using System.Collections.Generic;
using static Koncoročný_projekt__RPG_game.ItemTypes;

namespace Koncoročný_projekt__RPG_game
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Mana  { get; set; }
        public string Description { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Heal { get; set; }
        public int EnemyDefense { get; set; }
        public bool Weaken { get; set; }
        public bool Revive { get; set; }
        public int AoEDamage { get; set; }

        public void UseItem(Player activePlayer, Monster activeMonster, Fighting activeFight)
        {

            if (Type == ItemType.Support)
            {
                activePlayer.PlayerHP += Heal;
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
            }

            else if (Type == ItemType.FightOnly)
            {
                activeMonster.TakeDamage(Attack - activeMonster.MonsterDefenceStatus);
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
            }

            else if (Type == ItemType.Wearable)
            {
                activePlayer.PlayerDefense += Defense;
                activePlayer.PlayerAttack += Attack;
                if (EnemyDefense == 0 && Name == "Breaker Ring")
                    activeMonster.MonsterDefenceStatus = 0;
            }

            if (Type == ItemType.AoE && activeFight != null)
            {
                foreach (var enemy in activeFight.currentEnemies)
                {
                    enemy.EnemyHP -= AoEDamage;
                }
            }

            if (activePlayer.PlayerHP > 100)
            {
                activePlayer.PlayerHP = 100;
            }
        }
    }
}