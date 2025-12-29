using Simulator;
using Simulator.Maps;
using System.Text;

namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== SIMULATION MENU ===");
        Console.WriteLine("1. Sim1 - Small square map");
        Console.WriteLine("2. Sim2 - Small torus map");
        Console.Write("Choose an option: ");
        string? input = Console.ReadLine();

        switch (input)
        {
            case "1":
                Sim1();
                break;
            case "2":
                Sim2();
                break;
            default:
                Console.WriteLine("Invalid option. Press any key to exit...");
                Console.ReadKey();
                return;
        }
    }

    static void Sim1()
    {
        Console.OutputEncoding = Encoding.UTF8;
        SmallSquareMap map = new(5);
        List<IMappable> beings = [new Orc("Gorbag"), new Elf("Elandor")];
        List<Point> points = [new(2, 2), new(3, 1)];
        string moves = "dlrludl";

        Simulation simulation = new(map, beings, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        Console.WriteLine("SIMULATION!\n");
        Console.WriteLine("Starting positions:");
        mapVisualizer.Draw();

        int turn = 1;
        while (!simulation.Finished)
        {
            IMappable current = simulation.CurrentBeing;
            string moveName = simulation.CurrentMoveName;
            Point previousPoint = current.Position;
            simulation.Turn();

            Console.WriteLine($"TURN {turn}");
            Console.WriteLine($"{current} {previousPoint} goes {moveName}:");
            mapVisualizer.Draw();

            turn++;
        }
    }

    static void Sim2() 
    {
        Console.OutputEncoding = Encoding.UTF8;
        SmallTorusMap map = new(8, 6);
        List<IMappable> beings =
        [
            new Orc("Gorbag"),
            new Elf("Elandor"),
            new Animals() { Description = "Rabbits", Size = 10 },
            new Birds() { Description = "Eagles", Size = 2, CanFly = true },
            new Birds() { Description = "Ostriches", Size = 4, CanFly = false }
        ];
        List<Point> points = [new(2, 2), new(3, 1), new(6, 2), new(5, 5), new(7, 4)];
        string moves = "lrurudlrddrurulduluu";

        Simulation simulation = new(map, beings, points, moves);
        MapVisualizer mapVisualizer = new(simulation.Map);

        Console.WriteLine("SIMULATION!\n");
        Console.WriteLine("Starting positions:");
        mapVisualizer.Draw();

        int turn = 1;
        while (!simulation.Finished)
        {
            IMappable current = simulation.CurrentBeing;
            string moveName = simulation.CurrentMoveName;
            Point previousPoint = current.Position;
            simulation.Turn();

            Console.WriteLine($"TURN {turn}");
            Console.WriteLine($"{current} {previousPoint} goes {moveName}:");
            mapVisualizer.Draw();

            turn++;
        }
    }
}
