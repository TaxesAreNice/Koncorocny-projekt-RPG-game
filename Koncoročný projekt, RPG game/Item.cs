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
    public class Item
    {
        Fighting fighting = new Fighting();
        Monster monster = new Monster();
        Player player = new Player();
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


        public void UseItem()
        {
            if (Type == ItemType.FightOnly)
            {
                monster.MonsterHP -= Attack - monster.MonsterDefense;
                player.PlayerDefense += Defense;
                player.PlayerAttack += Attack;
                monster.MonsterDefense = EnemyDefense;
            }
            else if (Type == ItemType.Wearable)
            {
                player.PlayerDefense += Defense;
                player.PlayerAttack += Attack;
                monster.MonsterDefense = EnemyDefense;
            }
            else if (Type == ItemType.Support)
            {
                player.PlayerHP += Heal;
                player.PlayerMana += Mana;
                player.PlayerDefense += Defense;
                player.PlayerAttack += Attack;
                monster.MonsterDefense = EnemyDefense;
            }
            else if (Revive == true)
            {
                if(player.PlayerHP <= 0)
                {
                    player.PlayerHP = 100;
                }
                else
                {
                    return;
                }
            }
            else if (LifeLeech > 0)
            {
                player.PlayerHP += player.PlayerDamage * LifeLeech / 100;
            }
            else if (Weaken == true)
            {
                monster.MonsterAttack /= 2;
            }
            switch (Type)
            {
                case ItemType.AoE:
                    foreach (var m in fighting.currentEnemies)
                    {
                        m.EnemyHP -= AoEDamage;
                    }
                    break;
            }
            if (player.PlayerHP > 100)
            {
                player.PlayerHP = 100;
            }
        }
    }
}
