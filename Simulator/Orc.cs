namespace Simulator;

public class Orc : Creature
{
    private int rage;
    public int Rage
    {
        get { return rage; }
        init
        {
            rage = Validator.Limiter(value, 0, 10);
        }
    }
    private static int orcCount = 0;
    public void Hunt() 
    { 
        Console.WriteLine($"{Name} is hunting.");
        orcCount++;
        if (orcCount % 2 == 0)
        {
            if (rage < 10)
            {
                rage++;
                Console.WriteLine($"{Name} has improved rage to {rage}!");
            }
            else
            {
                Console.WriteLine($"{Name} already has maximum rage.");
            }
        }
    }

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }

    public Orc() : base() { }

    public override void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.");
    }
    public override string Info
    {
        get { return $"{Name} [{Level}][{Rage}]"; }
    }

    public override int Power
    {
        get
        {
            return Level * 7 + Rage * 3;
        }
    }
}
