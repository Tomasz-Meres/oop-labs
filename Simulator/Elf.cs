namespace Simulator;

public class Elf : Creature
{
    private int agility;
    public int Agility 
    { get { return agility; } 
        init {
            agility = Validator.Limiter(value, 0, 10);
        }
    }
    private static int elfCount = 0;
    public void Sing()
    {
        elfCount++;
        if (elfCount % 3 == 0)
        {
            if (agility < 10)
            {
                agility++;
            }

        }
    }

    public Elf(string name, int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }

    public Elf() : base() { }

    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, my agility is {Agility}.";
    }

    public override string Info
    {
        get { return $"{Name} [{Level}][{Agility}]"; }
    }

    public override int Power
    {
        get
        {
            return Level * 8 + Agility * 2;
        }
    }
}
