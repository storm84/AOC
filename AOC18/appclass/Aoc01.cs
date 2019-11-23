using System;
using System.IO;
using System.Collections.Generic;

namespace AOC18
{
    class Aoc01 : AocBase
    {
        public Aoc01() : base(@"data/input01.txt")
        {
            
        }
        public override string executeA(string[] inputs)
        {
            int result = 0;
            foreach(var input in inputs)
            {
                    if(input[0] == '+')
                    {
                        result += int.Parse(input.Substring(1));
                    }
                    else
                    {
                        result -= int.Parse(input.Substring(1));
                    }
            }
            return result.ToString();
        }
        public override string executeB(string[] inputs)
        {
            int result = 0;
            List<int> freqList = new List<int>();
            bool foundFreq = false;
            while(!foundFreq)
            {
                foreach(var input in inputs)
                {
                    if(input[0] == '+')
                    {
                        result += int.Parse(input.Substring(1));
                    }
                    else
                    {
                        result -= int.Parse(input.Substring(1));
                    }
                    if(freqList.Contains(result))
                    {
                        foundFreq = true;
                        break;
                    }
                    freqList.Add(result);
                }
            }
            return result.ToString();
        }
    }
}