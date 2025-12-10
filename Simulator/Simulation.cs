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
    public List<Creature> Creatures { get; }

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
    private int _currentCreatureIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public Creature CurrentCreature 
    { 
        get { return Creatures[_currentCreatureIndex]; }
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
    public Simulation(Map map, List<Creature> creatures,
        List<Point> positions, string moves)
    {
        if (creatures.Count == 0)
            throw new ArgumentException("Creatures list is empty", nameof(creatures));
        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures differs from number of starting positions");

        Map = map;
        Creatures = creatures;
        Positions = positions;
        Moves = moves ?? "";
        _parsedMoves = DirectionParser.Parse(moves);
        for (int i = 0; i < Creatures.Count; i++)
        {
            Creatures[i].InitMapAndPosition(Map, Positions[i]);
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
        CurrentCreature.Go(dir);

        _currentCreatureIndex = (_currentCreatureIndex + 1) % Creatures.Count;
        _currentTurnIndex = (_currentTurnIndex + 1) % _parsedMoves.Count;
        _movesDone++;
        if (_movesDone >= _parsedMoves.Count)
            Finished = true;

    }
}