namespace Simulator.Maps.StaticObjects;

public class Inn : StaticObject
{
    public override char MapSymbol => 'I';

    public override void OnEntry(IMappable visitor)
    {
        if (visitor is IDamageable target)
        {
            // Karczma leczy każdego, kto może otrzymać obrażenia
            target.Heal(target.MaxHealth / 10);
        }
    }

}
