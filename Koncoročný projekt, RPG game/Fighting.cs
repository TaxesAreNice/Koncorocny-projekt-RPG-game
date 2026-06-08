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
    public class Fighting
    {
        public List<Enemy> currentEnemies = new List<Enemy>();

        public int selectedEnemyID = -1;
        public bool enemySelected = false;

        Player player = new Player();
        Monster monster = new Monster();
        Specials specials = new Specials();

        public enum TurnState
        {
            PlayerTurn,
            EnemyTurn,
            WonFight,
            StartFight,
        }

        public TurnState State { get; private set; }

        public List<(int hp, int attack, int defense, string name)> enemies = new List<(int hp, int attack, int defense, string name)>()
        {
            (200, 30, 30, "Cursed Trader"),
            (35, 7, 0, "Bat"),
            (65, 10, 0, "Vampire"),
            (80, 30, 20, "Jester"),
            (250, 10, 50, "Shield Spirit"),
            (180, 25, 10, "Knight"),
            (150, 20, 0, "Mage"),
            (220, 70, 10, "Phoenix"),
            (300, 80, 20, "Dragon"),
            (100, 1, 50, "Mythical Pig")
        };

        public void EnemyAttacks()
        {
            State = TurnState.EnemyTurn;

            foreach (var enemy in currentEnemies)
            {
                if (enemy.EnemyName != "Bat")

                monster.MonsterHP = enemy.EnemyHP;
                monster.MonsterAttack = enemy.EnemyAttack;
                monster.MonsterDefense = enemy.EnemyDefense;
                monster.MonsterDamage = 0;

                
                EnemyTurn();
            }
        }

        public void EnemyTurn()
        {
            if (State != TurnState.EnemyTurn)
                return;
            /*if (monster.MonsterName == "Jester")
            {
                specials.JesterSpecial();
            }
            else if (monster.MonsterName == "Mage")
            {
                specials.MageSpecials();
            }
            else
            {*/
                monster.CalculateDamage(player);
                monster.DoDamage(player);
            //}
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

        public bool playerDead()
        {
            if (player.PlayerHP <= 0)
            {
                State = TurnState.EnemyTurn;
                return true;
            }
            return false;
        }

        public List<int> GetPlayerStats()
        {
            return new List<int> { player.PlayerHP, player.PlayerAttack, player.PlayerDefense };
        }

        public string EnemySelected(int id)
        {
            if (!enemySelected)
            {
                selectedEnemyID = id;
                enemySelected = true;
                return "true";
            }
            else if (id == selectedEnemyID)
            {
                selectedEnemyID = -1;
                enemySelected = false;
                return "unselect";
            }
            return "false";
        }

        public string PlayerAttack()
        {
            if (!enemySelected) { return "nothingSelected"; }
            enemySelected = false;

            State = TurnState.PlayerTurn;

            Enemy? target = currentEnemies.FirstOrDefault(e => e.EnemyID == selectedEnemyID);
            if (target == null) { selectedEnemyID = -1; return "nothingSelected"; }

            monster.MonsterName = target.EnemyName;
            monster.MonsterHP = target.EnemyHP;
            monster.MonsterAttack = target.EnemyAttack;
            monster.MonsterDefense = target.EnemyDefense;
            monster.MonsterDamage = 0;

            PlayerTurn();

            bool isEnemyDead = enemyDead();
            if (isEnemyDead)
            {
                for (int i = currentEnemies.Count - 1; i >= 0; i--)
                {
                    if (currentEnemies[i].EnemyID == target.EnemyID)
                    {
                        selectedEnemyID = -1;
                        return "enemyDead_" + i;
                    }
                }
            }
            else
            {
                target.EnemyHP = monster.MonsterHP;
            }

            selectedEnemyID = -1;
            State = TurnState.EnemyTurn;
            return "ContinueFight";
        }

        internal Player RequestPlayer()
        {
            return player;
        }

        internal Monster RequestMonster()
        {
            return monster;
        }

        public void KillEnemy(int num)
        {
            currentEnemies.RemoveAt(num);
        }

        public void RevivePlayer()
        {
            player.PlayerHP = 100;
        }
    }
}
