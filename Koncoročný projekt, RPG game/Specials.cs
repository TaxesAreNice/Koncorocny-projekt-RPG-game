using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class Specials
    {
        Monster monster = new Monster();
        Player player = new Player();

        public void JesterSpecial()
        {
            Random random = new Random();
            int roll =random.Next(0, 5);

            if (roll == 0)
            {
                monster.MonsterHP -= 30;
            }
            else if (roll == 1)
            {
                monster.MonsterDefense += 30;
            }
            else if (roll == 2)
            {
                monster.MonsterHP += 30;
            }
            else if (roll == 3)
            {
                player.PlayerHP += 30;
            }
            else if (roll == 4)
            {
                monster.MonsterAttack += 15;
            }
            monster.CalculateDamage(player);
            monster.DoDamage(player);
        }
        public void MageSpecials()
        {
            Random random = new Random();
            int roll = random.Next(0, 4);
            if (roll == 0)
            {
                monster.MonsterDamage += 10; //fire scroll
            }
            if (roll == 1)
            {
                monster.MonsterDamage += 20; //lightning scroll
            }
            if (roll == 2)
            {
                monster.MonsterHP += 25; //healing scroll
            }
            if (roll == 3)
            {
                monster.MonsterDefense += 15; //shield scroll
            }
        }
        public void PlayerSpecials()
        {
            //ak ma hrac eqquiped scythe,tak urobi scytheDamage metodu
        }
    }
}
