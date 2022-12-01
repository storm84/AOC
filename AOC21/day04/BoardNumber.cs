public class BoardNumber
{
    public int Value { get; }
    private bool _marked;

    public bool Mark () => _marked = true;
    public bool IsMarked => _marked;

    public BoardNumber(int value )
    {
        Value = value;
    }
}
