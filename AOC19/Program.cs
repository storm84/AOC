using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AOC19
{
 class Program
    {
        static void Main(string[] args)
        {
            int day = 0;
            if(args.Length > 0)
            {
                int.TryParse(args[0],out day);
            }
            
            var implementation = CreateInstanceByDay(day.ToString("00"));
            if(implementation != null)
            {
                implementation.Run();
            }
            else 
            {
                Console.WriteLine("Invalid parameter or day is not implemented\nRun the application with day as input parameter (1-25)");
            }
        }

        private static AocBase CreateInstanceByDay(string day)
        {
            string className = $"Aoc{day}";
            string pathParam = $"data/{day}.aoc";

            var type = GetDerivedTypesFor(typeof(AocBase))
                .FirstOrDefault(t => t.Name == className);

            return type != null ?  (AocBase) Activator.CreateInstance(type, pathParam): null;
        }

        private static IEnumerable<Type> GetDerivedTypesFor(Type baseType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes()
                .Where(baseType.IsAssignableFrom)
                .Where(t => baseType != t);
        }
    }
}
