using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

/*
 *  Stuff to do:
 *  
 *  Random movements for Pacman
 *  Capture movements and grid for Ghost and Pacman
 *  Make Training data
 *  Install Training method in Form
 *  
 */

namespace PacMan
{
    public partial class Form1 : Form
    {
        private int[,] grid;
        private PictureBox[,] picArr;
        private NN_Ghost[] ghostArray;
        private NN_Pacman[] pacArray;
        private int currGhost = 0;
        private int currPac = 0;
        private Boolean ghostFood = true;

        private int GhostX;
        private int GhostY;
        private int PacX;
        private int PacY;
        private int width = 10;
        private int height = 10;

        private int time = 0;
        private int cycles = 0;
        private int gen = 1;

        private int steps = 0;
        private int score = 0;
        private Timer timer;
        private Random ran;

        private int point_invalid = -10;
        private int point_food = 10;
        private int point_step = -1;
        private int point_ghost = -10;
        private int point_kill = -1000000;

        private int value_wall = -10;
        private int value_food = 10;
        private int value_pacman = 5;
        private int value_ghost = -5;
        private int value_empty = 0;

        private int turn_length = 50;
        private int time_interval = 10; // pac speed
        private Boolean render = true;

        private int[,] picture;
        private int[] move;
        private int[] GhostGrid;
        private string BatchLoc = "@H:/lLegends";  


        public Form1()
        {
            GhostGrid = new int[8];
            GhostX = width - 1;
            GhostY = height - 1;
            PacX = 0;
            PacY = 0;

            grid = new int[width, height];
            picArr = new PictureBox[width, height];
            ran = new Random();

            move = new int[200];
            picture = new int[move.Length, 8];

            timer = new Timer
            {
                Interval = time_interval
            };
            timer.Tick += new EventHandler(TimerTick);
            timer.Enabled = false;

            ghostArray = new NN_Ghost[2];
            pacArray = new NN_Pacman[2];
            for (int x = 0; x < ghostArray.Length; x++)
            {
                ghostArray[x] = new NN_Ghost();
            }
            for (int x = 0; x < pacArray.Length; x++)
            {
                pacArray[x] = new NN_Pacman(width, height);
            }

            for (int x = 0; x < width; x++)
            {

                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = value_food;
                }
            }

            GenWall();
            grid[GhostX, GhostY] = value_ghost;
            grid[0, 0] = value_pacman;
            if (render)
            {
                PaintBoard();
            }
            InitializeComponent();

            PacX = 0;
            PacY = 0;
        }

        private void PaintBoard()
        {
            PacX = 10;
            PacY = 150;

            int x = 0;
            int y = 0;

            do
            {
                do
                {
                    picArr[x, y] = new PictureBox();
                    if (grid[x, y] == value_wall)
                    {
                        picArr[x, y].BackColor = Color.Black;
                    }
                    else if (grid[x, y] == value_food)
                    {
                        picArr[x, y].BackColor = Color.Blue;
                    }
                    else if (grid[x, y] == value_pacman)
                    {
                        picArr[x, y].BackColor = Color.Yellow;
                    }
                    else if (grid[x, y] == value_pacman)
                    {
                        picArr[x, y].BackColor = Color.Red;
                    }
                    else
                    {
                        picArr[x, y].BackColor = Color.LightGray;
                    }
                    picArr[x, y].Location = new Point(PacX, PacY);
                    picArr[x, y].Size = new Size(25, 25);
                    this.Controls.Add(picArr[x, y]);
                    PacX += 27;
                    y++;
                } while (y < height);
                x++;
                PacY += 27;
                PacX = 10;
                y = 0;
            } while (x < width);

            PacX = 0;
            PacY = 0;
        }

