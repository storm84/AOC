var input = File.ReadAllLines("input.txt");

int SolveA(string[] input)
{
    var result = 0;

    foreach (var row in input)
    {
        var s1 = row.Split(':');
        var s2 = s1[1].Split('|');
        var winningNumbers = s2[0]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Intersect(
                s2[1]
                    .Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse));

        if (winningNumbers.Count() > 0)
        {
            result += TwoPowerOf(winningNumbers.Count() - 1);
        }
    }

    return result;
}

int SolveB(string[] input)
{
    var resultArr = new int[input.Length];
    for (int i = 0; i < resultArr.Length; i++)
    {
        resultArr[i]++; // instead of init resultArr values to 1

        var s1 = input[i].Split(':');
        var s2 = s1[1].Split('|');
        var winningNumbers = s2[0]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .Intersect(
                s2[1]
                    .Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse));

        for (int j = 1; j <= winningNumbers.Count(); j++)
        {
            if (i + j < resultArr.Length)
                resultArr[i + j] += resultArr[i];
        }
    }

    return resultArr.Sum();
}

int TwoPowerOf(int n) => 1 << n;

Console.WriteLine($"The answer to A is {SolveA(input)}");
Console.WriteLine($"The answer to B is {SolveB(input)}");
