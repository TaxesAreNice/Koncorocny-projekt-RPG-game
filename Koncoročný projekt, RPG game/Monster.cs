namespace Koncoročný_projekt__RPG_game
{
    internal class Monster
    {
        Player player = new Player();
        public string Name = "";
        public int MonsterHP { get; set; }
        public int MonsterDamage { get; set; }
        public int MonsterAttack { get; set; }
        public int MonsterDefense { get; set; }

        public void DoDamage()
        {
            player.PlayerHP -= MonsterDamage;
        }
        public void TakeDamage()
        {
            MonsterHP -= player.PlayerDamage;
        }

        public int CalculateDamage()
        {
            MonsterDamage = MonsterAttack - player.PlayerDefense;

            if (MonsterDamage < 0)
                MonsterDamage = 0;

            return MonsterDamage;
        }
    }
}