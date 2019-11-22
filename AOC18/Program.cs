using System;
using System.IO;
namespace AOC18
{
    class Program
    {
        static void Main(string[] args)
        {
            int.TryParse(args[0],out int day);
            switch(day)
            {
                case 1: new Aoc01().run(); break;
                case 2: new Aoc02().run(); break;
                case 3: new Aoc03().run(); break;
                default: Console.WriteLine("Invalid parameter or not day implemented"); break;
            }
        }
    }
}
