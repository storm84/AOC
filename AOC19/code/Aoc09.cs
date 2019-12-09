using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class Aoc09 : AocBase
    {
        public Aoc09(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(long.Parse).ToArray();
            long[] mem = new long[intcodes.Length * 10];
            intcodes.CopyTo(mem,0);
            Queue<long> inputQueue = new Queue<long>();
            inputQueue.Enqueue(1);
            return Compute(mem,inputQueue, new Queue<long>()).Dequeue().ToString();           
        }


        public override string PartB(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(long.Parse).ToArray();
            long[] mem = new long[intcodes.Length * 10];
            intcodes.CopyTo(mem,0);
            Queue<long> inputQueue = new Queue<long>();
            inputQueue.Enqueue(2);
            return Compute(mem,inputQueue, new Queue<long>()).Dequeue().ToString();           

        }

        
        private long GetParameter(long [] intcodes, long index, char mode, long relativeBase)
        {
            switch(mode)
            {               
                case '1': return intcodes[index];                               //1 == immediate mode
                case '2': return intcodes[relativeBase + intcodes[index]];      //2 == relative mode
                default : return intcodes[intcodes[index]];                     //0 == position mode
            }
        }
        private void SetParameter(long [] intcodes, long index, char mode, long relativeBase, long value)
        {
            switch(mode)
            {               
                case '1': intcodes[index] = value; break;                           //1 == immediate mode
                case '2': intcodes[relativeBase + intcodes[index]] = value; break;  //2 == relative mode
                default : intcodes[intcodes[index]] = value; break;                 //0 == position mode
            }
        }

        private Queue<long> Compute(long[] intcodes, Queue<long> inputQueue, Queue<long> outputQueue)
        {
            long pc = 0;    //program counter
            long rb = 0;    //relative base
            while(pc < intcodes.Length)
            {
                var instruction = intcodes[pc].ToString("00000");
                long opcode = int.Parse(instruction.Substring(instruction.Length-2));
                char p1Mode = instruction[2];
                char p2Mode = instruction[1];
                char p3Mode = instruction[0];

                if(opcode == 99) 
                {
                    //halt
                    break;
                }    
                else if(opcode == 1)
                {
                    //add
                    SetParameter(intcodes, pc+3, p3Mode, rb, GetParameter(intcodes, pc+1, p1Mode, rb) + GetParameter(intcodes, pc+2, p2Mode, rb));
                    pc += 4;
                }
                else if(opcode == 2)
                {
                    //mult
                    SetParameter(intcodes, pc+3, p3Mode, rb, GetParameter(intcodes, pc+1, p1Mode, rb) * GetParameter(intcodes, pc +2, p2Mode, rb));
                    pc += 4;
                }
                else if(opcode == 3)
                {                   
                    //read input from inputQueue
                    bool spinlock = true;
                    while (spinlock)
                    {
                        lock(inputQueue){
                            if(inputQueue.Any())
                            {
                                SetParameter(intcodes, pc+1, p1Mode, rb, inputQueue.Dequeue());
                                spinlock = false;
                            }
                         }
                    }
                    pc += 2;
                }
                else if(opcode == 4)
                {
                    //write output
                    var output = GetParameter(intcodes, pc+1, p1Mode, rb);
                    //Console.WriteLine(output);
                    lock(outputQueue)
                    {
                        outputQueue.Enqueue(output);
                    }
                    pc += 2;
                }
                else if(opcode == 5)
                {
                    //jump if true
                    if(GetParameter(intcodes, pc+1, p1Mode, rb) != 0 )
                    {
                        pc = GetParameter(intcodes, pc+2, p2Mode, rb);
                    }
                    else
                    {
                        pc += 3;
                    }

                }
                else if(opcode == 6)
                {
                    //jump if false
                    if(GetParameter(intcodes, pc+1, p1Mode, rb) == 0 )
                    {
                        pc = GetParameter(intcodes, pc+2, p2Mode, rb);
                    }
                    else
                    {
                        pc += 3;
                    }
                }
                else if(opcode == 7)
                {
                    //less than
                    if(GetParameter(intcodes, pc+1, p1Mode, rb) < GetParameter(intcodes, pc+2, p2Mode, rb))
                    {
                        SetParameter(intcodes, pc+3, p3Mode, rb, 1);
                    }
                    else
                    {
                        SetParameter(intcodes, pc+3, p3Mode, rb, 0);
                    }
                    pc += 4;
                }
                else if(opcode == 8)
                {
                    //equals
                    if(GetParameter(intcodes, pc+1, p1Mode, rb) == GetParameter(intcodes, pc+2, p2Mode, rb))
                    {
                        SetParameter(intcodes, pc+3, p3Mode, rb, 1);
                    }
                    else
                    {
                        SetParameter(intcodes, pc+3, p3Mode, rb, 0);
                    }
                    pc += 4;
                }
                else if(opcode == 9)
                {
                    rb += GetParameter(intcodes, pc + 1, p1Mode, rb);
                    pc += 2;
                }
                else
                {
                    Console.WriteLine("ERR!!!!!");
                }

            }
            return outputQueue;
        }
    }
}