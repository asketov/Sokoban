using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

/*Игра «Сокобан».
Должна быть реализована игра “Сокобан”. Должны быть добавлены не
менее пяти уровней и таблица рекордов. Уровни должны загружаться из
файлов. Должен быть добавлен редактор уровней.*/
namespace Sokoban
{
    public partial class Form1 : Form
    {
        Stopwatch stopwatch;
        Player player1;
        List<Stone> Stones = new List<Stone>();
        List<Point> Points = new List<Point>();
        Font font = new Font("Candara", 16);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        List<Box> Boxes = new List<Box>();
        Box box,box1;
        Image BigWall = new Bitmap("resources1\\BigWall.png");
        Image Fon = new Bitmap("resources1\\fon.png");
        Image Menu = new Bitmap("resources1\\menu.png");
        Image Levels = new Bitmap("resources1\\levels.png");
        Image InLevel = new Bitmap("resources1\\inlevel.png");
        Image Table = new Bitmap("resources1\\table.png");
        Image InChangeLevel = new Bitmap("resources1\\inchangelevel.png");
        bool levelsSelectedInMenu = false,levelSelectedInLevels = false, menu = true, table = false, changeLevel = false,
           selectedBlock = false;
        int level,block;
        int[,] matrix = new int[8, 8];



        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
            stopwatch = new Stopwatch();
        }

