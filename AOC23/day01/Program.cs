using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

int SolveA(string[] input)
{
    var result = 0;
    for (var i = 0; i < input.Length; i++)
    {
        result += GetCalibration(input[i]);
    }
    return result;
}

int GetCalibration(string str)
{
    char? first = null,
        last = null;
    for (var i = 0; i < str.Length; i++)
    {
        if (first is null && Char.IsDigit(str, i))
            first = str[i];
        if (last is null && Char.IsDigit(str, str.Length - 1 - i))
            last = str[str.Length - 1 - i];
        if (first is not null && last is not null)
            break;
    }
    return int.Parse($"{first}{last}");
}

int SolveB(string[] input)
{
    var result = 0;
    for (var i = 0; i < input.Length; i++)
    {
        result += GetCalibrationB(input[i]);
    }
    return result;
}

int GetCalibrationB(string str)
{
    string[] arr =
    [
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    ];

    var result = arr.SelectMany(
            sw => Regex.Matches(str, sw).Select(m => new { SearchWord = sw, Index = m.Index })
        )
        .OrderBy(x => x.Index);

    var first = Array.IndexOf(arr, result.First().SearchWord) % 10;
    var last = Array.IndexOf(arr, result.Last().SearchWord) % 10;
    return first * 10 + last;
}

Console.WriteLine($"The answer to A is {SolveA(input)}");
Console.WriteLine($"The answer to B is {SolveB(input)}");
