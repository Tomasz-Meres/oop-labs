using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;
using Simulator.Maps.StaticObjects;

namespace SimWeb.Pages;

public class SimulationModel : PageModel
{
    // Zmieniamy na pole statyczne, aby dane nie zmienia³y siê przy odœwie¿aniu
    private static SimulationLog? _log;
    public SimulationLog Log => _log!;

    [BindProperty(SupportsGet = true)]
    public int Turn { get; set; } = 0;

    public int CurrentTurn => Turn;
    public int MaxTurn => Log.TurnLogs.Count - 1;
    public string CurrentMove => Log.TurnLogs[CurrentTurn].Move;
    public int SizeX => Log.SizeX;
    public int SizeY => Log.SizeY;

    public SimulationModel(IHttpContextAccessor httpContextAccessor)
    {
        // 2. Warunek: Twórz symulacjê tylko, jeœli _cachedLog jest pusty
        if (_log == null)
        {
            SmallTorusMap map = new(8, 6);
            var beings = new List<IMappable>
            {
                new Orc("Gorbag", 4, 5),
                new Elf("Elandor", 3, 3),
                new Animals() { Description = "Rabbits", Size = 10 },
                new Birds() { Description = "Eagles", Size = 2, CanFly = true },
                new Birds() { Description = "Ostriches", Size = 2, CanFly = false }
            };
            var points = new List<Point>
            {
                new Point(0,2), new Point(4,2), new Point(3,4), new Point(4,5), new Point(3,0)
            };

            var staticObjects = new List<StaticObject>
            {
                new Mountain(), new Inn(), new Plague(), new MagicSource()
            };
            var staticPositions = new List<Point>
            {
                new Point(3,5), new Point(2,3), new Point(2,1), new Point(3,2)
            };

            string moves = "rlulr rlddu sdrdr uuss dd";

            var simulation = new Simulation(map, beings, points, staticObjects, staticPositions, moves);
            _log = new SimulationLog(simulation);
        }

        // 3. Logika sesji zostaje, ale teraz operuje na sta³ym _cachedLog
        var context = httpContextAccessor.HttpContext;

        if (context != null)
        {
            int? savedTurn = context.Session.GetInt32("LastTurn");

            // Zamiast Request.Query u¿ywamy context.Request.Query
            bool hasTurnInQuery = context.Request.Query.ContainsKey("Turn");

            if (savedTurn.HasValue && !hasTurnInQuery)
            {
                Turn = Math.Clamp(savedTurn.Value, 0, _log.TurnLogs.Count - 1);
            }
        }
    }

    public void OnGet()
    {
        // Walidacja, ¿eby nie wyjœæ poza zakres logu
        Turn = Math.Clamp(Turn, 0, MaxTurn);
        HttpContext.Session.SetInt32("LastTurn", Turn);
    }

    public string GetCellContent(int x, int y)
    {
        var point = new Point(x, y);
        var turn = _log.TurnLogs[CurrentTurn];
        string html = "<div style='position:relative; width:60px; height:60px;'>";
        bool content = false;

        // 1. T£O: Obiekty statyczne 
        var staticObj = _log.simulation.StaticObjects
            .Zip(_log.simulation.StaticObjectsPositions, (obj, pos) => new { obj, pos })
            .FirstOrDefault(p => p.pos.X == x && p.pos.Y == y);

        if (staticObj != null)
        {
            string img = GetImg(staticObj.obj.MapSymbol);
            html += $"<img src='/images/{img}' style='position:absolute; z-index:1; width:60px; height:60px;' />";
            content = true;
        }

        // 2. PRZÓD: Istoty
        if (turn.Symbols.TryGetValue(point, out char symbol))
        {
            // Rysujemy tylko jeœli to nie jest ten sam obiekt statyczny
            if (!"MPIS".Contains(symbol))
            {
                string img = GetImg(symbol);
                html += $"<img src='/images/{img}' style='position:absolute; z-index:2; width:40px; height:40px; top:10px; left:10px;' />";
                content = true;
            }
        }

        html += "</div>";
        return content ? html : "";
    }

    private string GetImg(char s) => s switch
    {
        'O' => "orc-icon-64.png",
        'E' => "elf-icon-64.png",
        'M' => "mountain-icon.png",
        'P' => "plague-icon.png",
        'I' => "inn-icon.png",
        'S' => "magic-source-icon.png",
        'A' => "rabbit-icon-64.png",
        'B' => "eagle-icon-64.png",
        'b' => "ostrich-icon-64.png",
        _ => "combo-icon-64.png"
    };
}