        private void update(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (levelSelectedInLevels && !changeLevel)
            {
                if (e.KeyCode == Keys.Up && player1.y != 100 && !CollideWithStone(1, player1.i, player1.j))
                {
                    box = CollideWithBox(1, player1.i, player1.j);
                    if (box == null)
                    {
                        player1.y -= 50;
                        player1.i -= 1;
                    }
                    else if (!CollideWithStone(1, box.i, box.j) && box.y != 100)
                    {
                        box1 = CollideWithBox(1, box.i, box.j);
                        if (box1 == null)
                        {
                            player1.y -= 50;
                            player1.i -= 1;
                            box.y -= 50;
                            box.i -= 1;
                        }
                    }

                }
                if (e.KeyCode == Keys.Down && player1.y != 450 && !CollideWithStone(2, player1.i, player1.j))
                {
                    box = CollideWithBox(2, player1.i, player1.j);
                    if (box == null)
                    {
                        player1.y += 50;
                        player1.i += 1;
                    }
                    else if (!CollideWithStone(2, box.i, box.j) && box.y != 450)
                    {
                        box1 = CollideWithBox(2, box.i, box.j);
                        if (box1 == null)
                        {
                            player1.y += 50;
                            player1.i += 1;
                            box.y += 50;
                            box.i += 1;
                        }
                    }

                }
                if (e.KeyCode == Keys.Left && player1.x != 100 && !CollideWithStone(3, player1.i, player1.j))
                {
                    box = CollideWithBox(3, player1.i, player1.j);
                    if (box == null)
                    {
                        player1.x -= 50;
                        player1.j -= 1;
                    }
                    else if (!CollideWithStone(3, box.i, box.j) && box.x != 100)
                    {
                        box1 = CollideWithBox(3, box.i, box.j);
                        if (box1 == null)
                        {
                            player1.x -= 50;
                            player1.j -= 1;
                            box.x -= 50;
                            box.j -= 1;
                        }
                    }

                }
                if (e.KeyCode == Keys.Right && player1.x != 450 && !CollideWithStone(4, player1.i, player1.j))
                {
                    box = CollideWithBox(4, player1.i, player1.j);
                    if (box == null)
                    {
                        player1.x += 50;
                        player1.j += 1;
                    }
                    else if (!CollideWithStone(4, box.i, box.j) && box.x != 450)
                    {
                        box1 = CollideWithBox(4, box.i, box.j);
                        if (box1 == null)
                        {
                            box.x += 50;
                            box.j += 1;
                            player1.x += 50;
                            player1.j += 1;
                        }
                    }
                }
                Invalidate();
                if (CheckWinGame())
                {
                    levelSelectedInLevels = false;
                    menu = true;
                }
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if (menu) graphics.DrawImage(Menu, 0, 0, 600, 600); 
            else if (levelsSelectedInMenu) graphics.DrawImage(Levels, 0, 0, 600, 600);
            else if (table)
            {
                graphics.DrawImage(Table, 0, 0, 600, 600);
                using (TextReader reader = File.OpenText("tableRecords.txt"))
                {
                    double x;
                    for (int i = 0; i < 5; i++)
                    {
                        string text = reader.ReadLine();
                        string[] bits = text.Split(' ');
                        x = double.Parse(bits[1]);
                        graphics.DrawString(x.ToString(), font, drawBrush, 276, 120+i*26);
                    }
                }
            }
            else if (levelSelectedInLevels)
            {
                if(!changeLevel) graphics.DrawImage(InLevel, 0, 0, 600, 600);
                else graphics.DrawImage(InChangeLevel, 0, 0, 600, 600);
                graphics.DrawImage(Fon, 100, 100, 400, 400);
                for (int i = 0; i < Stones.Count; i++)
                {
                    Stone stone = Stones.ElementAt(i);
                    graphics.DrawImage(stone.StoneImg, stone.x, stone.y, stone.size, stone.size);
                }
                for (int i = 0; i < Points.Count; i++)
                {
                    Point point = Points.ElementAt(i);
                    graphics.DrawImage(point.PointImg, point.x, point.y, point.size, point.size);
                }
                for (int i = 0; i < Boxes.Count; i++)
                {
                    Box box = Boxes.ElementAt(i);
                    graphics.DrawImage(box.BoxImg, box.x, box.y, box.size, box.size);
                }
                graphics.DrawImage(player1.PlayerImg, player1.x, player1.y, player1.size, player1.size);
            }
        }

        public void ReadFile(string file)
        {
            int i, x, j;
            using (TextReader reader = File.OpenText(file))
            {
                if (reader != null)
                {
                    for (i = 0; i < 8; i++)
                    {
                        string text = reader.ReadLine();
                        string[] bits = text.Split(' ');
                        for (j = 0; j < 8; j++)
                        {
                            x = int.Parse(bits[j]);
                            matrix[i,j] = x;
                            if(x==31)
                            {
                                Box box = new Box(i, j);
                                Boxes.Add(box);
                                Point point = new Point(i, j);
                                Points.Add(point);
                            }
                            if(x == 0)
                            {
                                player1 = new Player(i, j);
                            }
                            else if(x == 1)
                            {
                                Box box = new Box(i, j);
                                Boxes.Add(box);
                            }
                            else if(x == 2)
                            {
                                Stone stone = new Stone(i, j);
                                Stones.Add(stone);
                            }
                            else if(x == 3)
                            {
                                Point point = new Point(i, j);
                                Points.Add(point);
                            }
                        }
                    }
                }
            }
        }
        
        public bool CheckWinGame()
        {
            int col=0;
            foreach (Point point in Points)
                    foreach (Box box in Boxes)
                        if (box.i == point.i && box.j == point.j) col++;
            if (col == Points.Count)
            {
                stopwatch.Stop();
                double time = stopwatch.ElapsedMilliseconds / 1000.0;
                stopwatch.Reset();
                string[] str = File.ReadAllLines("tableRecords.txt");
                using (TextReader reader = File.OpenText("tableRecords.txt"))
                {
                    double x;
                    string text;
                    for (int i = 1; i < level; i++)
                    {
                        text = reader.ReadLine();
                    }
                    text = reader.ReadLine();
                    string[] bits = text.Split(' ');
                    x = double.Parse(bits[1]);
                    if (x < time) return true;
                }
                using (StreamWriter sw = new StreamWriter("tableRecords.txt"))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if ((i + 1) != level) sw.WriteLine(str[i]);
                        else sw.WriteLine(level + " " + time);
                    }
                }
                return true;
            }
            else return false;
        }
        public bool CollideWithStone(int side,int i, int j)
        {
            switch (side)
            {
                case 1:
                    foreach (Stone stone in Stones)
                    {
                        if (stone.i == (i - 1) && stone.j == j) return true;
                    }
                    return false;
                case 2:
                    foreach (Stone stone in Stones)
                    {
                        if (stone.i == (i + 1) && stone.j == j) return true;
                    }
                    return false;
                case 3:
                    foreach (Stone stone in Stones)
                    {
                        if (stone.j == (j - 1) && stone.i == i) return true;
                    }
                    return false;
                case 4:
                    foreach (Stone stone in Stones)
                    {
                        if (stone.j == (j + 1) && stone.i == i) return true;
                    }
                    return false;
                default:
                    return false;
            }
            
        }
        
        public void ClearLists()
        {
            Boxes.Clear();
            Points.Clear();
            Stones.Clear();
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if(menu) //меню
            {
                if(e.Location.X>197&&e.Location.X<401&&e.Location.Y>148 && e.Location.Y<200) //выбрал уровни
                {
                    levelsSelectedInMenu = true;
                    menu = false;
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 236 && e.Location.Y < 287) //таблица
                {
                    menu = false;
                    table = true;
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 320 && e.Location.Y < 371) //редактор уровней
                {
                    levelsSelectedInMenu = true;
                    menu = false;
                    changeLevel = true;
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 406 && e.Location.Y < 458) // Выход из формы
                {
                    Close();
                }
            }
            else if (levelsSelectedInMenu) //выбраны уровни
            {
                if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 66 && e.Location.Y < 118) // 1 level
                {
                    ReadLevel(1);
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 149 && e.Location.Y < 200)// 2 level
                {
                    ReadLevel(2);
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 236 && e.Location.Y < 287) // 3 level
                {
                    ReadLevel(3);
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 320 && e.Location.Y < 371) // 4 level 
                {
                    ReadLevel(4);
                }
                else if (e.Location.X > 197 && e.Location.X < 401 && e.Location.Y > 406 && e.Location.Y < 458) // 5 level
                {
                    ReadLevel(5);
                }
            }
            else if (levelSelectedInLevels && !selectedBlock)
            {
                if (e.Location.X > 117 && e.Location.X < 287 && e.Location.Y > 506 && e.Location.Y < 554) // начинаем вновь уровень
                {
                    if(!changeLevel) stopwatch.Reset();
                    ReadLevel(level);
                }
                else if (e.Location.X > 299 && e.Location.X < 420 && e.Location.Y > 506 && e.Location.Y < 554) // возврат в меню
                {
                    menu = true;
                    if(!changeLevel) stopwatch.Reset();
                    levelSelectedInLevels = false;
                    changeLevel = false;
                    
                }
                else if (e.Location.X > 4 && e.Location.X < 108 && e.Location.Y > 506 && e.Location.Y < 554) // возврат в уровни
                {
                    if(!changeLevel) stopwatch.Reset();
                    levelsSelectedInMenu = true;
                    levelSelectedInLevels = false;
                }
                else if (e.Location.X > 64 && e.Location.X < 555 && e.Location.Y > 37 && e.Location.Y < 97 && changeLevel)
                {
                    if (e.Location.X > 171 && e.Location.X < 232) { this.Cursor = Cursors.Cross; block = 0; selectedBlock = true; }
                    else if (e.Location.X > 279 && e.Location.X < 339) { this.Cursor = Cursors.Cross; block = 1; selectedBlock = true; } 
                    else if (e.Location.X > 388 && e.Location.X < 449) { this.Cursor = Cursors.Cross; block = 2; selectedBlock = true; }
                    else if (e.Location.X > 495 && e.Location.X < 555) { this.Cursor = Cursors.Cross; block = 3; selectedBlock = true; }
                    else if (e.Location.X < 125) { this.Cursor = Cursors.Cross; block = 4; selectedBlock = true; }
                }
                else if (e.Location.X > 432 && e.Location.X < 552 && e.Location.Y > 506 && e.Location.Y < 553 && changeLevel)
                {
                    using (StreamWriter sw = new StreamWriter("levels\\level" + level + ".txt"))
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            sw.WriteLine(matrix[i,0] + " " + matrix[i,1] + " " + matrix[i,2] + " " + matrix[i,3] + " "
                                + matrix[i,4] + " " + matrix[i,5] + " " + matrix[i,6] + " " + matrix[i,7]);
                        }
                    }
                }
            }
            else if(selectedBlock)
            {
                int k = (e.Location.X - 100) / 50, f = (e.Location.Y - 100) / 50;
                if (e.Location.X > 100 && e.Location.X < 500 && e.Location.Y > 100 && e.Location.Y < 500)
                {
                    if (block == 0)
                    {
                        for (int i = 0; i < 8; i++)
                            for (int j = 0; j < 8; j++)
                                if (matrix[i, j] == 0) matrix[i, j] = 4;
                    }
                    matrix[f, k] = block;
                    selectedBlock = false;
                    this.Cursor = Cursors.Arrow;
                    ReadMatrix();
                }
            }
            else if(table) //таблица рекордов
            {
                if (e.Location.X > 299 && e.Location.X < 420 && e.Location.Y > 506 && e.Location.Y < 554)
                {
                    menu = true; table = false;
                }
            }
        }

        public void ReadLevel(int level)
        {
            levelSelectedInLevels = true; this.level = level;
            levelsSelectedInMenu = false;
            ClearLists();
            ReadFile("levels\\level"+level+".txt");
            if(!changeLevel) stopwatch.Start();
        }

        public void ReadMatrix()
        {
            ClearLists();
            int i, j;
                    for (i = 0; i < 8; i++)
                    {
                        for (j = 0; j < 8; j++)
                        {
                            if (matrix[i,j] == 31)
                            {
                                Box box = new Box(i, j);
                                Boxes.Add(box);
                                Point point = new Point(i, j);
                                Points.Add(point);
                            }
                            if (matrix[i, j] == 0)
                            {
                                player1 = new Player(i, j);
                            }
                            else if (matrix[i, j] == 1)
                            {
                                Box box = new Box(i, j);
                                Boxes.Add(box);
                            }
                            else if (matrix[i, j] == 2)
                            {
                                Stone stone = new Stone(i, j);
                                Stones.Add(stone);
                            }
                            else if (matrix[i, j] == 3)
                            {
                                Point point = new Point(i, j);
                                Points.Add(point);
                            }
                        }
                    }
                
            
        }

        public Box CollideWithBox(int side, int i, int j)
        {
            switch (side)
            {
                case 1:
                    foreach (Box box in Boxes)
                    {
                        if (box.i == (i - 1) && box.j == j) return box;
                    }
                    return null;
                case 2:
                    foreach (Box box in Boxes)
                    {
                        if (box.i == (i + 1) && box.j == j) return box;
                    }
                    return null;
                case 3:
                    foreach (Box box in Boxes)
                    {
                        if (box.j == (j - 1) && box.i == i) return box;
                    }
                    return null;
                case 4:
                    foreach (Box box in Boxes)
                    {
                        if (box.j == (j + 1) && box.i == i) return box;
                    }
                    return null;
                default:
                    return null;
            }

        }

        
    }
}
