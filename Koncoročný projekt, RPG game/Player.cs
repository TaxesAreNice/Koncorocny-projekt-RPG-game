namespace Koncoročný_projekt__RPG_game
{
    internal class Player
    {
        Monster monster = new Monster();
        public int PlayerHP = 100;
        public int PlayerDamage = 15;
        public bool IsDead => PlayerHP <= 0;
        public void DoDamage()
        {
            monster.MonsterHP -= PlayerDamage;
        }
        public void TakeDamage()
        {
            PlayerHP -= monster.MonsterDamage;
        }
    }
}