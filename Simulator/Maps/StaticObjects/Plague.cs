namespace Simulator.Maps.StaticObjects;

public class Plague : StaticObject
{
    public override char MapSymbol => 'P';

    public override void OnEntry(IMappable visitor)
    {
        if (visitor is Creature creature)
        {
            creature.ApplyMagicBuff(0);
            creature.TakeDamage(5);
        }
        else if (visitor is Animals a)
        {
            a.DecreaseSize(0.20);
        }
    }
}
