using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Fact]
    public void Constructor_ShouldSetSize()
    {
        var map = new SmallSquareMap(8);
        Assert.Equal(8, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void Constructor_InvalidSize_ShouldThrow(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(0, 0, 5, true)]
    [InlineData(4, 4, 5, true)]
    [InlineData(5, 5, 5, false)]
    [InlineData(-1, 3, 5, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y, int size, bool expected)
    {
        var map = new SmallSquareMap(size);
        var p = new Point(x, y);

        Assert.Equal(expected, map.Exist(p));
    }

    [Theory]
    [InlineData(5, 5, Direction.Up, 5, 6)]
    [InlineData(5, 5, Direction.Down, 5, 4)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    [InlineData(5, 5, Direction.Right, 6, 5)]
    [InlineData(9, 5, Direction.Right, 9, 5)]
    [InlineData(0, 0, Direction.Down, 0, 0)]
    [InlineData(15, 15, Direction.Left, 15, 15)]
    [InlineData(-5, -5, Direction.Up, -5, -5)]
    public void Next_ShouldShiftCorrectly(int x, int y, Direction dir, int ex, int ey)
    {
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        Assert.Equal(new Point(ex, ey), map.Next(p, dir));
    }

    [Theory]
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(5, 5, Direction.Down, 4, 4)]
    [InlineData(5, 5, Direction.Left, 4, 6)]
    [InlineData(5, 5, Direction.Right, 6, 4)]
    [InlineData(9, 5, Direction.Right, 9, 5)]
    [InlineData(0, 0, Direction.Down, 0, 0)]
    [InlineData(15, 15, Direction.Left, 15, 15)]
    [InlineData(-5, -5, Direction.Up, -5, -5)]
    public void NextDiagonal_ShouldShiftCorrectly(int x, int y, Direction dir, int ex, int ey)
    {
        var map = new SmallSquareMap(10);
        var p = new Point(x, y);

        Assert.Equal(new Point(ex, ey), map.NextDiagonal(p, dir));
    }
}