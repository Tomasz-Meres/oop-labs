namespace Simulator;

public abstract class Creature
{
    private string name = "Unknown";
    public string Name
    {
        get { return name; }
        init
        {
            if (value == null)
                value = "Unknown";

            value = value.Trim();

            if (value.Length > 25)
                value = value.Substring(0, 25);

            value = value.Trim();

            if (value.Length < 3)
                value = value.PadRight(3, '#');

            if (char.IsLetter(value[0]) && char.IsLower(value[0]))
                value = char.ToUpper(value[0]) + value.Substring(1);
            name = value;
        }
    }
    private int level;
    public int Level
    {
        get { return level; }
        init
        {
            if (value < 1)
                value = 1;
            if (value > 10)
                value = 10;
            level = value;
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

