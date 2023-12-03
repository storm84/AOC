var input = File.ReadAllLines("input.txt");
const int MaxRed = 12;
const int MaxGreen = 13;
const int MaxBlue = 14;

int SolveA(string[] input)
{
    var result = 0;
    for (var i = 0; i < input.Length; i++)
    {
        var firstSplit = input[i].Split(":");
        var gameId = int.Parse(firstSplit[0].Split(" ")[1]);
        var hands = firstSplit[1].Split(";");

        if (hands.All(IsHandPossible))
        {
            result += gameId;
        }
    }
    return result;
}

bool IsHandPossible(string hand)
{
    foreach (var cube in hand.Split(","))
    {
        var s = cube.Trim().Split(" ");
        var color = s[1];
        var cubeCnt = int.Parse(s[0]);
        var isPossible = color switch
        {
            "red" => cubeCnt <= MaxRed,
            "green" => cubeCnt <= MaxGreen,
            "blue" => cubeCnt <= MaxBlue,
            _ => throw new InvalidOperationException("invalid cube color")
        };

        if (!isPossible)
            return false;
    }
    return true;
}

int SolveB(string[] input)
{
    var result = 0;
    for (var i = 0; i < input.Length; i++)
    {
        var gameCubeDict = new Dictionary<string, int>();
        var firstSplit = input[i].Split(":");
        var hands = firstSplit[1].Split(";");
        foreach (var hand in hands)
        {
            foreach (var cube in hand.Split(","))
            {
                var s = cube.Trim().Split(" ");
                var color = s[1];
                var cubeCnt = int.Parse(s[0]);

                if (!gameCubeDict.ContainsKey(color))
                {
                    gameCubeDict.Add(color, cubeCnt);
                }
                else if (gameCubeDict[color] < cubeCnt)
                {
                    gameCubeDict[color] = cubeCnt;
                }
            }
        }
        var product = 1;
        foreach (var val in gameCubeDict.Values)
        {
            product *= val;
        }
        result += product;
    }
    return result;
}

Console.WriteLine($"The answer to A is {SolveA(input)}");
Console.WriteLine($"The answer to B is {SolveB(input)}");
