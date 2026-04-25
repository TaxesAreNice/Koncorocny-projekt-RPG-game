using Koncoročný_projekt__RPG_game.UI_Generations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Koncoročný_projekt__RPG_game
{
    internal class Fighting
    {
        public List<string> currentEnemies = new List<string>();

        Player player = new Player();
        Monster monster = new Monster();
        Specials specials = new Specials();

        public enum TurnState
        {
            PlayerTurn,
            EnemyTurn,
            WonFight,
            Win,
            Lose
        }

        public TurnState State { get; private set; }

        public List<(int hp, int attack, int defense, string name)> enemies = new List<(int hp, int attack, int defense, string name)>()   
        {
            (150, 25, 20, "Trader"),
            (320, 35, 15, "Possessed King"),
            (200, 30, 30, "Cursed Trader"),
            (35, 4, 0, "Bat"),
            (65, 10, 0, "Vampire"),
            (80, 30, 20, "Jester"),
            (250, 10, 0, "Shield Spirit"),
            (180, 25, 10, "Knight"),
            (150, 20, 0, "Mage"),
            (220, 30, 10, "Phoenix"),
            (300, 40, 20, "Dragon"),
            (100, 1, 50, "Mythical Pig")
        };
        private void EnemyTurn()
        {
            if (State != TurnState.EnemyTurn)
                return;
            if (monster.MonsterName == "Jester")
            {
                specials.JesterSpecial();
            }
            else if (monster.MonsterName == "Mage")
            {
                specials.MageSpecials();
            }
            else
            {
                monster.CalculateDamage(player);
                monster.DoDamage(player);
            }

            monster.CalculateDamage(player);
            monster.DoDamage(player);
        }

        private void PlayerTurn()
        {
            if (State != TurnState.PlayerTurn)
                return;

            player.CalculateDamage(monster);
            player.DoDamage(monster);
        }

        public bool enemyDead()
        {
            if (monster.MonsterHP <= 0)
            {
                State = TurnState.WonFight;
                return true;
            }
            return false;
        }
    }
}