using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class Aoc08 : AocBase
    {
        public Aoc08(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            List<List<string>> layers = new List<List<string>>();
            int ptr = 0;
            int zeroMin = int.MaxValue;
            int selectedLayer = -1;
            for(int l = 0; l < inputs[0].Length / (25 * 6); l++)
            {
                layers.Add(new List<string>());
                int zeroCnt = 0;
                for(int r = 0; r < 6; r++)
                {
                    layers[l].Add(inputs[0].Substring(ptr, 25));
                    ptr += 25;
                    zeroCnt += layers[l][r].Count(c => c == '0');
                }
                if(zeroCnt < zeroMin)
                {
                    zeroMin = zeroCnt;
                    selectedLayer = l;
                }
            }

            int ones =0, twos = 0;
            foreach(var s in layers[selectedLayer])
            {
                ones += s.Count(c => c == '1');
                twos += s.Count(c => c == '2');
            }

            return (ones * twos).ToString();
        }

        public override string PartB(string[] inputs)
        {
            List<List<string>> layers = new List<List<string>>();
            int ptr = 0;
            for(int l = 0; l < inputs[0].Length / (25 * 6); l++)
            {
                layers.Add(new List<string>());
                for(int r = 0; r < 6; r++)
                {
                    layers[l].Add(inputs[0].Substring(ptr, 25));
                    ptr += 25;
                }
 
            }
            char[,] screen = new char[6,25];
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 25; j++)
                {
                    screen[i,j] = '2';
                }
            }
            foreach(var layer in layers)
            {
                for(int i = 0; i < layer.Count; i++)
                {
                    for(int j = 0; j < layer[i].Length; j++)
                    {
                        if(screen[i,j] == '2')
                        {
                            screen[i,j] = layer[i][j];
                        }
                    }
                }
            }
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 25; j++)
                {
                    Console.Write(screen[i,j] == '0' ? " " : "#" );
                }
                Console.Write("\n");
            }
            return "done";
        }
    }
}