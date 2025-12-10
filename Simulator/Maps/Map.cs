namespace Simulator.Maps;

/// <summary>
/// Map of points.
/// </summary>
public abstract class Map
{
    private Dictionary<Point, List<Creature>> _points;

    public readonly int SizeX, SizeY;
    private readonly Rectangle area;

    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
            throw new ArgumentOutOfRangeException(nameof(sizeX), "The minimum size must be 6.");
        if (sizeY < 5)
            throw new ArgumentOutOfRangeException(nameof(sizeY), "The minimum size must be 6.");
        SizeX = sizeX;
        SizeY = sizeY;
        area = new Rectangle(0, 0, SizeX, SizeY);
        _points = new Dictionary<Point, List<Creature>>();
    }
    /// <summary>
    /// Check if give point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public virtual bool Exist(Point p)
    {
        return area.Contains(p);
    }

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);
    /// <summary>
    /// Add creature to map
    /// </summary>
    /// <param name="creature">Creature to place on map</param>
    /// <param name="p">Point for creature</param>
    public void Add(Creature creature, Point p)
    {
        if (!_points.ContainsKey(p))
        {
            _points[p] = new List<Creature>();
        }
        _points[p].Add(creature);
    }
    /// <summary>
    /// Remove creature from map
    /// </summary>
    /// <param name="creature"></param>
    public void Remove(Creature creature)
    {
        var p = creature.Position;

        if (_points.TryGetValue(p, out var list))
        {
            list.Remove(creature);

            if (list.Count == 0)
                _points.Remove(p);
        }
    }

    public void Move(Creature creature, Point p)
    {
        Remove(creature);
        Add(creature, p);
    }
    /// <summary>
    /// Get list of creatures
    /// </summary>
    /// <param name="p">Point to check</param>
    /// <returns>List of creatures at given point or null if none</returns>
    public List<Creature>? At(Point p)
    {
        return _points.TryGetValue(p, out var list) ? new List<Creature>(list) : null;
    }
    /// <summary>
    /// Get list of creatures
    /// </summary>
    /// <param name="x">Point to check x coordinate</param>
    /// <param name="y">Point to check y coordinate</param>
    /// <returns>List of creatures at given point or null if none</returns>
    public List<Creature>? At(int x, int y) => At(new Point(x, y));
}