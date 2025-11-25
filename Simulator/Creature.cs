namespace Simulator;

public abstract class Creature
{
    private string name = "Unknown";
    public string Name
    {
        get { return name; }
        init
        {
            name = Validator.Shortener(value, 3, 25, '#');
        }
    }
    private int level;
    public int Level
    {
        get { return level; }
        init
        {
            level = Validator.Limiter(value, 1, 10);
        }
    }

    public Creature()
    {
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }
    public abstract void SayHi();

    public string Info
    {
        get { return $"{Name} [{Level}]"; }
    }

    public void Upgrade()
    {
        if (level < 10)
            level += 1;
    }

    public void Go(Direction dir)
    {
        string directionText = dir.ToString().ToLower();
        Console.WriteLine($"{Name} goes {directionText}");
    }

    public void Go(Direction[] dirs)
    {
        foreach (var dir in dirs)
        {
            Go(dir);
        }
    }

    public void Go(string dirText)
    {
        Direction[] dirs = DirectionParser.Parse(dirText);
        Go(dirs);
    }

    public abstract int Power { get; }
}

