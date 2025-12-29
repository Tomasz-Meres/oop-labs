using Simulator.Maps;

namespace Simulator;

public class Birds : Animals
{
    private bool canFly = true;
    public bool CanFly
    {
        get { return canFly; }
        set { canFly = value; }
    }

    public override char MapSymbol => CanFly ? 'B' : 'b';

    public override void Go(Direction direction)
    {
        if (_map == null) return;

        Point next;

        if (CanFly)
        {
            next = _map.Next(_point, direction);
            next = _map.Next(next, direction);
        }
        else
        {
            next = _map.NextDiagonal(_point, direction);
        }

        try
        {
            _map.Move(this, next);
            _point = next;
        }
        catch
        {
        }
    }


    public override string Info
    {
        get { return $"{Description} (fly{(CanFly ? "+" : "-")}) <{Size}>"; }
    }
}
