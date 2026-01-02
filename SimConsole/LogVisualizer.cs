using Simulator;

namespace SimConsole;

internal class LogVisualizer
{
    private readonly SimulationLog _log;

    public LogVisualizer(SimulationLog log)
    {
        _log = log ?? throw new ArgumentNullException(nameof(log));
    }

    public void Draw(int turnIndex)
    {
        if (turnIndex < 0 || turnIndex >= _log.TurnLogs.Count)
            throw new ArgumentOutOfRangeException(nameof(turnIndex));

        var turn = _log.TurnLogs[turnIndex];

        int width = _log.SizeX;
        int height = _log.SizeY;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("SIMULATION!\n");

        // Tura 0 → Starting positions
        if (turnIndex == 0)
        {
            Console.WriteLine("Starting positions:\n");
        }
        else
        {
            Console.WriteLine($"TURN {turnIndex}");
            Console.WriteLine($"{turn.Mappable} moves {turn.Move}\n");
        }

        // Górna krawędź
        Console.Write(Box.TopLeft);
        for (int x = 0; x < width - 1; x++)
            Console.Write($"{Box.Horizontal}{Box.TopMid}");
        Console.WriteLine($"{Box.Horizontal}{Box.TopRight}");

        // Wiersze mapy
        for (int y = height - 1; y >= 0; y--)
        {
            if (y < height - 1)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < width - 1; x++)
                    Console.Write($"{Box.Horizontal}{Box.Cross}");
                Console.WriteLine($"{Box.Horizontal}{Box.MidRight}");
            }

            Console.Write(Box.Vertical);
            for (int x = 0; x < width; x++)
            {
                char c = ' ';
                var p = new Point(x, y);

                if (turn.Symbols.TryGetValue(p, out char symbol))
                    c = symbol;

                Console.Write($"{c}{Box.Vertical}");
            }
            Console.WriteLine();
        }

        // Dolna krawędź
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < width - 1; x++)
            Console.Write($"{Box.Horizontal}{Box.BottomMid}");
        Console.WriteLine($"{Box.Horizontal}{Box.BottomRight}");

        Console.WriteLine(); // odstęp po mapie
    }
}
