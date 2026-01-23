using Simulator.Maps;

namespace Simulator;

public abstract class Creature : IMappable, IDeletable, IAttacker
{
    private Map? _map;
    private Point _point;
    public Point Position => _point;
    public Map? Map => _map;

    private string _name = "Unknown";
    private int _damage = 0;
    public virtual int MaxHealth => Level * 30;
    public virtual int CurrentHealth => Math.Max(0, MaxHealth - _damage);
    public bool IsDeleted => CurrentHealth <= 0;
    public virtual Point GetDestination(Direction d)
    {
        return Map?.Next(Position, d) ?? Position;
    }

    public string Name
    {
        get { return _name; }
        init
        {
            _name = Validator.Shortener(value, 3, 25, '#');
        }
    }
    private int _level;
    public int Level
    {
        get { return _level; }
        init
        {
            _level = Validator.Limiter(value, 1, 10);
        }
    }

    public virtual char MapSymbol => '?';

    public void InitMapAndPosition(Map map, Point StartingPosition)
    {
        if (map == null) return;
        if (!map.Exist(StartingPosition))
            throw new ArgumentOutOfRangeException(nameof(StartingPosition), "Point out of map");

        map.Add(this, StartingPosition);

        _map = map;
        _point = StartingPosition;
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
        if (_level < 10)
            _level += 1;
    }

    public void Go(Direction direction)
    {
        if (_map == null) return;

        Point NextPoint = _map.Next(_point, direction);

        try
        {
            _map.Move(this, NextPoint);
            _point = NextPoint;
        }
        catch
        {

        }
    }

    public abstract int Power { get; }

    public abstract int MagicBuffDuration { get; protected set; }

    public void ApplyMagicBuff(int duration) => MagicBuffDuration = duration;
    public void UpdateBuffs() { if (MagicBuffDuration > 0) MagicBuffDuration--; }

    public virtual void Attack(IDamageable target)
    {
        // Atakujemy tylko jeśli sami żyjemy i cel istnieje
        if (IsDeleted || target == null || target == this) return;
        
        target.TakeDamage(this.Power);
    }

    public virtual void TakeDamage(int amount)
    {
        _damage = Math.Min(MaxHealth, _damage + Math.Max(0, amount));
    }

    public virtual void Heal(int amount)
    {
        if (IsDeleted) return;

        _damage = Math.Max(0, _damage - Math.Max(0, amount));
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}

