using Simulator.Maps;

namespace SimConsole
{
    public class MapVisualizer
    {
        private readonly Map _map;

        public MapVisualizer(Map map)
        {
            _map = map;
        }

        public void Draw()
        {
            int width = _map.SizeX;
            int height = _map.SizeY;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write(Box.TopLeft);
            for (int x = 0; x < width - 1; x++) Console.Write($"{Box.Horizontal}{Box.TopMid}");
            Console.WriteLine($"{Box.Horizontal}{Box.TopRight}");

            for (int y = height - 1; y >= 0; y--)
            {
                if (y < height - 1)
                {
                    Console.Write(Box.MidLeft);
                    for (int x = 0; x < width - 1; x++) Console.Write($"{Box.Horizontal}{Box.Cross}");
                    Console.WriteLine($"{Box.Horizontal}{Box.MidRight}");
                }

                Console.Write(Box.Vertical);
                for (int x = 0; x < width; x++)
                {
                    var creatures = _map.At(x, y);
                    char c = ' ';
                    if (creatures != null && creatures.Count > 0)
                    {
                        c = creatures.Count > 1 ? 'X' : creatures[0].Symbol;
                    }
                    Console.Write($"{c}{Box.Vertical}");
                }
                Console.WriteLine();
            }

            Console.Write(Box.BottomLeft);
            for (int x = 0; x < width - 1; x++) Console.Write($"{Box.Horizontal}{Box.BottomMid}");
            Console.WriteLine($"{Box.Horizontal}{Box.BottomRight}");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
}
