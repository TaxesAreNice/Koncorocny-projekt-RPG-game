using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class Items
    {
        Player player = new Player();
        private List<string> items = new List<string>()
{
    "Phoenix Feather",
    "BloodThorn Sword",
    "Blood Pack Ring",
    "Knight Armor",
    "Knight Sword",
    "Mythical Meal",
    "Health Potion",
    "Big Health Potion",
    "2x Damage Potion",
    //scrolls
};
        private void UseItem(string item)
        {
            switch (item)
            {
                case "Phoenix Feather":
                    if (player.IsDead)
                    {
                        //dorobit este ze to napise ze ta to revivlo
                        player.PlayerHP = 100;
                    }
                    break;
        /*case "BloodThorn Sword":
                    player.PlayerDamage += 20;
                    break;
                case "Blood Pack Ring":
                    player.PlayerHP += 20;
                    player.PlayerDamage += 10;
                    break;
                case "Knight Armor":
                    player.PlayerHP += 30;
                    break;
                case "Knight Sword":
                    player.PlayerDamage += 15;
                    break;
                case "Mythical Meal":
                    player.PlayerHP += 100;
                    player.PlayerDamage += 50;
                    break;
                case "Health Potion":
                    player.PlayerHP += 20;
                    break;
                case "Big Health Potion":
                    player.PlayerHP += 50;
                    break;
                case "2x Damage Potion":
                    player.PlayerDamage = 2;
                    break; */
            }
        }



    }
}
