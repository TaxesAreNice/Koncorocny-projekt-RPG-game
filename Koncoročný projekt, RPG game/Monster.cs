namespace Koncoročný_projekt__RPG_game
{
    internal class Monster
    {
        public string MonsterName = "";
        public int MonsterHP { get; set; }
        public int MonsterDamage { get; set; }
        public int MonsterAttack { get; set; }
        public int MonsterDefense { get; set; }

        public void DoDamage(Player player)
        {
            player.PlayerHP -= MonsterDamage;
        }

        public void TakeDamage(int damage)
        {
            MonsterHP -= damage;
        }

        public int CalculateDamage(Player player)
        {
            MonsterDamage = MonsterAttack - player.PlayerDefense;

            if (MonsterDamage < 0)
                MonsterDamage = 0;

            return MonsterDamage;
        }
    }
}