using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Koncoročný_projekt__RPG_game
{
    internal class ChestMovementClass
    {
        public int ChosenX = 0;
        public int ChosenY = 0;

        public int BackupX = 0;
        public int BackupY = 0;

        public bool isPressed = false;

        public void Pressed(int x, int y )
        {
            ChosenX = x;
            ChosenY = y;
            isPressed = true;
        }

     

        
    }
}
