namespace Simulator.Maps.StaticObjects;

public abstract class StaticObject : IMappable
{
    private Map? _map;
    private Point _point;
    public Point Position => _point;
    public Map? Map => _map;

    public virtual char MapSymbol => '?';
    public Point GetDestination(Direction d) => Position;

    public void Go(Direction direction){}

    public void InitMapAndPosition(Map map, Point StartingPosition)
    {
        if (map == null) return;
        if (!map.Exist(StartingPosition))
            throw new ArgumentOutOfRangeException(nameof(StartingPosition), "Point out of map");

        map.Add(this, StartingPosition);

        _map = map;
        _point = StartingPosition;
    }

    public virtual void OnEntry(IMappable visitor) { }

    public override string ToString() => $"{GetType().Name} at {Position}";
}
