using System.Numerics;

namespace Koncoročný_projekt__RPG_game
{
    internal class Player
    {
        Monster monster = new Monster();
        public int PlayerHP = 100;
        public int PlayerDamage = 0;
        public int PlayerAttack = 15;
        public int PlayerDefense = 0;
        public bool IsDead => PlayerHP <= 0;
        public bool DoDamage()
        {
            monster.MonsterHP -= PlayerDamage;
            if (monster.MonsterHP <= 0)
            {
                return true; // monster dead
            }

            return false;
        }
        public void TakeDamage()
        {
            PlayerHP -= monster.MonsterDamage;
        }

        public int CalculateDamage()
        {
            PlayerDamage = PlayerAttack - monster.MonsterDefense;

            if (PlayerDamage < 0)
                PlayerDamage = 0;

            return PlayerDamage;
        }
    }
}