using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;

namespace SimWeb.Pages;

public class SimulationModel : PageModel
{
    private SimulationLog _log;
    public SimulationLog Log => _log;
    [BindProperty(SupportsGet = true)]
    public int Turn { get; set; } = 0;

    public int CurrentTurn => Turn;
    public int MaxTurn => _log.TurnLogs.Count - 1;
    public string CurrentMove => _log.TurnLogs[CurrentTurn].Move;
    public int SizeX => _log.SizeX;
    public int SizeY => _log.SizeY;

    public SimulationModel(IHttpContextAccessor httpContextAccessor)
    {
        // Przygotowanie przyk³adowej symulacji
        SmallTorusMap map = new(8, 6);
        var beings = new List<IMappable>
        {
            new Orc("Gorbag"),
            new Elf("Elandor"),
            new Animals() { Description = "Rabbits", Size = 10 },
            new Birds() { Description = "Eagles", Size = 2, CanFly = true },
            new Birds() { Description = "Ostriches", Size = 4, CanFly = false }
        };
        var points = new List<Point>
        {
            new Point(2,2),
            new Point(3,1),
            new Point(6,2),
            new Point(5,5),
            new Point(7,4)
        };
        string moves = "lrurudlrddrurulduluu";

        var simulation = new Simulation(map, beings, points, moves);
        _log = new SimulationLog(simulation);

        // Odczyt tury z sesji
        int? savedTurn = httpContextAccessor.HttpContext?.Session.GetInt32("LastTurn");
        if (savedTurn.HasValue)
        {
            Turn = Math.Clamp(savedTurn.Value, 0, _log.TurnLogs.Count - 1);
        }
        else
        {
            Turn = 0;
        }
    }

    public void OnGet()
    {
        // Zapis bie¿¹cej tury do sesji
        HttpContext.Session.SetInt32("LastTurn", Turn);
    }

    public string GetCellContent(int x, int y)
    {
        var point = new Point(x, y);
        var turn = _log.TurnLogs[CurrentTurn];

        if (turn.Symbols.TryGetValue(point, out char symbol))
        {
            string imageName = symbol switch
            {
                'O' => "orc-icon-64.png",
                'E' => "elf-icon-64.png",
                'A' => "rabbit-icon-64.png",
                'B' => "eagle-icon-64.png",
                'b' => "ostrich-icon-64.png",
                'X' => "combo-icon-64.png",
                _ => null
            };

            if (imageName != null)
            {
                return $"<img src='/images/{imageName}' alt='{symbol}' style='width:60px;height:60px;' />";
            }

            return symbol.ToString();
        }

        return ""; // puste pole
    }
}
