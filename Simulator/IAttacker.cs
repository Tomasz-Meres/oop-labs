namespace Simulator;

public interface IAttacker : IDamageable
{
    int Power { get; }
    void Attack(IDamageable target);
}
