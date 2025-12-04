using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class PointTests
{
    [Fact]
    public void Constructor_ShouldSetCoordinates()
    {
        var p = new Point(3, 7);
        Assert.Equal(3, p.X);
        Assert.Equal(7, p.Y);
    }

    [Fact]
    public void Equals_SameCoordinates_ShouldBeEqual()
    {
        var p1 = new Point(5, -2);
        var p2 = new Point(5, -2);

        Assert.Equal(p1, p2);
    }

    [Fact]
    public void Equals_DifferentCoordinates_ShouldNotBeEqual()
    {
        var p1 = new Point(5, -2);
        var p2 = new Point(5, 1);

        Assert.NotEqual(p1, p2);
    }

    [Fact]
    public void ToString_ShouldReturnFormattedValue()
    {
        var p = new Point(3, 4);
        Assert.Equal("(3, 4)", p.ToString());
    }
}
