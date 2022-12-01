using System.Collections;
var inputStrings = File.ReadAllLines("input.txt");

uint SolveA(string[] input)
{
    var bitArr = new BitArray(input.First().Length);
    for(int i = 0; i < input.First().Length; i++)
    {
        var cnt = 0;
        for(int j = 0; j < input.Length; j++)
        {
            cnt += input[j][i] == '1' ? 1 : 0;
        }
        bitArr.Set(input.First().Length - 1 - i, cnt > input.Length /2);  
    }
    byte[] data = new byte[4];
    bitArr.CopyTo(data,0);
    var gamma = BitConverter.ToUInt32(data);
    bitArr.Not().CopyTo(data, 0);
    var epsilon = BitConverter.ToUInt32(data);

    return gamma * epsilon;
}

uint SolveB(string[] input){
    
    var oxySearch = input.ToList();
    for(int i = 0; i < oxySearch.First().Length; i++)
    {
        if(oxySearch.Count(x=> x[i] == '1' ) >= oxySearch.Count() / 2.0)
            oxySearch.RemoveAll(x=> x[i] != '1');
        else
            oxySearch.RemoveAll(x=> x[i] == '1');
        
        if(oxySearch.Count() == 1){
            break;
        }
    }

    var co2Search = input.ToList();
    for(int i = 0; i < co2Search.First().Length; i++)
    {
        if(co2Search.Count(x=> x[i] == '1' ) >= co2Search.Count() / 2.0)
            co2Search.RemoveAll(x=> x[i] == '1');
        else
            co2Search.RemoveAll(x=> x[i] != '1');
        
        if(co2Search.Count() == 1){
            break;
        }
    }

    return Convert.ToUInt32(oxySearch.First(), 2) * Convert.ToUInt32(co2Search.First(), 2);
}

Console.WriteLine($"Answer to A is: {SolveA(inputStrings)}");
Console.WriteLine($"Answer to B is: {SolveB(inputStrings)}");
