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
        Monster monster = new Monster();
        Player player = new Player();
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public string Description { get; set; } // napr. damage, heal, defense
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Heal { get; set; }
        public int EnemyDefense { get; set; }


        public void UseItem()
        {
            if (Type == ItemType.FightOnly)
            {
                monster.MonsterHP -= Attack - monster.MonsterDefense;
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
            }
        }
    }
}
