using System;
using System.IO;
namespace AOC18
{
    class Program
    {
        static void Main(string[] args)
        {
            int day = 0;
            if(args.Length > 0)
                int.TryParse(args[0],out day);
            
            switch(day)
            {
                case 1: new Aoc01().run(); break;
                case 2: new Aoc02().run(); break;
                case 3: new Aoc03().run(); break;
                case 4: new Aoc04().run(); break;
                default: Console.WriteLine("Invalid parameter or day is not implemented"); break;
            }
        }
    }
}
