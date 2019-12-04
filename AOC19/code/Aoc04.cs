using System;

namespace AOC19
{
    class Aoc04 : AocBase
    {
        public Aoc04(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            int start = int.Parse(inputs[0].Split('-')[0]);
            int stop = int.Parse(inputs[0].Split('-')[1]);
            int validPwCnt = 0;
            for(int i = start; i <= stop; i++)
            {
                string pw = i.ToString();
                if(HasAdjacentChars(pw) && NeverDecrease(pw))
                {
                    validPwCnt++;
                }
            }
            return validPwCnt.ToString();
        }
        public override string PartB(string[] inputs)
        {
            int start = int.Parse(inputs[0].Split('-')[0]);
            int stop = int.Parse(inputs[0].Split('-')[1]);
            int validPwCnt = 0;
            for(int i = start; i <= stop; i++)
            {
                string pw = i.ToString();
                if(NeverDecrease(pw) && HasGroupOfTwo(pw))
                {
                    validPwCnt++;
                }
            }
            return validPwCnt.ToString();
        }
        private bool HasAdjacentChars(string s)
        {
            for(int i = 0; i < s.Length - 1; i++)
            {
                if(s[i] == s[i+1])
                {
                    return true; 
                }
            }
            return false;
        }

        private bool HasGroupOfTwo(string s)
        {
            int maxGroupSize = 0;
            int currGroupSize = 1;
            for(int i = 0; i < s.Length - 1; i++)
            {
                if(s[i] == s[i+1])
                {
                    currGroupSize++;
                    if(currGroupSize > maxGroupSize) 
                        maxGroupSize = currGroupSize; 
                }
                else
                {
                    if(currGroupSize == 2) return true;
                    currGroupSize = 1;
                }
            }
            return currGroupSize == 2;
        }

        private bool NeverDecrease(string s)
        {
            for(int i = 0; i < s.Length - 1; i++)
            {
                if(s[i] > s[i+1])
                {
                    return false; 
                }
            }
            return true; 
        }
    }
}