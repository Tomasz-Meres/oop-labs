using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Beings { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of creatures moves.
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves,
    /// next move is again for first creature and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;



    /// <summary>
    /// Index of current turn in moves list.
    /// </summary>
    private int _currentTurnIndex = 0;

    /// <summary>
    /// index of current creature to move.
    /// </summary>
    private int _currentBeingIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public IMappable CurrentBeing
    { 
        get { return Beings[_currentBeingIndex]; }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName 
    {
        get { return _parsedMoves[_currentTurnIndex].ToString().ToLower(); }
    }

    /// <summary>
    /// parsed list of moves.
    /// </summary>
    private readonly List<Direction> _parsedMoves;
    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> beings,
        List<Point> positions, string moves)
    {
        if (beings.Count == 0)
            throw new ArgumentException("Creatures list is empty", nameof(beings));
        if (beings.Count != positions.Count)
            throw new ArgumentException("Number of creatures differs from number of starting positions");

        Map = map;
        Beings = beings;
        Positions = positions;
        Moves = moves ?? "";
        _parsedMoves = DirectionParser.Parse(moves);
        for (int i = 0; i < Beings.Count; i++)
        {
            Beings[i].InitMapAndPosition(Map, Positions[i]);
        }

    }

    private int _movesDone = 0;

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn() 
    {
        if (Finished)
            throw new InvalidOperationException("Simulation has finished.");

        Direction dir = _parsedMoves[_currentTurnIndex];
        CurrentBeing.Go(dir);

        _currentBeingIndex = (_currentBeingIndex + 1) % Beings.Count;
        _currentTurnIndex = (_currentTurnIndex + 1) % _parsedMoves.Count;
        _movesDone++;
        if (_movesDone >= _parsedMoves.Count)
            Finished = true;

    }
}