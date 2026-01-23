namespace Simulator;

public class SimulationLog
{
    public Simulation simulation { get; }
    public int SizeX { get; }
    public int SizeY { get; }
    public List<TurnLog> TurnLogs { get; } = [];

    public SimulationLog(Simulation simulation)
    {
        this.simulation = simulation ??
            throw new ArgumentNullException(nameof(simulation));
        SizeX = simulation.Map.SizeX;
        SizeY = simulation.Map.SizeY;
        Run();
    }

    private void Run()
    {
        List<string> GetCurrentHealth() => simulation.Beings
                .Select(b =>
                {
                    if (b is Creature c)
                    {
                        return $"{c.Name}: {c.CurrentHealth}/{c.MaxHealth} HP";
                    }
                    if (b is Animals a)
                    {
                        return $"{a.Description}: Size {a.Size}";
                    }
                    return b.ToString();
                })
                .ToList();

        // TURA 0 – stan początkowy
        TurnLogs.Add(new TurnLog
        {
            Mappable = "START",
            Move = "-",
            Symbols = simulation.Map.GetSymbols(),
            HealthStatus = GetCurrentHealth()
        });

        // kolejne tury
        while (!simulation.Finished)
        {
            string mappable = simulation.CurrentBeing.ToString();
            string move = simulation.CurrentMoveName;

            simulation.Turn();

            TurnLogs.Add(new TurnLog
            {
                Mappable = mappable,
                Move = move,
                Symbols = simulation.Map.GetSymbols(),
                HealthStatus = GetCurrentHealth(),
                Actions = new List<string>(simulation.ActionMessages)
            });
        }
    }
}