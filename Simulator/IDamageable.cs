namespace Simulator;

public interface IDamageable
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    void TakeDamage(int amount);
    void Heal(int amount);
}
