

var inputStrings = File.ReadAllLines("input.txt");


IEnumerable<int> GetBingoNumbers(string[] inputs) => inputs.First().Split(',').Select(x => int.Parse(x));

List<List<List<BoardNumber>>> GetBoards(string[] inputs) => inputs
    .Skip(1)
    .Where(x => x != "")
    .Select((x, i)=> new 
    { 
        Row = x
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x=> new BoardNumber(int.Parse(x))
                ).ToList(),
        Index = i
    })
    .GroupBy(x => x.Index / 5)
    .Select( g => g.Select(r => r.Row).ToList())
    .ToList();


bool CheckVerticalRows(List<List<BoardNumber>> board){
    
    for(int i = 0; i < board.First().Count(); i++){
        var isVertivalMarked = true;
        for(int j = 0;  j < board.Count(); j++){
            if(!board[j][i].IsMarked)
                isVertivalMarked = false;
        }
        if(isVertivalMarked)
            return true;
   }
   return false;
}

int SolveA (string[] inputs)
{
    var numbers = GetBingoNumbers(inputs);
    var boards = GetBoards(inputs);

    foreach( var num in numbers){
        foreach(var board in boards){
            foreach(var row in board){
                row.SingleOrDefault(x => x.Value == num)?.Mark();
                if(row.All(x=> x.IsMarked) || CheckVerticalRows(board))
                {
                    return num * board.Sum(row => row.Where(x=> !x.IsMarked).Select(x=> x.Value).Sum());
                }
            }
        }
    }

    throw new Exception("No bingo this time!");
}

int SolveB (string[] inputs)
{
    var numbers = GetBingoNumbers(inputs);
    var boards = GetBoards(inputs);

    int result = 0;
    var bordsWon = new List<int>();
    foreach( var num in numbers){
        foreach(var board in boards){
            if(!bordsWon.Contains(boards.IndexOf(board)))
                foreach(var row in board){
                    row.SingleOrDefault(x => x.Value == num && !x.IsMarked)?.Mark();
                    if(row.All(x=> x.IsMarked) || CheckVerticalRows(board))
                    {
                        bordsWon.Add(boards.IndexOf(board));
                        result = num * board.Sum(row => row.Where(x=> !x.IsMarked).Select(x=> x.Value).Sum());
                    }
                }
        }
    }
    
    return result;
}

Console.WriteLine($"The answer to A is: {SolveA(inputStrings)}");
Console.WriteLine($"The answer to B is: {SolveB(inputStrings)}");



