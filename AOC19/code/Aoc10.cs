using System;
using System.Collections.Generic;
using System.Numerics;

namespace AOC19
{
    class Aoc10 : AocBase
    {
        public Aoc10(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            int maxCnt = 0;
            for(int y = 0; y < inputs.Length; y++)
            {
                for(int x = 0; x < inputs[y].Length; x++)
                    if(inputs[y][x] == '#')
                    {
                        int meteorCount = CountMeteors(inputs,x,y);
                        if(meteorCount > maxCnt)
                        {
                            maxCnt = meteorCount;
                        }
                    }
            }
            return maxCnt.ToString();
        }

        private int CountMeteors(string[] inputs, int xIn, int yIn)
        {
            List<string> vectorList = new List<string>();
            for(int y = 0; y < inputs.Length; y++)
            {
                for(int x = 0; x < inputs[y].Length; x++)
                {
                    if((x != xIn || y!=yIn) && inputs[y][x] == '#')
                    {
                        var xtmp = x - xIn;
                        var ytmp = y - yIn;
                        int gcd = GCD(xtmp,ytmp);
                        xtmp = xtmp/gcd;
                        ytmp = ytmp/gcd;
                        if(!vectorList.Contains($"X{xtmp}_Y{ytmp}"))
                        {
                            vectorList.Add($"X{xtmp}_Y{ytmp}");
                        }
                    }
                }
            }
            return vectorList.Count;
        }

        private int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            if(a==0)
                return b;
            if(b==0)
                return a;
            if(Math.Abs(a) > Math.Abs(b))
                return GCD(a % b, b);
            else
                return GCD(a, b % a);
        }
        public override string PartB(string[] inputs)
        {
            return "not implemented";
        }
    }
}