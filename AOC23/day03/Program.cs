var input = File.ReadAllLines("input.txt");

int SolveA(string[] input)
{
    var parts = GetParts(input);

    return parts.Select(p => p.PartNumber).Sum();
}

int SolveB(string[] input)
{
    var parts = GetParts(input);

    var gears = parts.Where(p => p.Symbols.Any(s => s.Character == '*'));

    return gears
        .Select(
            p =>
                new { PartNumber = p.PartNumber, Symbol = p.Symbols.First(s => s.Character == '*') }
        )
        .GroupBy(
            p => $"{p.Symbol.PosX},{p.Symbol.PosY}",
            p => p.PartNumber,
            (key, partNumbers) =>
                new
                {
                    GerRatio = partNumbers.Aggregate((int a, int b) => a * b),
                    GearCount = partNumbers.Count()
                }
        )
        .Where(g => g.GearCount > 1)
        .Sum(x => x.GerRatio);
}

IEnumerable<Part> GetParts(string[] partMap)
{
    var parts = new List<Part>();
    for (var i = 0; i < partMap.Length; i++)
    {
        var numStart = -1;
        var numLength = 0;
        for (var j = 0; j < partMap[i].Length; j++)
        {
            if (char.IsDigit(partMap[i][j]))
            {
                if (numStart == -1)
                {
                    numStart = j;
                }
                numLength++;
            }
            if (!char.IsDigit(partMap[i][j]) || j == partMap[i].Length - 1)
            {
                if (numStart != -1)
                {
                    // number found, check if part
                    var symbols = GetSymbols(partMap, numStart, i, numLength);
                    if (symbols.Count() > 0)
                    {
                        // part found, add part to list
                        parts.Add(
                            new Part(int.Parse(partMap[i].Substring(numStart, numLength)), symbols)
                        );
                    }
                }
                numStart = -1;
                numLength = 0;
            }
        }
    }
    return parts;
}

IEnumerable<Symbol> GetSymbols(string[] partMap, int x, int y, int numLength)
{
    var symbols = new List<Symbol>();

    var scanX = x;
    var scanLength = numLength;

    if (x > 0)
    {
        scanX--;
        scanLength++;
        if (IsSymbol(partMap[y][scanX]))
        {
            symbols.Add(new Symbol(partMap[y][scanX], scanX, y));
        }
    }
    if (x + numLength < partMap[y].Length)
    {
        scanLength++;
        var x_calculated = scanX + scanLength - 1;
        if (IsSymbol(partMap[y][x_calculated]))
        {
            symbols.Add(new Symbol(partMap[y][x_calculated], x_calculated, y));
        }
    }
    if (y > 0)
    {
        for (var ix = scanX; ix < scanX + scanLength; ix++)
        {
            if (IsSymbol(partMap[y - 1][ix]))
            {
                symbols.Add(new Symbol(partMap[y - 1][ix], ix, y - 1));
            }
        }
    }
    if (y < partMap.Length - 1)
    {
        for (var ix = scanX; ix < scanX + scanLength; ix++)
        {
            if (IsSymbol(partMap[y + 1][ix]))
            {
                symbols.Add(new Symbol(partMap[y + 1][ix], ix, y + 1));
            }
        }
    }

    return symbols;
}

bool IsSymbol(char c)
{
    return c != '.' && !char.IsDigit(c);
}

Console.WriteLine($"The answer to A is {SolveA(input)}");
Console.WriteLine($"The answer to B is {SolveB(input)}");

class Part(int partNumber, IEnumerable<Symbol> symbols)
{
    public int PartNumber { get; } = partNumber;
    public IEnumerable<Symbol> Symbols { get; } = symbols;
}

class Symbol(char character, int posX, int posY)
{
    public char Character { get; } = character;
    public int PosX { get; } = posX;
    public int PosY { get; } = posY;
}
