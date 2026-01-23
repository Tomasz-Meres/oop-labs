using Simulator.Maps;

namespace Simulator;

public class Animals : IMappable, IDeletable
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

    public bool IsDeleted => Size == 0;

    public virtual Point GetDestination(Direction d) => Map?.Next(Position, d) ?? Position;

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


    /// <summary>
    /// Reduces the size of the animal by a given percentage.
    /// </summary>
    /// <param name="percent"></param>
    public virtual void DecreaseSize(double percent)
    {
        if (Size == 1)
        {
            Size = 0;
        }
        else
        {
            Size = (uint)Math.Max(0, Math.Floor(Size * (1 - percent)));
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
