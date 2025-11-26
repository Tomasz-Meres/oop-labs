namespace Simulator;

public class Birds : Animals
{
    private bool canFly = true;
    public bool CanFly
    {
        get { return canFly; }
        set { canFly = value; }
    }

    public override string Info
    {
        get { return $"{Description} (fly{(CanFly ? "+" : "-")}) <{Size}>"; }
    }
}
