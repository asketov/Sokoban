using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban
{
    public class Player
    {
        public int x;
        public int y;
        public int i;
        public int j;
        public Image PlayerImg;
        public int size = 50;
        
        public Player(int i, int j)
        {
            x = 100 + j * 50;
            y = 100 + i * 50;
            this.i = i;
            this.j = j;
            PlayerImg = new Bitmap("resources1\\chel.png");
        }
    }
}
