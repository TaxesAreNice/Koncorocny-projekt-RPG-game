namespace Koncoročný_projekt__RPG_game
{
    internal class Player
    {
        Monster monster = new Monster();
        public int PlayerHP { get; set; }
        public int PlayerDamage { get; set; }
        public bool IsDead => PlayerHP <= 0;
        private void DoDamage()
        {
            monster.MonsterHP -= PlayerDamage;
        }
        private void TakeDamage()
        {
            PlayerHP -= monster.MonsterDamage;
        }
    }
}