using System;
using System.Linq;

namespace AOC19
{
    
    class Aoc02 : AocBase
    {
        public Aoc02(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            
            // before run 
            intcodes[1] = 12;
            intcodes[2] = 2;
            return Compute(intcodes).ToString();
        }
        
        public override string PartB(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            for (int noun = 0; noun < 99; noun++)
            {
                for(int verb = 0; verb < 99; verb++)
                {
                    var local = (int[]) intcodes.Clone();
                    local[1] = noun;
                    local[2] = verb;
                    if(Compute(local) == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }       
                }
            }
            return "not found";
        }
        private int Compute(int[] intcodes)
        {
            for(int i = 0; i< intcodes.Length; i+=4)
            {
                var opcode = intcodes[i];
                if(opcode == 99) 
                {
                    //halt
                    break;
                }    
                else if(opcode == 1)
                {
                    //add
                    intcodes[intcodes[i+3]] = intcodes[intcodes[i+1]] + intcodes[intcodes[i+2]];
                }
                else if(opcode == 2)
                {
                    //mult
                    intcodes[intcodes[i+3]] = intcodes[intcodes[i+1]] * intcodes[intcodes[i+2]];
                }
                else
                {
                    Console.WriteLine("ERR!!!!!");
                }

            }
            return intcodes[0];
        }
    }
}