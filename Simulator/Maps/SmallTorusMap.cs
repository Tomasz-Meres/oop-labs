namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public readonly int Size;

    private readonly Rectangle area;

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be between 5 and 20.");

        Size = size;

        area = new Rectangle(0, 0, Size - 1, Size - 1);
    }

    public override bool Exist(Point p)
    {
        return area.Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);

        int x = (next.X + Size) % Size;
        int y = (next.Y + Size) % Size;

        return new Point(x, y);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point next = p.NextDiagonal(d);

        int x = (next.X + Size) % Size;
        int y = (next.Y + Size) % Size;

        return new Point(x, y);
    }
}
