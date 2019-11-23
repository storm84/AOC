using System;
using System.Collections.Generic;
using System.Linq;
namespace AOC18
{
    class Aoc04 : AocBase
    {
        public Aoc04() : base(@"data/input04.txt")
        {
        }
        
        public override string executeA(string[] inputs)
        {
            List<string> inputList = inputs.ToList();
            inputList.Sort();
            //debug
            //foreach(var input in inputList)
            //{
            //    Console.WriteLine(input);
            //}
            return "Not Implemented";
        }

        public override string executeB(string[] inputs)
        {
            return "Not Implemented";
        }
        

        

    }
}