namespace Simulator.Maps.StaticObjects;

public class MagicSource : StaticObject
{
    public override char MapSymbol => 'S';

    /// <summary>
    /// Applies a magic bonus for a specified number of moves.
    /// The duration value should be 1 greater than the intended number of turns,
    /// as UpdateBuffs() is called at the start of each turn and immediately subtracts 1.
    /// </summary>
    public override void OnEntry(IMappable visitor)
    {
        if (visitor is Elf e)
        {
            e.ApplyMagicBuff(5);
        }
        else if (visitor is Orc o)
        {
            o.ApplyMagicBuff(3);
        }
    }

}
