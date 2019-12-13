using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class Aoc13 : AocBase
    {
        public Aoc13(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var program = inputs[0].Split(',').Select(long.Parse).ToArray();
            var computer = new IntecodeComputer(program);
            Dictionary<Point,int> screen = new Dictionary<Point, int>();
            computer.Compute();
            while(computer.OutputQueue.Any())
            {
                if(computer.OutputQueue.Count < 3)
                    throw new System.Exception("Invalid queue count");
                var p = new Point((int)computer.OutputQueue.Dequeue(),(int)computer.OutputQueue.Dequeue());
                int tile = (int) computer.OutputQueue.Dequeue();
                if(screen.ContainsKey(p))
                {
                    screen[p] = tile;
                }
                else
                {
                    screen.Add(p,tile);
                }
            }
            return screen.Values.Where(x=> x == 2).Count().ToString();
        }

        public override string PartB(string[] inputs)
        {
            return "not implemented";
        }
        
        private struct Point
        {
            public int X;
            public int Y; 
            public Point(int x, int y)
            {
                X= x;
                Y= y;
            }
        }
    }
}