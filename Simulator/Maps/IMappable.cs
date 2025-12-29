namespace Simulator.Maps;

public interface IMappable
{
    Point Position { get; }
    Map? Map { get; }
    char MapSymbol { get; }

    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point StartingPosition);

    public string ToString();
}
