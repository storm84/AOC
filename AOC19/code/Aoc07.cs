using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var maxSignal = FindMaxSignal(intcodes, 0);
            return maxSignal.ToString();
        }


        public override string PartB(string[] inputs)
        {
            var intcodes = inputs[0].Split(',').Select(int.Parse).ToArray();
            var maxSignal = FindMaxSignal(intcodes, 5);
            return maxSignal.ToString();           

        }



        private int FindMaxSignal(int[] intcodes, int offset)
        {
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
                                                int signal = runAmps(intcodes, a+offset, b+offset, c+offset, d+offset, e+offset);
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
            return maxSignal;
        }
        
        private int runAmps(int[] intcodes, int a, int b, int c, int d, int e)
        {
            List<Queue<int>> queues = new List<Queue<int>>{ new Queue<int>(), new Queue<int>(), new Queue<int>(), new Queue<int>(), new Queue<int>() };

            //init queues
            queues[0].Enqueue(a);
            queues[0].Enqueue(0);  //startinput
            queues[1].Enqueue(b);
            queues[2].Enqueue(c);
            queues[3].Enqueue(d);
            queues[4].Enqueue(e);
            
            var t1 = Task.Run(() => Compute(intcodes.ToArray(), queues[0], queues[1]));
            var t2 = Task.Run(() => Compute(intcodes.ToArray(), queues[1], queues[2]));
            var t3 = Task.Run(() => Compute(intcodes.ToArray(), queues[2], queues[3]));
            var t4 = Task.Run(() => Compute(intcodes.ToArray(), queues[3], queues[4]));
            var t5 = Task.Run(() => Compute(intcodes.ToArray(), queues[4], queues[0]));
            Task.WaitAll(t1,t2,t3,t4,t5); 

            return t5.Result.Last();
        }
        
        private Queue<int> Compute(int[] intcodes, Queue<int> inputQueue, Queue<int> outputQueue)
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
                    //read input from console
                    //Console.WriteLine("Enter a valid integer input: ");
                    //intcodes[intcodes[pc+1]] = int.Parse(Console.ReadLine());
                    
                    //read input from inputQueue
                    bool spinlock = true;
                    while (spinlock)
                    {
                        lock(inputQueue){
                            if(inputQueue.Any())
                            {
                                intcodes[intcodes[pc+1]] = inputQueue.Dequeue();
                                spinlock = false;
                            }
                         }
                    }
                    pc += 2;
                }
                else if(opcode == 4)
                {
                    //write output
                    var output = p1PosMode ? intcodes[intcodes[pc+1]] : intcodes[pc+1];
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
            return outputQueue;
        }
    }
}