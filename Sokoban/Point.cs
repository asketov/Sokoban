using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban
{
    public class Point
    {
        public int x;
        public int y;
        public int i;
        public int j;
        public Image PointImg;
        public int size = 50;
        public Point(int i, int j)
        {
            x = 100 + j * 50;
            y = 100 + i * 50;
            this.i = i;
            this.j = j;
            PointImg = new Bitmap("resources1\\point.png");
        }
    }
}
