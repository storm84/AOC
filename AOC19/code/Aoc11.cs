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
            //computer.OutputToConsole = true;
            PaintingRobot rob = new PaintingRobot(computer);
            rob.Run(0);
            return rob.PaintedPanelCount.ToString();
        }

        public override string PartB(string[] inputs)
        {
            var program = inputs[0].Split(',').Select(long.Parse).ToArray();
            var computer = new IntecodeComputer(program);
            //computer.OutputToConsole = true;
            PaintingRobot rob = new PaintingRobot(computer);
            rob.Run(1);
            rob.Print();
            return "done!";
            
        }
    }
}