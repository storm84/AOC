var inputStrings = File.ReadAllLines("input.txt");

int RunA(string[] inputs){
    int depth = 0, horizontalPos = 0;
    var splittedInputs = inputs
        .Select(x=> x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
        .ToList();
    foreach(var split in splittedInputs){
        switch (split[0]) {
            case "forward": 
                horizontalPos += int.Parse(split[1]); 
                break; 
            case "up":      
                depth -= int.Parse(split[1]); 
                break;
            case "down":    
                depth += int.Parse(split[1]); 
                break;
        }
    }
    return horizontalPos * depth;
}

int RunB(string[] inputs){
    int depth = 0, horizontalPos = 0, aim = 0;
    var splittedInputs = inputs
        .Select(x=> x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
        .ToList();
    foreach(var split in splittedInputs){
        switch (split[0]) {
            case "forward": 
                horizontalPos += int.Parse(split[1]); 
                depth += (aim * int.Parse(split[1]));
                break; 
            case "up":      
                aim -= int.Parse(split[1]); 
                break;
            case "down":    
                aim += int.Parse(split[1]); 
                break;
        }
    }
    return horizontalPos * depth;
}

Console.WriteLine($"Answer to A: {RunA(inputStrings)}");
Console.WriteLine($"Answer to B: {RunB(inputStrings)}");
