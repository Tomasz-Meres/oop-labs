namespace Simulator;

public class Orc : Creature
{
    private int _rage;
    public int Rage
    {
        get {
            int boosted = MagicBuffDuration > 0 ? _rage + 4 : _rage;
            return Validator.Limiter(boosted, 0, 10);
        }
        init
        {
            _rage = Validator.Limiter(value, 0, 10);
        }
    }
    private static int orcCount = 0;
    public override int MagicBuffDuration { get; protected set; }
    public void Hunt() 
    { 
        orcCount++;
        if (orcCount % 2 == 0)
        {
            if (_rage < 10)
            {
                _rage++;
            }
        }
    }

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }

    public Orc() : base() { }

    public override char MapSymbol => 'O';
    public override string Greeting()
    {
        return $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
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

    public override void Attack(IDamageable target)
    {
        if (IsDeleted || target == null || target == this) return;

        int finalPower = this.Power;

        // Logika ciosu krytycznego: 1 Rage = 5% szansy
        Random rnd = new();
        if (rnd.Next(100) < (Rage * 5))
        {
            finalPower *= 2; // Podwajamy obrażenia
        }

        target.TakeDamage(finalPower);
    }

}
