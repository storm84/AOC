using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC19
{
    class Aoc06 : AocBase
    {
        public Aoc06(string filepath) : base(filepath)
        {
        }

        public override string PartA(string[] inputs)
        {
            var orbitMap = inputs
                .Select(input => input.Split(')'))
                .ToDictionary(nodes => nodes[1], nodes => nodes[0]);
            
            int orbitSum = 0;
            foreach(var orbit in orbitMap)
            {
                orbitSum += countParentOrbits(orbit.Value, orbitMap);
            }
            return orbitSum.ToString();
        }
        private int countParentOrbits(string parentOrbit, Dictionary<string,string> orbitMap)
        {
            if(orbitMap.ContainsKey(parentOrbit))
            {
                return 1 + countParentOrbits(orbitMap[parentOrbit], orbitMap);
            }
            return 1;
        }
        public override string PartB(string[] inputs)
        {
            var orbitMap = inputs
                .Select(input => input.Split(')'))
                .ToDictionary(nodes => nodes[1], nodes => nodes[0]);
            
            var currentOrbit = "YOU";
            var yourParents = new List<string>();
            //list all of your parents
            while (orbitMap.ContainsKey(orbitMap[currentOrbit]))
            {
                yourParents.Add(orbitMap[currentOrbit]);
                currentOrbit = orbitMap[currentOrbit];
            }
            // find the first shared parent (and count Santas orbital transfers to it)
            int sanCnt = 0;
            string fsParent = orbitMap["SAN"];
            while(!yourParents.Contains(fsParent))
            {
                fsParent = orbitMap[fsParent];
                sanCnt++;
            }
            // use santas count to shared parent and the index of the shared parent in yourParents list to get the result            
            int result = sanCnt + yourParents.IndexOf(fsParent);
            return result.ToString();
        }
    }
}