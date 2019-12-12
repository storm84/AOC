using System.Collections.Generic;
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
            var inputQueue = new Queue<long>();
            inputQueue.Enqueue(0);
            //computer.Compute(inputQueue, new Queue<long>());
            return "not implemented";
        }

        public override string PartB(string[] inputs)
        {
            return "not implemented";
        }
    }
}