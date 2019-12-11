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
                        int meteorCount = GetVectorsForMeteors(inputs,x,y).Count;
                        if(meteorCount > maxCnt)
                        {
                            maxCnt = meteorCount;
                        }
                    }
            }
            return maxCnt.ToString();
        }

        private List<Vector2> GetVectorsForMeteors(string[] inputs, int xIn, int yIn)
        {
            List<Vector2> vectorList = new List<Vector2>();
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
                        var v2 = new Vector2(xtmp,ytmp);
                        if(!vectorList.Contains(v2) )
                        {
                            vectorList.Add(v2);
                        }
                    }
                }
            }
            return vectorList;
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
            int stationX, stationY;
            List<Vector2> vectorList = new List<Vector2>();
            for(int y = 0; y < inputs.Length; y++)
            {
                for(int x = 0; x < inputs[y].Length; x++)
                    if(inputs[y][x] == '#')
                    {
                        var v2List  = GetVectorsForMeteors(inputs,x,y);
                        if(v2List.Count > vectorList.Count)
                        {
                            vectorList = v2List;
                            stationX = x;
                            stationY = y;
                        }
                    }
            }
            // how to sort the vectors?
            return "not implemented";
        }
    }
}