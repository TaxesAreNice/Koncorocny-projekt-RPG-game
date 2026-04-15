namespace Koncoročný_projekt__RPG_game
{
    internal class Monster
    {
        Player player = new Player();
        public string Name { get; set; }
        public int MonsterHP { get; set; }
        public int MonsterDamage = 0;

        private void DoDamage()
        {
            player.PlayerHP -= MonsterDamage;
        }
        private void TakeDamage()
        {
            MonsterHP -= player.PlayerDamage;
        }
    }
}