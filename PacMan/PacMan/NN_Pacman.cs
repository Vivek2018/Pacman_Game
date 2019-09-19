using System;


namespace PacMan
{
    class NN_Pacman
    {
        private double[,] inBias;
        private double[] weight1;
        private double[] layer1;
        private double[,] outBias;
        double[] output;
        private Random ran;
        private int score = 0;

        public NN_Pacman(int w, int h)
        {
            ran = new Random();

            layer1 = new double[10];
            weight1 = new double[layer1.Length];
            inBias = new double[layer1.Length, 5 * 5];
            outBias = new double[layer1.Length, 4];
            output = new double[4];

            for (int x = 0; x < layer1.Length; x++)
            {
                layer1[x] = 0;
                weight1[x] = ran.NextDouble();
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    inBias[x, y] = ran.NextDouble();
                }
                for (int y = 0; y < 4; y++)
                {
                    outBias[x, y] = ran.NextDouble();
                }
            }

        }

        public int Calc(int[,] grid)
        {
            output[0] = 0;
            output[1] = 0;
            output[2] = 0;
            output[3] = 0;
            for (int x = 0; x < layer1.Length; x++)
            {
                layer1[x] = 0;
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    layer1[x] += grid[y % grid.GetLength(0), y / grid.GetLength(0)] * inBias[x, y];
                }
                layer1[x] += weight1[x];
                layer1[x] = Math.Pow(Math.E, 0.0001 * layer1[x]) / (Math.Pow(Math.E, 0.0001 * layer1[x]) + 1);

                for (int y = 0; y < 4; y++)
                {
                    output[y] += layer1[x] * outBias[x, y];
                    output[y] = Math.Pow(Math.E, 0.01 * output[y]) / (Math.Pow(Math.E, 0.01 * output[y]) + 1);
                }
            }

            int big = 0;
            for (int x = 1; x < 4; x++)
            {
                if (output[big] < output[x])
                {
                    big = x;
                }
            }
            return big;
        }





        public void Train(int[,,] picture, int[] move)
        {
            double change = 0.001;
            double diff1 = 0.0;
            double diff2 = 0.0;


            int answer;
            int[,] surround = new int[5, 5];


            for (int w = 0; w < 5; w++)
            {
                for (int Frame = 0; Frame < move.Length; Frame++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        for (int y = 0; y < 5; y++)
                        {
                            surround[x, y] = picture[Frame, x, y];
                        }
                    }

                    answer = Calc(surround);

                    while (answer != move[Frame])
                    {
                        double[] dna = DNA();

                        for (int x = 0; x < 4; x++)
                        {
                            if (x != move[Frame])
                            {
                                diff1 += Math.Abs(output[x]);
                            }
                            else
                            {
                                diff1 += Math.Abs(1 - output[x]);
                            }
                        }

                        for (int x = 0; x < dna.Length; x++)
                        {
                            dna[x] += change;
                            diff2 = 0.0;

                            Calc(surround);

                            for (int y = 0; y < 4; y++)
                            {
                                if (y != move[Frame])
                                {
                                    diff2 += Math.Abs(output[y]);
                                }
                                else
                                {
                                    diff2 += Math.Abs(1 - output[y]);
                                }
                            }

                            if (diff2 > diff1)
                            {
                                dna[x] -= 2 * change;
                            }
                            else
                            {
                                diff1 = diff2;
                            }

                            answer = Calc(surround);
                        }
                    }

                }
            }
        }





        override public string ToString()
        {
            String text = "";

            for (int x = 0; x < weight1.Length; x++)
            {
                text += weight1[x] + ";";
            }
            for (int x = 0; x < layer1.Length; x++)
            {
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    text += inBias[x, y] + ";";
                }
            }
            for (int x = 0; x < layer1.Length; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    text += outBias[x, y] + ";";
                }
            }

            return text;
        }

        public string Load(string text)
        {

            for (int x = 0; x < layer1.Length; x++)
            {
                int index = text.IndexOf(";");
                weight1[x] = double.Parse(text.Substring(0, index));
                text = text.Substring(index + 1);
            }
            for (int x = 0; x < layer1.Length; x++)
            {
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    int index = text.IndexOf(";");
                    inBias[x, y] = double.Parse(text.Substring(0, index));
                    text = text.Substring(index + 1);
                }
            }
            for (int x = 0; x < layer1.Length; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    int index = text.IndexOf(";");
                    outBias[x, y] = double.Parse(text.Substring(0, index));
                    text = text.Substring(index + 1);
                }
            }

            return text;
        }





        public double[] DNA()
        {
            double[] dna = new double[weight1.Length + inBias.Length + outBias.Length];
            for (int x = 0; x < weight1.Length; x++)
            {
                dna[x] = weight1[x];
            }
            int c1 = 0;
            int c2 = 0;
            for (int x = weight1.Length; x < weight1.Length + inBias.Length;)
            {
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    dna[x] = inBias[c1, c2];
                    c2++;
                    x++;
                }
                c1++;
                c2 = 0;
                x++;
            }
            c1 = 0;
            c2 = 0;
            for (int x = weight1.Length + inBias.Length; x < dna.Length;)
            {
                for (int y = 0; y < outBias.GetLength(1); y++)
                {
                    dna[x] = outBias[c1, c2];
                    c2++;
                    x++;
                }
                c1++;
                c2 = 0;
                x++;
            }

            return dna;
        }

        public void Child(double[] dna)
        {

            for (int x = 0; x < layer1.Length; x++)
            {
                weight1[x] = dna[x];
            }
            for (int x = layer1.Length; x < layer1.Length + inBias.GetLength(0); x++)
            {
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    inBias[x - layer1.Length, y] = dna[x + y * layer1.Length];
                }
            }
            for (int x = layer1.Length + inBias.Length; x < layer1.Length + inBias.Length + outBias.GetLength(0); x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    outBias[x - (layer1.Length + inBias.Length), y] = dna[x + y * outBias.GetLength(0)];
                }
            }

        }





        public void AddScore(int value)
        {
            score += value;
        }

        public int GetScore()
        {
            return score;
        }
    }
}