using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        SmallSquareMap map = new(5);
        List<Creature> creatures = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, creatures, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        Console.WriteLine("SIMULATION!\n");
        Console.WriteLine("Starting positions:");
        mapVisualizer.Draw();
        int turnNumber = 1;

        int turn = 1;
        while (!simulation.Finished)
        {
            Creature current = simulation.CurrentCreature;
            string moveName = simulation.CurrentMoveName;
            Point previousPoint = current.Position;
            simulation.Turn();

            Console.WriteLine($"TURN {turn}");
            Console.WriteLine($"{current.GetType().Name.ToUpper()}: {current.Info} {previousPoint} goes {moveName}:");
            mapVisualizer.Draw();

            turn++;
        }
    }
}
