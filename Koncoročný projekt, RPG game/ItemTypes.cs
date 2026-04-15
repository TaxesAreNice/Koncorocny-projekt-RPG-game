using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class ItemTypes
    {
        public List<(string Hemlet, string Chestplate, string Leggins, string Boots, string Sword, string Ring, string SecondHand, string Accessory)> items = new List<(string Hemlet, string Chestplate, string Leggins, string Boots, string Sword, string Ring, string SecondHand, string Accessory)>()
        {
         (
            "Phoenix Feather",
            "Knight Armor",
            "Knight Leggins",
            "Knight Boots",
            "Knight Sword",
            "Blood Pack Ring",
            "faf", // tester
            ""
          )
        };
        
        
          
        
    }
    //"Hemlet:", "Chestplate:", "Leggins:", "Boots:", "Sword:", "Ring:", "2nd hand:", "Accesory:"
}

/*
 "Phoenix Feather",
"BloodThorn Sword",
"Blood Pack Ring",
"Knight Armor",
"Knight Sword",
"Mythical Meal",
"Health Potion",
"Big Health Potion",
"2x Damage Potion",
 * */