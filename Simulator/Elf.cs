namespace Simulator;

public class Elf : Creature
{
    private int _agility;
    public int Agility 
    { get { 
            int boosted = MagicBuffDuration > 0 ? _agility + 2 : _agility; 
            return Validator.Limiter(boosted, 0, 10);
        } 
      init {
            _agility = Validator.Limiter(value, 0, 10);
        }
    }
    private static int elfCount = 0;
    public override int MagicBuffDuration { get; protected set; }

    public void Sing()
    {
        elfCount++;
        if (elfCount % 3 == 0)
        {
            if (_agility < 10)
            {
                _agility++;
            }

        }
    }

    public Elf(string name, int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }

    public Elf() : base() { }

    public override char MapSymbol => 'E';
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
            return Level * 5 + Agility * 2;
        }
    }

    public override void TakeDamage(int amount)
    {
        // Obliczamy szansę: 1 agility = 3%, max 30%
        int dodgeChance = Agility * 2;

        Random rnd = new();
        if (rnd.Next(100) < dodgeChance)
        {
            // UNIK! Metoda kończy się tutaj, nie zadajemy obrażeń
            return;
        }

        // Jeśli unik się nie udał, otrzymujemy obrażenia zgodnie z bazową logiką
        base.TakeDamage(amount);
    }
}
