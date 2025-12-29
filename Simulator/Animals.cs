using Simulator.Maps;

namespace Simulator;

public class Animals : IMappable
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

    protected Map? _map;
    protected Point _point;
    public Point Position => _point;
    public Map? Map => _map;

    public virtual char MapSymbol => 'A';

    public virtual void Go(Direction direction)
    {
        if (_map == null) return;

        Point NextPoint = _map.Next(_point, direction);

        try
        {
            _map.Move(this, NextPoint);
            _point = NextPoint;
        }
        catch
        {

        }
    }

    public void InitMapAndPosition(Map map, Point StartingPosition)
    {
        if (map == null) return;
        if (!map.Exist(StartingPosition))
            throw new ArgumentOutOfRangeException(nameof(StartingPosition), "Point out of map");

        map.Add(this, StartingPosition);

        _map = map;
        _point = StartingPosition;
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
