using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class RectangleTests
{
    [Fact]
    public void Constructor_ShouldNormalizeCoordinates()
    {
        var r = new Rectangle(10, 8, 2, -1);

        Assert.Equal(2, r.X1);
        Assert.Equal(-1, r.Y1);
        Assert.Equal(10, r.X2);
        Assert.Equal(8, r.Y2);
    }

    [Fact]
    public void Constructor_ThinRectangle_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(1, 2, 1, 5));
        Assert.Throws<ArgumentException>(() => new Rectangle(3, 4, 7, 4));
    }

    [Theory]
    [InlineData(0, 0, 5, 5, 3, 3, true)]
    [InlineData(0, 0, 5, 5, 0, 0, true)]
    [InlineData(0, 0, 5, 5, 5, 5, true)]
    [InlineData(0, 0, 5, 5, -1, 3, false)]
    [InlineData(0, 0, 5, 5, 3, 6, false)]
    public void Contains_ShouldReturnCorrectValue(
        int x1, int y1, int x2, int y2,
        int px, int py,
        bool expected)
    {
        var r = new Rectangle(x1, y1, x2, y2);
        var p = new Point(px, py);

        Assert.Equal(expected, r.Contains(p));
    }
}
