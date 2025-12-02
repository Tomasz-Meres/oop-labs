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
    public abstract string Greeting();

    public abstract string Info { get; }

    public void Upgrade()
    {
        if (level < 10)
            level += 1;
    }

    string Go(Direction direction) => $"{direction.ToString().ToLower()}";

    public string[] Go(Direction[] dirs)
    {
        string[] results = new string[dirs.Length];
        for (int i = 0; i < dirs.Length; i++)
        {
            results[i] = Go(dirs[i]);
        }
        return results;
    }

    public string[] Go(string dirText)
    {
        Direction[] dirs = DirectionParser.Parse(dirText);
        return Go(dirs);
    }

    public abstract int Power { get; }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}

