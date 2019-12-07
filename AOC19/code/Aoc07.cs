using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class Aoc07 : AocBase
    {
        public Aoc07(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            Stack<int> inputStack  = new Stack<int>();
            int maxSignal = int.MinValue;
            
            for(int a = 0; a < 5; a++) //Amp A
            {
                for(int b = 0; b < 5; b++) //Amp B
                {
                    if(b != a)
                    {
                        for(int c = 0; c < 5; c++) //Amp C
                        {
                            if(c != a && c != b)
                            {
                                for(int d = 0; d < 5; d++) //Amp D
                                {
                                    if(d != a && d != b && d != c)
                                    {
                                        for(int e = 0; e < 5; e++) //Amp E
                                        {
                                            if(e != a && e != b && e != c && e != d) 
                                            {
                                                inputStack.Push(0);
                                                inputStack.Push(a);
                                                inputStack.Push(Compute(intcodes, inputStack));
                                                inputStack.Push(b);
                                                inputStack.Push(Compute(intcodes, inputStack));
                                                inputStack.Push(c);
                                                inputStack.Push(Compute(intcodes, inputStack));
                                                inputStack.Push(d);
                                                inputStack.Push(Compute(intcodes, inputStack));
                                                inputStack.Push(e);
                                                int signal = Compute(intcodes, inputStack);
                                                if(signal > maxSignal)
                                                {
                                                    maxSignal = signal;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return maxSignal.ToString();
        }


        public override string PartB(string[] inputs)
        {
            return "Not implemented";
        }

        private int Compute(int[] intcodes, Stack<int> inputStack)
        {
            int pc = 0;
            int output = 0;
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
                    //read input from console
                    //Console.WriteLine("Enter a valid integer input: ");
                    //intcodes[intcodes[pc+1]] = int.Parse(Console.ReadLine());
                    //read input from stack
                    intcodes[intcodes[pc+1]] = inputStack.Pop();
                    pc += 2;
                }
                else if(opcode == 4)
                {
                    //write output
                    output = p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1];
                    Console.WriteLine(output);
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
            return output;
        }
    }
}