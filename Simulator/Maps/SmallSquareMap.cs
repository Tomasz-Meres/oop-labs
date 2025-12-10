namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public readonly int Size;

    public SmallSquareMap(int size) : base(size, size)
    {
        if (size > 20)
            throw new ArgumentOutOfRangeException(nameof(size), "The maximum size can be 20");

        Size = size;
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);

        return Exist(next) ? next : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point next = p.NextDiagonal(d);

        return Exist(next) ? next : p;
    }
}
