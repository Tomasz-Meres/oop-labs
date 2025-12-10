using Simulator.Maps;

namespace Simulator;

public abstract class Creature
{
    private Map? _map;
    private Point _point;
    public Point Position => _point;
    public Map? CurrentMap => _map;

    private string _name = "Unknown";
    public string Name
    {
        get { return _name; }
        init
        {
            _name = Validator.Shortener(value, 3, 25, '#');
        }
    }
    private int _level;
    public int Level
    {
        get { return _level; }
        init
        {
            _level = Validator.Limiter(value, 1, 10);
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

    public Creature()
    {
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }
    public abstract string Greeting();

    public abstract string Info { get; }

    public void Upgrade()
    {
        if (_level < 10)
            _level += 1;
    }

    public void Go(Direction direction)
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

    public abstract int Power { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}

