namespace Koncoročný_projekt__RPG_game
{
    public class Monster
    {
        public string MonsterName = "";
        public int MonsterHP { get; set; }
        public int MonsterDamage { get; set; }
        public int MonsterAttack { get; set; }
        public int MonsterDefense { get; set; }

        public int MonsterDefenceStatus { get; set; }
        public int MonsterAttackStatus { get; set; }
        public int MonsterHPStatus { get; set; }

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
            MonsterDamage = MonsterAttack - (player.PlayerDefense - MonsterAttackStatus);

            if (MonsterDamage < 0)
                MonsterDamage = 0;

            return MonsterDamage;
        }
    }
}