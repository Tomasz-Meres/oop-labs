namespace Simulator;

public class SimulationLog
{
    private Simulation _simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<TurnLog> TurnLogs { get; } = [];
    // store starting positions at index 0

    public SimulationLog(Simulation simulation)
    {
        _simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = _simulation.Map.SizeX;
        SizeY = _simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        // TURA 0 – stan początkowy
        TurnLogs.Add(new TurnLog
        {
            Mappable = "START",
            Move = "-",
            Symbols = _simulation.Map.GetSymbols()
        });

        // kolejne tury
        while (!_simulation.Finished)
        {
            string mappable = _simulation.CurrentBeing.ToString();
            string move = _simulation.CurrentMoveName;

            _simulation.Turn();

            TurnLogs.Add(new TurnLog
            {
                Mappable = mappable,
                Move = move,
                Symbols = _simulation.Map.GetSymbols()
            });
        }
    }
}