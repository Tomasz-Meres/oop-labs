namespace Simulator.Maps;

public class SmallTorusMap : Map
{
    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 20)
            throw new ArgumentOutOfRangeException(nameof(sizeX), "The maximum size is 20.");
        if (sizeY > 20)
            throw new ArgumentOutOfRangeException(nameof(sizeY), "The maximum size is 20.");
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);

        int x = (next.X + SizeX) % SizeX;
        int y = (next.Y + SizeY) % SizeY;

        return new Point(x, y);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point next = p.NextDiagonal(d);

        int x = (next.X + SizeX) % SizeX;
        int y = (next.Y + SizeY) % SizeY;

        return new Point(x, y);
    }
}
