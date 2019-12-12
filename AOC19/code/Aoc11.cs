using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC19
{
    class Aoc11 : AocBase
    {
        public Aoc11(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var program = inputs[0].Split(',').Select(long.Parse).ToArray();
            var computer = new IntecodeComputer(program);
            computer.OutputToConsole = true;
            var inputOutputQueue = new Queue<long>();
            inputOutputQueue.Enqueue(0);
            computer.Compute(inputOutputQueue, inputOutputQueue);
            return "not implemented";
        }

        public override string PartB(string[] inputs)
        {
            return "not implemented";
        }
    }
    class Robot
    {
        public Robot(IntecodeComputer icc)
        {
            intecodeComputer = icc;
        }
        private int posX = 0, posY = 0, xDir = 0, yDir = 0;
        private IntecodeComputer intecodeComputer;
        private Dictionary<Point,char> grid = new Dictionary<Point, char>();
        private void Run()
        {       var p = new Point(posX,posY);
                char color;
                if(grid.ContainsKey(p))
                {
                    color = grid[p];
                }
                else{
                    color = '.';
                    grid.Add(p,color);
                }
        }

        
    }
}