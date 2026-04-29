using System.Numerics;

namespace Koncoročný_projekt__RPG_game
{
    internal class Player
    {
        public int PlayerHP = 100;
        public int PlayerDamage = 0;
        public int PlayerAttack = 15;
        public int PlayerDefense = 0;
        public int PlayerMana = 50;

        public bool IsDead => PlayerHP <= 0;

        public void DoDamage(Monster monster)
        {
            monster.MonsterHP -= PlayerDamage;
        }

        public void TakeDamage(int damage)
        {
            PlayerHP -= damage;
        }

        public int CalculateDamage(Monster monster)
        {
            PlayerDamage = PlayerAttack - monster.MonsterDefense;

            if (PlayerDamage < 0)
                PlayerDamage = 0;

            return PlayerDamage;
        }

        public void ScytheDamage(Monster monster)
        {
            monster.MonsterHP -= PlayerAttack;
        }
    }
}