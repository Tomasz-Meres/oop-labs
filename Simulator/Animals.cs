namespace Simulator;

public class Animals
{
    private string description = "Unknown";
    public required string Description { 
        get { return description; }
        init {
            description = Validator.Shortener(value, 3, 15, '#');
        }
    }
    public uint Size { get; set; } = 3;

    public virtual string Info
    {
        get { return $"{Description} <{Size}>"; }
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
