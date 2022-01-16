using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban
{
    public class Box
    {
        public int x;
        public int y;
        public Image BoxImg;
        public int i;
        public int j;
        public int size = 50;
        public Box(int i, int j)
        {
            x = 100 + j * 50;
            y = 100 + i * 50;
            this.i = i;
            this.j = j;
            BoxImg = new Bitmap("resources1\\box.png");
        }
    }
}
