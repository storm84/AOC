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
            int result = 0;
            foreach(var input in inputs)
            {
                Dictionary<char,int> dict = new Dictionary<char, int>();
                for(int i = 0; i< input.Length; i++)
                {
                    if(!dict.ContainsKey(input[i]))
                    {
                        dict.Add(input[i], countOccurance(input, input[i]));
                    }
                }
                /*
                    fortsätt här
                    bygg upp en lista över alla resultat i dict där värdet är störren än 1.
                    spara ner i en lista över antalet förekomster av en och samma

                */
            }
            return result.ToString();
        }

        public override string executeB(string[] inputs)
        {
            return "NotImplemented";
        }

        private int countOccurance(string inputString, char searchVal)
        {
            if(inputString.Length < 1)
                return 0;
            
            if(inputString[0] == searchVal)
                return 1 + countOccurance(inputString.Substring(1), searchVal);
            
            return 0;
        }
    }
}