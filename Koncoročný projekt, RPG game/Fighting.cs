using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    internal class Fighting
    {
        Player Player = new Player();
        Monster monster = new Monster();
        public enum TurnState
        {
            PlayerTurn,
            EnemyTurn,
            Win,
            Lose
        }
        public TurnState State { get; private set; }

        private void EnemyTurn()
        {
            if (State != TurnState.EnemyTurn)
                return;
            monster.DoDamage();
            Player.TakeDamage();
        }
        private void PlayerTurn()
        {
            if (State != TurnState.PlayerTurn)
                return;
            Player.DoDamage();
            monster.TakeDamage();
            if (Player.IsDead)
            {
                MessageBox.Show("You lost!");
            }
        }
        private void FunkcieInFight()
        {

        }

        private List<(int hp, int damage, string name)> enemies = new List<(int hp, int damage, string name)>()
        {
            (100, 10, "Trader"), //can sell you items, but you can try to kill him for chance to get potions, scrolls, etc
            (320, 35, "Possessed King "), // will drop Bloodthorn sword
            (35, 4, "Bat"),     //uselless
            (65, 10, "Vampire"), // can drop Blood pack ring
            (80, 12, "Jester"),  // can do special attacks....
            (250, 10, "Shield Spirit"), // everz 2nd attack will be blocked
            (180, 25, "Knight"),       // can drop knight armor, sword
            (150, 20, "Mage"),    // can drop scrolls..., ranged attacks + spells
            (220, 30, "Phoenix"), // drop phoenix feather
            (300, 40, "Dragon"),   // can drop egg, scale or heart
            (100, 1, "Mythical Pig") // can drop mythical meal
        };
    }
}
