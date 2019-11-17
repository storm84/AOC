using System;
using System.IO;
using System.Collections.Generic;

namespace AOC18
{
    abstract class AocBase
    {
        public AocBase(string filepath)
        {
            _filepath = filepath;
        }
        private string _filepath;
        public void run()
        {
            Console.WriteLine("********** start execution ********** ");
            var inputs = File.ReadAllLines(_filepath);
            Console.WriteLine($"result A: {executeA(inputs)}");
            Console.WriteLine($"result B: {executeB(inputs)}");
            Console.WriteLine("********** execution done  ********** ");
        }
        public void runTest(string[] inputs)
        {
            Console.WriteLine("********** start test ********** ");
            Console.WriteLine($"result A: {executeA(inputs)}");
            Console.WriteLine($"result B: {executeB(inputs)}");
            Console.WriteLine("********** test done  ********** ");
        }

        public abstract string executeA(string[] inputs);
        public abstract string executeB(string[] inputs);

    }
}