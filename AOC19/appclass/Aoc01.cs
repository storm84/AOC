namespace AOC19
{
    class Aoc01 : AocBase
    {
        public Aoc01(string filepath) : base(filepath)
        {
        }

        public override string ExecuteA(string[] inputs)
        {
            int sum = 0;
            foreach(var input in inputs)
            {
                sum += (int.Parse(input) / 3) - 2;
            }
            return sum.ToString();
        }

        public override string ExecuteB(string[] inputs)
        {
            int sum = 0;
            foreach(var input in inputs)
            {
                sum += CalculateFuel(int.Parse(input));
            }
            return sum.ToString(); 
        }
        private int CalculateFuel(int mass)
        {
            int fuel = ((mass / 3) - 2 );
            if(fuel < 0) 
                return 0;
            else
            {
                return fuel + CalculateFuel(fuel);
            }
        }
    }
}