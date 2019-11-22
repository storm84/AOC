
using System;
using System.Collections.Generic;

namespace AOC18{

    class Aoc03 : AocBase
    {
        public Aoc03() : base(@"data/input3.txt")
        {
        }

        public override string executeA(string[] inputs)
        {
            List<ClaimData> claimList = new List<ClaimData>();
            int xMax = 0, yMax = 0;
            foreach(var input in inputs)
            {
                //Parse input data
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
                
                //calc cube xMax and yMax
                if(claim.x+claim.length > xMax)
                    xMax = claim.x+claim.length;
                if(claim.y+claim.height > yMax)
                    yMax = claim.y+claim.height;  
                claimList.Add(claim);    
            }

            //build cube
            bool [,,] cube = new bool[xMax,yMax,claimList.Count];
            for(int z=0; z < claimList.Count; z++)
            {
                for(int y=0; y < claimList[z].height; y++)
                {
                    for(int x=0; x < claimList[z].length; x++)
                    {
                        cube[claimList[z].x+x, claimList[z].y+y, z] = true;
                    }
                }
            }
            
            // count overlap
            int overlap = 0;
            bool match;
            for (int z = 0; z < cube.GetLength(2)-1; z++)
            {
                match = false;
                for(int y=0; y < cube.GetLength(1); y++)
                {
                    for(int x=0; x < cube.GetLength(0); x++)
                    {
                        if(cube[x,y,z] == true && cube[x,y,z+1] == true)
                        {
                            match = true;
                            break;
                        }
                    }
                    if(match) break;
                }
                if(match) overlap++;
            }
            return overlap.ToString();
        }
        public override string executeB(string[] inputs)
        {
            return "not implemented";
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