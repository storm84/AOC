using System;
using System.IO;
using System.Collections.Generic;

namespace AOC18
{
    class Aoc02 : AocBase
    {
        public Aoc02() : base(@"data/input2.txt")
        {
        }
        public override string executeA(string[] inputs)
        {
            int twiceCnt = 0, thriceCnt = 0;
            var resultDict = new Dictionary<int, int>();
            foreach(var input in inputs)
            {
                var charDict = new Dictionary<char, int>();
                for(int i = 0; i< input.Length; i++)
                {
                    if(!charDict.ContainsKey(input[i]))
                    {
                        charDict.Add(input[i], countOccurance(input, input[i]));
                    }
                }
                if(charDict.ContainsValue(2))
                    twiceCnt++;
                if(charDict.ContainsValue(3))
                    thriceCnt++;
            }
            return (twiceCnt * thriceCnt).ToString();
        }

        public override string executeB(string[] inputs)
        {
            int charPos = -1;
            int arrPos = -1;
            for(int i = 0; i < inputs.Length; i++)
            {
                for(int j = 0; j < inputs.Length; j++)
                {
                    int diffCnt = 0;
                    for(int k = 0; k < inputs[i].Length; k++)
                    {
                        if(inputs[i][k] != inputs[j][k])
                        {
                            diffCnt++;
                            charPos = k;
                        }
                    }
                    if(diffCnt == 1)
                    {
                        arrPos = j;
                        break;
                    }

                }
                if(arrPos != -1)
                {
                    break;
                }
            }
            return inputs[arrPos].Remove(charPos,1);
            
        }

        public int countOccurance(string inputString, char searchVal)
        {
            if(inputString.Length < 1)
                return 0;
            
            return (inputString[0] == searchVal ? 1 : 0) + countOccurance(inputString.Substring(1), searchVal);
        }
    }
}