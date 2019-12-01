using System;
using System.IO;
using System.Collections.Generic;

namespace AOC19
{
    abstract class AocBase
    {
        public AocBase(string filepath)
        {
            _filepath = filepath;
        }
        private string _filepath;
        public void Run()
        {
            Console.WriteLine("********** start execution ********** ");
            if(File.Exists(_filepath))
            {
                var inputs = File.ReadAllLines(_filepath);
                Console.WriteLine($"result A: {ExecuteA(inputs)}");
                Console.WriteLine($"result B: {ExecuteB(inputs)}");
            }
            else 
            {
                Console.WriteLine("ERROR: Inputfile not not found!");
            }
            Console.WriteLine("********** execution done  ********** ");
        }
        
        public void runTest(string[] inputs)
        {
            Console.WriteLine("********** start test ********** ");
            Console.WriteLine($"result A: {ExecuteA(inputs)}");
            Console.WriteLine($"result B: {ExecuteB(inputs)}");
            Console.WriteLine("********** test done  ********** ");
        }

        public abstract string ExecuteA(string[] inputs);
        public abstract string ExecuteB(string[] inputs);

    }
}