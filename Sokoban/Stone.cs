using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban
{
    public class Stone
    {
        public int x;
        public int y;
        public Image StoneImg;
        public int i;
        public int j;
        public int size = 50;

        public Stone(int i, int j)
        {
            x = 100 + j * 50;
            y = 100 + i * 50;
            this.i = i;
            this.j = j;
            StoneImg = new Bitmap("resources1\\wall.jpg");
        }

        


    }
}
