
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC18{

    class Aoc03 : AocBase
    {
        public Aoc03() : base(@"data/input03.txt")
        {
        }

        // Skip cube and go for list with xyz values and look for matches
        public override string executeA(string[] inputs)
        {
            Dictionary<string,int> overlapCnt = new Dictionary<string, int>();
            foreach(var input in inputs)
            {
                //Parse input data
                ClaimData claim = parseClaim(input);
                
                //check overlap
                for(int y=0; y < claim.height; y++)
                {
                    for(int x=0; x < claim.length; x++)
                    {
                        var key = $"X{claim.x+x}Y{claim.y+y}"; 
                        if(overlapCnt.ContainsKey(key))
                        {
                            overlapCnt[key]++;
                        }
                        else
                        {
                            overlapCnt.Add(key, 1);
                        }
                    }
                }
            }
            return overlapCnt.Values.Where(x=> x > 1).Count().ToString();
        }
        public override string executeB(string[] inputs)
        {
            Dictionary<string,int> overlapCnt = new Dictionary<string, int>();
            List<ClaimData> claims = new List<ClaimData>();
            foreach(var input in inputs)
            {
                //Parse input data
                ClaimData claim = parseClaim(input);
                claims.Add(claim);
                //check overlap
                for(int y=0; y < claim.height; y++)
                {
                    for(int x=0; x < claim.length; x++)
                    {
                        var key = $"X{claim.x+x}Y{claim.y+y}"; 
                        if(overlapCnt.ContainsKey(key))
                        {
                            overlapCnt[key]++;
                        }
                        else
                        {
                            overlapCnt.Add(key, 1);
                        }
                    }
                }
            }

            // search for the single claim that doesn't overlap
            bool found;
            foreach(var claim in claims)
            {
                found = true;
                for(int y=0; y < claim.height; y++)
                {
                    for(int x=0; x < claim.length; x++)
                    {
                        var key = $"X{claim.x+x}Y{claim.y+y}"; 
                        if(overlapCnt[key] != 1)
                        {
                            found = false;
                            break;
                        }
                    }
                    if(!found) break;
                }
                if(found) return claim.z.ToString();
            }
            return "claim not found";
        }

        private ClaimData parseClaim(string input)
        {
            ClaimData claim = new ClaimData();
            var tmp = input.Split('@');
            claim.z = int.Parse(tmp[0].Trim().Remove(0,1));
            tmp = tmp[1].Split(':');
            var cord = tmp[0].Split(',');
            var sides = tmp[1].Split('x');
            claim.x = int.Parse(cord[0].Trim());
            claim.y = int.Parse(cord[1].Trim());
            claim.length = int.Parse(sides[0].Trim());
            claim.height = int.Parse(sides[1].Trim());
            return claim;
        }
        struct ClaimData
        {
            public int z;
            public int x;
            public int y;
            public int length;
            public int height;

        }

         
    }
}