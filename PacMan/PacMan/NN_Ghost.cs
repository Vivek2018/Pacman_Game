using System;

namespace PacMan
{
    class NN_Ghost
    {
        private double[,] inBias;
        private double[] layer1;
        private double[] weight1;
        private double[,] outBias;
        private double[] output;

        private double[,,] comparsions;

        private Random ran;
        private int score = 0;



        public NN_Ghost()
        {
            ran = new Random();

            layer1 = new double[10];
            weight1 = new double[layer1.Length];
            inBias = new double[layer1.Length, 8];
            outBias = new double[layer1.Length, 4];
            output = new double[outBias.GetLength(1)];

            comparsions = new double[4, 10, output.Length];

            for (int x = 0; x < layer1.Length; x++)
            {
                weight1[x] = ran.NextDouble() - 0.5;
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    inBias[x, y] = ran.NextDouble() - 0.5;
                }
                for (int y = 0; y < output.Length; y++)
                {
                    outBias[x, y] = ran.NextDouble() - 0.5;
                }
            }

        }



        public int Calc(int[] GhostGrid)
        {
            for (int x = 0; x < output.Length; x++)
            {
                output[x] = 0;
            }

            for (int x = 0; x < layer1.Length; x++)
            {
                layer1[x] = 0;
                for (int y = 0; y < inBias.GetLength(1); y++)
                {
                    layer1[x] += GhostGrid[y] * inBias[x, y];
                }
                layer1[x] += weight1[x];
                layer1[x] = 1 / (1 + Math.Pow(Math.E, -(layer1[x] - 0)));

                for (int y = 0; y < output.Length; y++)
                {
                    output[y] += layer1[x] * outBias[x, y];
                }
            }

            int big = 0;
            for (int x = 1; x < 4; x++)
            {
                if (output[x] > output[big])
                {
                    big = x;
                }
            }
            return big;
        }

        public void Train(int[,] picture, int[] move)
        {
            double change = 0.0001;
            double diff1 = 0.0;
            double diff2 = 0.0;


            int answer;
            int[] inputs = new int[picture.GetLength(1)];
            double[] dna = DNA();

            for (int w = 0; w < 500; w++)
            {
                for (int Frame = 0; Frame < move.Length; Frame++)
                {
                    for (int x = 0; x < inputs.Length; x++)
                    {
                        inputs[x] = picture[Frame, x];
                    }

                    answer = Calc(inputs);

                    for (int y = 0; y < 4; y++)
                    {
                        if (y != move[Frame])
                        {
                            diff1 += Math.Pow(output[y], 2);
                        }
                        else
                        {
                            diff1 += Math.Pow(1 - output[y], 2);
                        }
                    }

                    //while (answer != move[Frame]){ 
                    for (int x = 0; x < dna.Length; x++)
                    {
                        dna[x] += change;
                        Child(dna);

                        diff2 = 0.0;

                        Calc(inputs);

                        for (int y = 0; y < 4; y++)
                        {
                            if (y != move[Frame])
                            {
                                diff2 += Math.Pow(output[y], 2);
                            }
                            else
                            {
                                diff2 += Math.Pow(1 - output[y], 2);
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

                        answer = Calc(inputs);
                    }
                    //}
                }
            }
            Child(dna);
        }



        public void UploadData(int[,] picture, int[] move)
        {
            int x0 = 0;
            int x1 = 0;
            int x2 = 0;
            int x3 = 0;

            int[] inputs = new int[picture.GetLength(1)];

            for (int Frame = 0; Frame < move.Length; Frame++)
            {
                for (int x = 0; x < inputs.Length; x++)
                {
                    inputs[x] = picture[Frame, x];
                }

                Calc(inputs);

                if (move[Frame] == 0 && x0 < comparsions.GetLength(1))
                {
                    for (int y = 0; y < output.Length; y++)
                    {
                        comparsions[0, x0, y] = output[y];
                    }
                    x0++;
                }
                else if (move[Frame] == 1 && x1 < comparsions.GetLength(1))
                {
                    for (int y = 0; y < output.Length; y++)
                    {
                        comparsions[1, x1, y] = output[y];
                    }
                    x1++;
                }
                else if (move[Frame] == 2 && x2 < comparsions.GetLength(1))
                {
                    for (int y = 0; y < output.Length; y++)
                    {
                        comparsions[2, x2, y] = output[y];
                    }
                    x2++;
                }
                else if (x3 < comparsions.GetLength(1))
                {
                    for (int y = 0; y < output.Length; y++)
                    {
                        comparsions[3, x3, y] = output[y];
                    }
                    x3++;
                }
            }

        }

        public int Calc2(int[] GhostGrid)
        {
            Calc(GhostGrid);

            int move = 0;
            double diff1 = 0.0;
            double diff2 = 0.0;

            for (int z = 0; z < comparsions.GetLength(2); z++)
            {
                diff1 += Math.Pow(comparsions[0, 0, z] - output[z], 2);
            }

            for (int x = 0; x < comparsions.GetLength(0); x++)
            {
                for (int y = 0; y < comparsions.GetLength(1); y++)
                {
                    if (comparsions[x, y, 0] != 0)
                    {
                        diff2 = 0;
                        for (int z = 0; z < comparsions.GetLength(2); z++)
                        {
                            diff2 += Math.Pow(comparsions[x, y, z] - output[z], 2);
                        }

                        if (diff2 < diff1)
                        {
                            diff1 = diff2;
                            move = x;
                        }
                    }
                }
            }

            return move;
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
                for (int y = 0; y < output.Length; y++)
                {
                    text += outBias[x, y] + ";";
                }
            }

            return text;
        }

        public String Load(string text)
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
                for (int y = 0; y < output.Length; y++)
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
                for (int y = 0; y < output.Length; y++)
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
