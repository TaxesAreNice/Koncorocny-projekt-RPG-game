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
        Player player = new Player();
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
            player.TakeDamage();
        }
        private void PlayerTurn()
        {
            if (State != TurnState.PlayerTurn)
                return;
            player.DoDamage();
            monster.TakeDamage();
        }

        private List<(int hp, int damage, int defense, string name)> enemies = new List<(int hp, int damage, int defense, string name)>()
        {
            (100, 10, 5, "Trader"), //can sell you items, but you can try to kill him for chance to get potions, scrolls, etc
            (320, 35, 15, "Possessed King "), // will drop Bloodthorn sword
            (35, 4, 0, "Bat"),     //uselless
            (65, 10, 0, "Vampire"), // can drop Blood pack ring
            (80, 12, 20, "Jester"),  // can do special attacks....
            (250, 10, 0, "Shield Spirit"), // everz 2nd attack will be blocked
            (180, 25, 10, "Knight"),       // can drop knight armor, sword
            (150, 20, 0, "Mage"),    // can drop scrolls..., ranged attacks + spells
            (220, 30, 10, "Phoenix"), // drop phoenix feather
            (300, 40, 20, "Dragon"),   // can drop egg, scale or heart
            (100, 1, 0, "Mythical Pig") // can drop mythical meal
        };
        private List<string> items = new List<string>()
        {
        "Phoenix Feather",
        "BloodThorn Sword",
        "Breaker Ring",
        "Knight Helmet",
        "Knight Chestplate",
        "Knight Leggins",
        "Knight Boots",
        "Knight Sword",
        "Mythical Meal",
        "Health Potion",
        "Big Health Potion",
        "2x Damage Potion",
        //scrolls
        };
    }
}