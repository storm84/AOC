using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class IntecodeComputer
    {
        public IntecodeComputer(long [] program)
        {
            //Expand memory
            memory = new long[program.Length * 10];
            program.CopyTo(memory,0);
        }
        private long [] memory;
        public bool OutputToConsole { get; set; }

        private long GetParameter(long index, char mode, long relativeBase)
        {
            switch(mode)
            {               
                case '1': return memory[index];                               //1 == immediate mode
                case '2': return memory[relativeBase + memory[index]];      //2 == relative mode
                default : return memory[memory[index]];                     //0 == position mode
            }
        }
        private void SetParameter(long index, char mode, long relativeBase, long value)
        {
            switch(mode)
            {               
                case '1': memory[index] = value; break;                           //1 == immediate mode
                case '2': memory[relativeBase + memory[index]] = value; break;  //2 == relative mode
                default : memory[memory[index]] = value; break;                 //0 == position mode
            }
        }

        public Queue<long> Compute(Queue<long> inputQueue, Queue<long> outputQueue)
        {
            long pc = 0;    //program counter
            long rb = 0;    //relative base
            while(pc < memory.Length)
            {
                var instruction = memory[pc].ToString("00000");
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
                    SetParameter(pc+3, p3Mode, rb, GetParameter(pc+1, p1Mode, rb) + GetParameter(pc+2, p2Mode, rb));
                    pc += 4;
                }
                else if(opcode == 2)
                {
                    //mult
                    SetParameter(pc+3, p3Mode, rb, GetParameter(pc+1, p1Mode, rb) * GetParameter(pc +2, p2Mode, rb));
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
                                SetParameter(pc+1, p1Mode, rb, inputQueue.Dequeue());
                                spinlock = false;
                            }
                         }
                    }
                    pc += 2;
                }
                else if(opcode == 4)
                {
                    //write output
                    var output = GetParameter(pc+1, p1Mode, rb);
                    if(OutputToConsole)
                        Console.WriteLine(output);
                    lock(outputQueue)
                    {
                        outputQueue.Enqueue(output);
                    }
                    pc += 2;
                }
                else if(opcode == 5)
                {
                    //jump if true
                    if(GetParameter(pc+1, p1Mode, rb) != 0 )
                    {
                        pc = GetParameter(pc+2, p2Mode, rb);
                    }
                    else
                    {
                        pc += 3;
                    }

                }
                else if(opcode == 6)
                {
                    //jump if false
                    if(GetParameter(pc+1, p1Mode, rb) == 0 )
                    {
                        pc = GetParameter(pc+2, p2Mode, rb);
                    }
                    else
                    {
                        pc += 3;
                    }
                }
                else if(opcode == 7)
                {
                    //less than
                    if(GetParameter(pc+1, p1Mode, rb) < GetParameter(pc+2, p2Mode, rb))
                    {
                        SetParameter(pc+3, p3Mode, rb, 1);
                    }
                    else
                    {
                        SetParameter(pc+3, p3Mode, rb, 0);
                    }
                    pc += 4;
                }
                else if(opcode == 8)
                {
                    //equals
                    if(GetParameter(pc+1, p1Mode, rb) == GetParameter(pc+2, p2Mode, rb))
                    {
                        SetParameter(pc+3, p3Mode, rb, 1);
                    }
                    else
                    {
                        SetParameter(pc+3, p3Mode, rb, 0);
                    }
                    pc += 4;
                }
                else if(opcode == 9)
                {
                    rb += GetParameter(pc + 1, p1Mode, rb);
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