        private void GenWall()
        {
            grid[3, 3] = value_wall;
            grid[4, 3] = value_wall;
            grid[5, 3] = value_wall;
            grid[6, 3] = value_wall;

            grid[3, 6] = value_wall;
            grid[4, 6] = value_wall;
            grid[5, 6] = value_wall;
            grid[6, 6] = value_wall;

            grid[1, 1] = value_wall;
            grid[1, 2] = value_wall;
            grid[1, 3] = value_wall;

            grid[1, 6] = value_wall;
            grid[1, 7] = value_wall;
            grid[1, 8] = value_wall;

            grid[8, 1] = value_wall;
            grid[8, 2] = value_wall;
            grid[8, 3] = value_wall;

            grid[8, 6] = value_wall;
            grid[8, 7] = value_wall;
            grid[8, 8] = value_wall;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Ghost_Grid();
            if (steps < move.Length)
            {
                picture[steps, 0] = GhostGrid[0];
                picture[steps, 1] = GhostGrid[1];
                picture[steps, 2] = GhostGrid[2];
                picture[steps, 3] = GhostGrid[3];
                picture[steps, 4] = GhostGrid[4];
                picture[steps, 5] = GhostGrid[5];
                picture[steps, 6] = GhostGrid[6];
                picture[steps, 7] = GhostGrid[7];
            }

            steps++;
            if (keyData == Keys.Left)
            {
                GhostMove(2);
            }
            if (keyData == Keys.Right)
            {
                GhostMove(3);
            }
            if (keyData == Keys.Up)
            {
                GhostMove(0);
            }
            if (keyData == Keys.Down)
            {
                GhostMove(1);
            }
            RandomMove();
            //GhostMove(ghostArray[currGhost].Calc(grid));
            if (GhostX == PacX && GhostY == PacY)
            {
                score += point_ghost;
            }
            LBL_Score.Text = "Score: " + score;

            if (GhostX == PacX && GhostY == PacY)
            {
                do
                {
                    GhostX = ran.Next(width);
                    GhostY = ran.Next(height);
                } while ((GhostX == PacX && GhostY == PacY) || grid[GhostX, GhostY] == value_wall);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        #region PacMan Methods
        private void RandomMove()
        {
            int temp = (int)(ran.NextDouble() * 4);
            int[] grid = Pac_Grid();
            while (grid[temp + 4] == 1)
            {
                temp = (int)(ran.NextDouble() * 4);
            }
            PacMove(temp);
        }

        private void PacMove(int d) //Remade Sudo - Fitness

        /*

      Remodled Fitness

        fitness = 1.0 (weight) / (time + score) 


       */
        {
            time++;
            score += point_step;

            grid[PacX, PacY] = value_empty;

            if (d == 0)
            {
                if (PacY != 0)
                {
                    PacY--;
                    if (grid[PacX, PacY] == value_wall)
                    {
                        PacY++;
                        score += point_invalid;
                    }
                }
                else
                {
                    score += point_invalid;
                }
            }
            else if (d == 1)
            {
                if (PacY != height - 1)
                {
                    PacY++;
                    if (grid[PacX, PacY] == value_wall)
                    {
                        PacY--;
                        score += point_invalid;
                    }
                }
                else
                {
                    score += point_invalid;
                }
            }
            else if (d == 2)
            {
                if (PacX != 0)
                {
                    PacX--;
                    if (grid[PacX, PacY] == value_wall)
                    {
                        PacX++;
                        score += point_invalid;
                    }
                }
                else
                {
                    score += point_invalid;
                }
            }
            else if (d == 3)
            {
                if (PacX != width - 1)
                {
                    PacX++;
                    if (grid[PacX, PacY] == value_wall)
                    {
                        PacX--;
                        score += point_invalid;
                    }
                }
                else
                {
                    score += point_invalid;
                }
            }

            if (grid[PacX, PacY] == value_food)
            {
                score += point_food;
            }

            grid[PacX, PacY] = value_pacman;
            if (render)
            {
                PacPaint(d);
            }

            double fitness = 1.0 / (score + time);



        }



        private int[] Pac_Grid()
        {
            int[] surrounding = new int[10];

            surrounding[0] = PacX;
            surrounding[1] = PacY;
            surrounding[2] = GhostX;
            surrounding[3] = GhostY;
            if (PacY == 0 || grid[PacX, PacY - 1] == value_wall)
            {
                surrounding[4] = 1;
            }
            else
            {
                surrounding[4] = 0;
            }
            if (PacY + 1 == grid.GetLength(1) || grid[PacX, PacY + 1] == value_wall)
            {
                surrounding[5] = 1;
            }
            else
            {
                surrounding[5] = 0;
            }
            if (PacX == 0 || grid[PacX - 1, PacY] == value_wall)
            {
                surrounding[6] = 1;
            }
            else
            {
                surrounding[6] = 0;
            }
            if (PacX + 1 == grid.GetLength(0) || grid[PacX + 1, PacY] == value_wall)
            {
                surrounding[7] = 1;
            }
            else
            {
                surrounding[7] = 0;
            }

            int dx = 0;
            int dy = 0;
            int TotalMove = 1;
            Boolean food = true;

            do
            {
                for (int x = -TotalMove; x <= TotalMove; x++)
                {
                    if (PacX + x >= 0 && PacX + x < width && food)
                    {
                        if (PacY - (TotalMove - Math.Abs(x)) >= 0)
                        {
                            if (grid[PacX + x, PacY - (TotalMove - Math.Abs(x))] == value_food)
                            {
                                dx = x;
                                dy = -(TotalMove - Math.Abs(x));
                                food = false;
                            }
                        }
                        else if (PacY + (TotalMove - Math.Abs(x)) < height)
                        {
                            if (grid[PacX + x, PacY + (TotalMove - Math.Abs(x))] == value_food)
                            {
                                dx = x;
                                dy = TotalMove - Math.Abs(x);
                                food = false;
                            }
                        }
                    }
                }
                TotalMove++;
                if (TotalMove > width + 1 && TotalMove > height + 1)
                {
                    food = false;
                }
            } while (food);

            surrounding[8] = PacX + dx;
            surrounding[9] = PacY + dy;

            return surrounding;
        }

        private void PacPaint(int d)
        {
            if (d == 0)
            {
                if (!(PacX == GhostX && PacY + 1 == GhostY) && PacY != height - 1)
                {
                    picArr[PacX, PacY + 1].BackColor = Color.LightGray;
                    this.Controls.Add(picArr[PacX, PacY + 1]);
                }
            }
            else if (d == 1)
            {
                if (!(PacX == GhostX && PacY - 1 == GhostY) && PacY != 0)
                {
                    picArr[PacX, PacY - 1].BackColor = Color.LightGray;
                    this.Controls.Add(picArr[PacX, PacY - 1]);
                }
            }
            else if (d == 2)
            {
                if (!(PacX + 1 == GhostX && PacY == GhostY) && PacX != width - 1)
                {
                    picArr[PacX + 1, PacY].BackColor = Color.LightGray;
                    this.Controls.Add(picArr[PacX + 1, PacY]);
                }
            }
            else if (d == 3)
            {
                if (!(PacX - 1 == GhostX && PacY == GhostY) && PacX != 0)
                {
                    picArr[PacX - 1, PacY].BackColor = Color.LightGray;
                    this.Controls.Add(picArr[PacX - 1, PacY]);
                }
            }
            picArr[PacX, PacY].BackColor = Color.Yellow;
            this.Controls.Add(picArr[PacX, PacY]);

        }
        #endregion

        #region Ghost Move Methods
        private void GhostMove(int direction)
        {
            if (steps < move.Length)
            {
                move[steps - 1] = direction;
            }

            if (direction == 0 && GhostX != 0)
            {

                if (GhostX != 0)
                {
                    GhostX--;
                }
                if (grid[GhostX, GhostY] == value_wall)
                {
                    GhostX++;
                }
                if (GhostX != width - 1)
                {
                    if (ghostFood)
                    {
                        grid[GhostX + 1, GhostY] = value_food;
                    }
                    else
                    {
                        grid[GhostX + 1, GhostY] = value_empty;
                    }
                }

            }
            else if (direction == 1 && GhostX != width - 1)
            {
                if (GhostX != width - 1)
                {
                    GhostX++;
                }
                if (grid[GhostX, GhostY] == value_wall)
                {
                    GhostX--;
                }
                if (GhostX != 0)
                {
                    if (ghostFood)
                    {
                        grid[GhostX - 1, GhostY] = value_food;
                    }
                    else
                    {
                        grid[GhostX - 1, GhostY] = value_empty;
                    }
                }
            }
            else if (direction == 2 && GhostY != 0)
            {
                if (GhostY != 0)
                {
                    GhostY--;
                }
                if (grid[GhostX, GhostY] == value_wall)
                {
                    GhostY++;
                }
                if (GhostY != height - 1)
                {
                    if (ghostFood)
                    {
                        grid[GhostX, GhostY + 1] = value_food;
                    }
                    else
                    {
                        grid[GhostX, GhostY + 1] = value_empty;
                    }
                }
            }
            else if (direction == 3 && GhostY != height - 1)
            {
                if (GhostY != width - 1)
                {
                    GhostY++;
                }
                if (grid[GhostX, GhostY] == value_wall)
                {
                    GhostY--;
                }
                if (GhostY != 0)
                {
                    if (ghostFood)
                    {
                        grid[GhostX, GhostY - 1] = value_food;
                    }
                    else
                    {
                        grid[GhostX, GhostY - 1] = value_empty;
                    }
                }
            }

            if (grid[GhostX, GhostY] == value_food)
            {
                ghostFood = true;
            }
            else
            {
                ghostFood = false;
            }

            grid[GhostX, GhostY] = value_pacman;

            if (render)
            {
                GhostPaint(direction);
            }
        }

        private void Ghost_Grid()
        {
            if (PacX - GhostX > 0)
            {
                GhostGrid[0] = 1;
                GhostGrid[1] = 0;
            }
            else if (PacX - GhostX < 0)
            {
                GhostGrid[0] = 0;
                GhostGrid[1] = 1;
            }
            else
            {
                GhostGrid[0] = 0;
                GhostGrid[1] = 0;
            }
            if (PacY - GhostY > 0)
            {
                GhostGrid[2] = 1;
                GhostGrid[3] = 0;
            }
            else if (PacY - GhostY < 0)
            {
                GhostGrid[2] = 0;
                GhostGrid[3] = 1;
            }
            else
            {
                GhostGrid[2] = 0;
                GhostGrid[3] = 0;
            }
            if (GhostX + 1 == grid.GetLength(0) || grid[GhostX + 1, GhostY] == value_wall)
            {
                GhostGrid[4] = 1;
            }
            else
            {
                GhostGrid[4] = 0;
            }
            if (GhostX == 0 || grid[GhostX - 1, GhostY] == value_wall)
            {
                GhostGrid[5] = 1;
            }
            else
            {
                GhostGrid[5] = 0;
            }
            if (GhostY + 1 == grid.GetLength(1) || grid[GhostX, GhostY + 1] == value_wall)
            {
                GhostGrid[6] = 1;
            }
            else
            {
                GhostGrid[6] = 0;
            }
            if (GhostY == 0 || grid[GhostX, GhostY - 1] == value_wall)
            {
                GhostGrid[7] = 1;
            }
            else
            {
                GhostGrid[7] = 0;
            }
        }

        private void GhostPaint(int direction)
        {
            if (direction == 0 && GhostX != width - 1)
            {
                if (ghostFood)
                {
                    picArr[GhostX + 1, GhostY].BackColor = Color.Blue;
                }
                else
                {
                    picArr[GhostX + 1, GhostY].BackColor = Color.LightGray;
                }
                this.Controls.Add(picArr[GhostX + 1, GhostY]);
            }
            else if (direction == 1 && GhostX != 0)
            {
                if (ghostFood)
                {
                    picArr[GhostX - 1, GhostY].BackColor = Color.Blue;
                }
                else
                {
                    picArr[GhostX - 1, GhostY].BackColor = Color.LightGray;
                }
                this.Controls.Add(picArr[GhostX - 1, GhostY]);
            }
            else if (direction == 2 && GhostY != height - 1)
            {
                if (ghostFood)
                {
                    picArr[GhostX, GhostY + 1].BackColor = Color.Blue;
                }
                else
                {
                    picArr[GhostX, GhostY + 1].BackColor = Color.LightGray;
                }
                this.Controls.Add(picArr[GhostX, GhostY + 1]);
            }
            else if (GhostY != 0)
            {
                if (ghostFood)
                {
                    picArr[GhostX, GhostY - 1].BackColor = Color.Blue;
                }
                else
                {
                    picArr[GhostX, GhostY - 1].BackColor = Color.LightGray;
                }
                this.Controls.Add(picArr[GhostX, GhostY - 1]);
            }
            picArr[GhostX, GhostY].BackColor = Color.Red;
            this.Controls.Add(picArr[GhostX, GhostY]);
        }
        #endregion



        private void TimerTick(Object myObject, EventArgs myEventArgs)
        {
            steps++;
            time++;
            GhostMove(ghostArray[currGhost].Calc(GhostGrid));
            //PacMove(pacArray[currPac].Calc(Pac_Grid()));
            RandomMove();
            if (GhostX == PacX && GhostY == PacY)
            {
                score += point_ghost;
            }
            LBL_Score.Text = "Score: " + score;

            if (time % turn_length == 0)
            {
                ghostArray[currGhost].AddScore(-score);
                pacArray[currPac].AddScore(score);

                if (PacX == 0 && PacY == 0)
                {
                    pacArray[currPac].AddScore(point_kill);
                }

                currGhost++;
                currPac++;

                currGhost %= ghostArray.Length;
                currPac %= pacArray.Length;
                if (AutoSave.Checked == true)
                {
                    AutoSave.Checked = true;
                }
                timer.Enabled = false;
                Restart();
                if (AutoSave.Checked == true)
                {
                    AutoSave.Checked = true;
                }
            }
        }


        #region Breeding Methods
        private void Restart()
        {
            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true; 
                DateTime Current = DateTime.Now;
                string TextName = Current.ToString("yyyy-MM-dd-HH-mm-ss"); //Path.Combine();
                TextName = TextName + "-score-" + score + ".txt";
                /*
                 * for (int i = 0; i < TextName.Length; i++)
                 { 
                     if (TextName[i] == ":") 
                 } */
                //bool yeet = AutoSave.Checked;
                
                string local = Path.Combine(BatchLoc, TextName);
                //MessageBox.Show(local);


                //  AutoSave.Checked = true;
                if (score > 174)
                {
                    string text = "";
                    //   string newfile2LOC = "";
                    for (int x = 0; x < pacArray.Length; x++)
                    {
                        text += pacArray[x].ToString();
                        //MessageBox.Show(BatchLoc + TextName);
                        //File.WriteAllText(BatchLoc + TextName, text);

                        /* StreamWriter sw = new StreamWriter(@BatchLoc + TextName + ".txt", true);
                         sw.WriteLine(text);
                         sw.Close(); */
                    }

                    using (StreamWriter sw = File.CreateText(local))
                    {
                        sw.WriteLine(text);

                    }

                }
                

            }

            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true;
            }

            PacX = 10;
            PacY = 100;
            GhostX = width - 1;
            GhostY = height - 1;
            score = 0;


            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = value_food;

                }
            }
            GenWall();
            grid[GhostX, GhostY] = value_ghost;
            grid[0, 0] = value_pacman;

            if (render)
            {
                this.Controls.Clear();
                PaintBoard();
                InitializeComponent();
                this.CBX_Train.Checked = true;
               // if (AutoSave.Checked == true)
                //{
                 //   AutoSave.Checked = true;
                //}
                   
                AutoSave.Checked = true;
              
            }

            cycles++;
            LBL_Round.Text = "Round: " + (cycles % (pacArray.Length * ghostArray.Length));
            LBL_Gen.Text = "Gen: " + gen;

            if (cycles % (pacArray.Length * ghostArray.Length) == 0)
            {
                BreedGhost();
                BreedPac();
                gen++;
            }

            PacX = 0;
            PacY = 0;
            timer.Enabled = true;
            //AutoSave.Checked = true;
            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true;
            }
           
        }

        private void BreedPac()
        {
            int counter1 = 0;
            NN_Pacman[] newGen = new NN_Pacman[pacArray.Length];
            NN_Pacman child = new NN_Pacman(width, height);

            int[] genNew = new int[pacArray.Length];

            for (int x = 0; x < genNew.Length; x++)
            {
                for (int y = 0; y < genNew.Length; y++)
                {
                    if (pacArray[counter1].GetScore() < pacArray[y].GetScore())
                    {
                        counter1 = y;
                    }
                }
                genNew[x] = counter1;
                pacArray[counter1].AddScore(point_kill);
            }

            newGen[0] = pacArray[genNew[0]];
            newGen[1] = pacArray[genNew[1]];
            newGen[0].AddScore(-newGen[0].GetScore());
            newGen[1].AddScore(-newGen[1].GetScore());

            int[] raffle = new int[(genNew.Length + 1) * genNew.Length / 2];
            for (int x = 1; x <= genNew.Length; x++)
            {
                for (int y = 0; y < x; y++)
                {
                    raffle[y] = (x - 1);
                }
            }

            int counter2 = 0;
            double[] parent1;
            double[] parent2;

            for (int x = 2; x < newGen.Length; x++)
            {
                counter1 = (int)(ran.NextDouble() * (pacArray.Length + 1) / 2 * pacArray.Length);
                counter2 = (int)(ran.NextDouble() * (pacArray.Length + 1) / 2 * pacArray.Length);
                parent1 = pacArray[raffle[counter1]].DNA();
                parent2 = pacArray[raffle[counter2]].DNA();

                for (int y = 0; y < parent1.Length; y++)
                {
                    parent1[x] = (parent1[x] + parent2[x]) / 2;
                    if (ran.NextDouble() < 1.1)
                    {
                        parent1[x] += (ran.NextDouble() - 0.5) / 100;
                    }
                }

                child.Child(parent1);
                newGen[x] = child;
            }

            for (int x = 0; x < pacArray.Length; x++)
            {
                pacArray[x] = newGen[x];
            }
        }

        private void BreedGhost()
        {
            int counter1 = 0;
            NN_Ghost[] newGen = new NN_Ghost[ghostArray.Length];
            NN_Ghost child = new NN_Ghost();

            int[] genNew = new int[ghostArray.Length];

            for (int x = 0; x < genNew.Length; x++)
            {
                for (int y = 0; y < genNew.Length; y++)
                {
                    if (ghostArray[counter1].GetScore() < ghostArray[y].GetScore())
                    {
                        counter1 = y;
                    }
                }
                genNew[x] = counter1;
                ghostArray[counter1].AddScore(-1000000);
            }

            newGen[0] = ghostArray[genNew[0]];
            newGen[1] = ghostArray[genNew[1]];
            newGen[0].AddScore(-newGen[0].GetScore());
            newGen[1].AddScore(-newGen[1].GetScore());

            int[] raffle = new int[(genNew.Length + 1) * genNew.Length / 2];
            for (int x = 1; x <= genNew.Length; x++)
            {
                for (int y = 0; y < x; y++)
                {
                    raffle[y] = (x - 1);
                }
            }

            int counter2 = 0;
            double[] parent1;
            double[] parent2;

            for (int x = 2; x < newGen.Length; x++)
            {
                counter1 = (int)(ran.NextDouble() * (ghostArray.Length + 1) / 2 * ghostArray.Length);
                counter2 = (int)(ran.NextDouble() * (ghostArray.Length + 1) / 2 * ghostArray.Length);
                parent1 = ghostArray[raffle[counter1]].DNA();
                parent2 = ghostArray[raffle[counter2]].DNA();

                for (int y = 0; y < parent1.Length; y++)
                {
                    parent1[x] = (parent1[x] + parent2[x]) / 2;
                    if (ran.NextDouble() < 1.1)
                    {
                        parent1[x] += (ran.NextDouble() - 0.5) / 10;
                    }
                }

                child.Child(parent1);
                newGen[x] = child;
            }

            for (int x = 0; x < ghostArray.Length; x++)
            {
                ghostArray[x] = newGen[x];
            }
        }
        #endregion

        #region GUI Activated Methods
        private void BTN_Save_Click(object sender, EventArgs e)
        {
            String file = @"\School-Work\VS Projects\DNA_Pacman\Ghost" + ghostArray.Length + "_0.txt";
            string text = "";
            for (int x = 0; x < ghostArray.Length; x++)
            {
                text += ghostArray[x].ToString();

            }

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            File.WriteAllText(mydocpath /*+ file*/, text);
        }

        private void BTN_Upload_Click(object sender, EventArgs e) //ghost
        {

            string path = null;
            SaveFileDialog save = new SaveFileDialog();

            //BatchLoc = save.FileName.ToString();
            //MessageBox.Show(BatchLoc); 
            if (save.ShowDialog() == DialogResult.OK)
            {
                path = save.FileName.ToString();
                // MessageBox.Show(BatchLoc); 

              

            }




            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line = sr.ReadToEnd();
                    for (int x = 0; x < ghostArray.Length; x++)
                    {
                        line = ghostArray[x].Load(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                LBL_Score.Text = "No";
            }
        }

        private void BTN_Save_Pac_Click(object sender, EventArgs e)
        {

            //String file = @"C:\Users\bhatt2223\Desktop" + pacArray.Length + "_0.txt";
            string text = "";
            string newfile2LOC = "";
            for (int x = 0; x < pacArray.Length; x++)
            {
                text += pacArray[x].ToString();

            }

            // string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //File.WriteAllText(mydocpath /*+ file*/, text);
            //H:\PacMan Repository

            //      File.WriteAllText(@"H:\PacMan Repository", text);




            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "";

            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                newfile2LOC = save.FileName.ToString();

                //StreamWriter sw = new StreamWriter(newfile2LOC, true);
                //sw.WriteLine(newfile2);


                File.WriteAllText(newfile2LOC, text);
                /*

                                for (int i = 0; i < newfile.Length; i++)
                                {
                                    sw.WriteLine(newfile2LOC, newfile[i]); 
                                }
                                sw.Close();
            
                                */
            }








        }

        private void BTN_Upload_Pac_Click(object sender, EventArgs e) // pacman
        {
                        
            string path = null;
            SaveFileDialog save = new SaveFileDialog();

            //BatchLoc = save.FileName.ToString();
            //MessageBox.Show(BatchLoc); 
            if (save.ShowDialog() == DialogResult.OK)
            {
                path = save.FileName.ToString();
                // MessageBox.Show(BatchLoc); 



            }
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line = sr.ReadToEnd();
                    for (int x = 0; x < pacArray.Length; x++)
                    {
                        line = pacArray[x].Load(line);// reverse this for pot. fix
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The file could not be read:");
                
               // LBL_Score.Text = "No";
            }
        }



        private void CBX_Train_Toggle(object sender, EventArgs e)
        {
            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true;
            }
            timer.Enabled = !timer.Enabled;  
            if (AutoSave.Checked == true)
                {
                    AutoSave.Checked = true;
                }
        }

        private void CBX_Render_Toggle(object sender, EventArgs e)
        {
            render = !render;
            if (render)
            {
                PaintBoard();
            }
        }



        private void BTN_Save_Training(object sender, EventArgs e)
        {
            String file = @"H:\PacMan Repository";
            string text = "";
            for (int x = 0; x < move.GetLength(0); x++)
            {
                text += "" + move[x];
            }

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            File.WriteAllText(mydocpath + file, text);

            file = @"H:\PacMan Repository";
            text = "";
            for (int x = 0; x < picture.GetLength(0); x++)
            {
                for (int y = 0; y < picture.GetLength(1); y++)
                {
                    text += picture[x, y];
                }
            }

            mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            File.WriteAllText(mydocpath + file, text);
        }

        private void BTN_Upload_Training(object sender, EventArgs e)
        {
            String file = @"H:\PacMan Repository";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + file;
            String line = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    line = sr.ReadToEnd();
                    for (int x = 0; x < line.Length; x++)
                    {
                        move[x] = int.Parse(line.Substring(x, 1));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                LBL_Score.Text = "No";
            }

            file = @"\School-Work\VS Projects\DNA_Pacman\Picture Data_0.txt";
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + file;
            line = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    line = sr.ReadToEnd();
                    for (int x = 0; x < picture.GetLength(0); x++)
                    {
                        for (int y = 0; y < picture.GetLength(1); y++)
                        {
                            picture[x, y] = int.Parse(line.Substring(x * picture.GetLength(1) + y, 1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                LBL_Score.Text = "No";
            }

            ghostArray[0].Train(picture, move);
        }
        #endregion

        private void Breed_Click(object sender, EventArgs e)
        {
            /*//catch file 1 
            String file = @"H:\PacMan Repository" + pacArray.Length + "_0.txt";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + file;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String line = sr.ReadToEnd();
                    for (int x = 0; x < pacArray.Length; x++)
                    {
                        line = pacArray[x].Load(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                LBL_Score.Text = "No";
            }

            */
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            string file1Loc = "";



            if (theDialog.ShowDialog() == DialogResult.OK)
            {



               file1Loc = theDialog.FileName.ToString();



                // MessageBox.Show(newfile);
            }

            StreamReader sr = new StreamReader(file1Loc);
          string  fileText = sr.ReadToEnd();
            sr.Close();

            // StreamReader






            //catch file 2 
            /*String file2 = @"H:\PacMan Repository" + pacArray.Length + "_1.txt";
            string path2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + file2;
            try
            {
                using (StreamReader sr = new StreamReader(path2))
                {
                    String line = sr.ReadToEnd();
                    for (int x = 0; x < pacArray.Length; x++)
                    {
                        line = pacArray[x].Load(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                LBL_Score.Text = "No";
            }
            */

            string file2Loc = "";



            if (theDialog.ShowDialog() == DialogResult.OK)
            {



                file2Loc = theDialog.FileName.ToString();



                // MessageBox.Show(newfile);
            }

            StreamReader sr2 = new StreamReader(file2Loc);
            string fileText2 = sr2.ReadToEnd();
            sr2.Close();

            //breeding begins


            string[] Lines1 = fileText.Split(';');   //takes the string text file 
            string[] Lines2 = fileText2.Split(';');

            double[] Values1 = new double[(Lines1.Length) - 1];
            double[] Values2 = new double[(Lines2.Length) - 1];

            for(int i = 0; i < Lines1.Length - 1; i++)
            {
                Values1[i] = Double.Parse(Lines1[i]);
            }



            for (int i = 0; i < Lines2.Length - 1; i++)
            {
                Values2[i] = Double.Parse(Lines2[i]);
            }

          //  MessageBox.Show(Lines1[0]);


            //int[] Values1 = Array.ConvertAll(Lines1, s => Int32.Parse(s));   //converts the string values to a weighted int value 
            //double[] Values2 = Array.ConvertAll(Lines2, s => Int32.Parse(s));
            double[] Child; 
            if (Values1.Length <= Values2.Length)
            {

                Child = new double[Values1.Length];
                for (int i = 0; i < Values1.Length; i++)    //compares values between the two string and compares the two 
                {

                    if (Values1[i] > Values2[i])
                    {
                        Child[i] = Values1[i];    //best result is chosen 
                    }
                    else
                    {
                        Child[i] = Values2[i];
                    }

                }
            }
            else
            {
                 Child = new double[Values2.Length];
                for (int i = 0; i < Values2.Length; i++)    //compares values between the two string and compares the two 
                {
                    if (Values1[i] > Values2[i])
                    {
                   //     Child[i] = Values1[i]; //best result is chosen 
                    }
                    else
                    {
                        Child[i] = Values2[i];
                    }
                }
            }
            

            //String Result = @"C:\Users\bhatt2223\Desktop" + Child.Length + "_0.txt";   //Saves to Desktop
            string text = "";
            for (int x = 0; x < Child.Length; x++)
            {
                text += Child[x].ToString();
                text += ";";
            }





/*
            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "";

            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                //newfile

                StreamWriter sw = new StreamWriter(BatchLoc + " Child.txt", true);
                sw.WriteLine(text);
                //         File.WriteAllText(newfile, newfile.ToString());
                sw.Close();

            AutoSave.Checked = true; 
                DateTime Current = DateTime.Now;
                string TextName = Current.ToString("yyyy-MM-dd-HH-mm-ss"); //Path.Combine();
                TextName = TextName + "-score-" + score + ".txt";
            } */

            AutoSave.Checked = true;
            DateTime Current = DateTime.Now;
            string TextName = Current.ToString("yyyy-MM-dd-HH-mm-ss"); //Path.Combine();
            TextName = TextName + "child.txt";            
            
            
            string local = Path.Combine(BatchLoc, TextName);
            
            using (StreamWriter sw = File.CreateText(local))
            {
                sw.WriteLine(text);

            }
        }




         private void button1_Click(object sender, EventArgs e)
        {
            //String file = @"C:\Users\bhatt2223\Desktop" + pacArray.Length + "_0.txt";
            string text = "";
            //   string newfile2LOC = "";
            for (int x = 0; x < pacArray.Length; x++)
            {
                text += pacArray[x].ToString();

            }

            // string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //File.WriteAllText(mydocpath /*+ file*/, text);
            //H:\PacMan Repository

            //      File.WriteAllText(@"H:\PacMan Repository", text);




            // SaveFileDialog save = new SaveFileDialog();

            DateTime Current = DateTime.Now;
            //save.FileName = Current.ToString();

            //MessageBox.Show(Current.ToString());  




            //StreamWriter sw = new StreamWriter(BatchLoc, true);
            //sw.WriteLine(newfile2);


            //File.WriteAllText(BatchLoc, text);

            //System.IO.FileStream fs = System.IO.File.Create(BatchLoc);

            //fs.WriteByte(text);

         /*   using (StreamWriter sw = File.CreateText(BatchLoc))
            {
                sw.WriteLine(text);
                
            }*/


            /*
             * 
             

                            for (int i = 0; i < newfile.Length; i++)
                            {
                                sw.WriteLine(newfile2LOC, newfile[i]); 
                            }
                            sw.Close();
            
                            */

        }

        private void SaveLoc_Click(object sender, EventArgs e)
        {
            /*
            SaveFileDialog save = new SaveFileDialog();

            BatchLoc = save.FileName.ToString();
            //MessageBox.Show(BatchLoc); 
            if (save.ShowDialog() == DialogResult.OK)
            {
                BatchLoc = save.FileName.ToString();
               // MessageBox.Show(BatchLoc); 

                FileLoc.Text = BatchLoc;
            
            
             }
             */

            Form2 Form2 = new Form2();
            Form2.ShowDialog();


            BatchLoc = Form2.BatchLoc;
            //MessageBox.Show(BatchLoc); 


        }

        private void AutoSave_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        private void FileLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true;
            }
            Restart();
            this.CBX_Train.Checked = true;

            if (AutoSave.Checked == true)
            {
                AutoSave.Checked = true;
            }
        }
    }

    }







       

