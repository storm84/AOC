using System;
using System.Linq;
namespace AOC19
{
    class Aoc05 : AocBase
    {
        public Aoc05(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            Console.WriteLine("(Use \"1\" as input.)");
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            Compute(intcodes);
            return"! Last output above is the diagnostic code.";
            
        }

        public override string PartB(string[] inputs)
        {
            Console.WriteLine("(Use \"5\" as input.)");
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            Compute(intcodes);
            return"! Last output above is the diagnostic code.";
        }

        private void Compute(int[] intcodes)
        {
            int pc = 0;
            while(pc < intcodes.Length)
            {
                var instruction = intcodes[pc].ToString("00000");
                int opcode = int.Parse(instruction.Substring(instruction.Length-2));
                bool p1PosMode = instruction[2] == '0';
                bool p2PosMode = instruction[1] == '0';
                bool p3PosMode = instruction[0] == '0';

                if(opcode == 99) 
                {
                    //halt
                    break;
                }    
                else if(opcode == 1)
                {
                    //add
                    intcodes[intcodes[pc+3]] = (p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) + (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] );
                    pc += 4;
                }
                else if(opcode == 2)
                {
                    //mult
                    intcodes[intcodes[pc+3]] =  (p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) * (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] );
                    pc += 4;
                }
                else if(opcode == 3)
                {
                    //read input
                    Console.WriteLine("Enter a valid integer input: ");
                    intcodes[intcodes[pc+1]] = int.Parse(Console.ReadLine());
                    pc += 2;
                }
                else if(opcode == 4)
                {
                    //write output
                    Console.WriteLine(p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1]);
                    pc += 2;
                }
                else if(opcode == 5)
                {
                    //jump if true
                    if((p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) != 0 )
                    {
                        pc = (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] );
                    }
                    else
                    {
                        pc += 3;
                    }

                }
                else if(opcode == 6)
                {
                    //jump if false
                    if((p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) == 0 )
                    {
                        pc = (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] );
                    }
                    else
                    {
                        pc += 3;
                    }
                }
                else if(opcode == 7)
                {
                    //less than
                    if((p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) < (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] ))
                    {
                        intcodes[intcodes[pc+3]] = 1;
                    }
                    else
                    {
                        intcodes[intcodes[pc+3]] = 0;
                    }
                    pc += 4;
                }
                else if(opcode == 8)
                {
                    //equals
                    if((p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1] ) == (p2PosMode ? intcodes[intcodes[pc+2]] : intcodes[pc+2] ))
                    {
                        intcodes[intcodes[pc+3]] = 1;
                    }
                    else
                    {
                        intcodes[intcodes[pc+3]] = 0;
                    }
                    pc += 4;
                }
                else
                {
                    Console.WriteLine("ERR!!!!!");
                }
            }
        }
    }
}