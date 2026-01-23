namespace Simulator;

/// <summary>
/// State of map after single simulation turn.
/// </summary>
public class TurnLog
{
    /// <summary>
    /// Text representation of moving object in this turn.
    /// CurrentMappable.ToString()
    /// </summary>
    public required string Mappable { get; init; }
    /// <summary>
    /// Text representation of move in this turn.
    /// CurrentMoveName.ToString();
    /// </summary>
    public required string Move { get; init; }
    /// <summary>
    /// Dictionary of IMappable.Symbol on the map in this turn.
    /// </summary>
    public required Dictionary<Point, char> Symbols { get; init; }

    /// <summary>
    /// List of health statuses of all Creatures after this turn.
    /// </summary>
    public List<string> HealthStatus { get; init; } = new();

    /// <summary>
    /// Additional actions taken during this turn.
    /// </summary>
    public List<string> Actions { get; init; } = new();
}
