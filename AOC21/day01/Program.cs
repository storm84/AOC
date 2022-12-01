var inputStrings = File.ReadAllLines("input.txt");
var inputs = Array.ConvertAll(inputStrings, s => int.Parse(s));

int RunA(int[] inputs){
    var cnt = 0;
    for(int i = 0; i < inputs.Length-1; i++){
        if(inputs[i] < inputs[i+1]) 
            cnt++;
    }
    return cnt;
}
int RunB(int[] inputs){
    var cnt = 0;
    for(int i=0; i<inputs.Length-3; i++){
        if(inputs[i] < inputs[i+3]) 
            cnt++;
    }
    return cnt;
}

Console.WriteLine($"Answer to A: {RunA(inputs)}");
Console.WriteLine($"Answer to B: {RunB(inputs)}");
