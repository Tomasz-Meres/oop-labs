namespace Simulator;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Simulator!\n");
        //TestCreatures();
        //TestDirections();
        //TestElfsAndOrcs();
        //TestValidators();
        TestObjectsToString();

    }

    //static void TestCreatures()
    //{
    //    Creature c = new() { Name = "   Shrek    ", Level = 20 };
    //    c.SayHi();
    //    c.Upgrade();
    //    Console.WriteLine(c.Info);

    //    c = new("  ", -5);
    //    c.SayHi();
    //    c.Upgrade();
    //    Console.WriteLine(c.Info);

    //    c = new("  donkey ") { Level = 7 };
    //    c.SayHi();
    //    c.Upgrade();
    //    Console.WriteLine(c.Info);

    //    c = new("Puss in Boots – a clever and brave cat.");
    //    c.SayHi();
    //    c.Upgrade();
    //    Console.WriteLine(c.Info);

    //    c = new("a                            troll name", 5);
    //    c.SayHi();
    //    c.Upgrade();
    //    Console.WriteLine(c.Info);

    //    var a = new Animals() { Description = "   Cats " };
    //    Console.WriteLine(a.Info);

    //    a = new() { Description = "Mice           are great", Size = 40 };
    //    Console.WriteLine(a.Info);
    //}

    //static void TestDirections()
    //{
    //    Creature c = new("Shrek", 7);
    //    c.SayHi();

    //    Console.WriteLine("\n* Up");
    //    c.Go(Direction.Up);

    //    Console.WriteLine("\n* Right, Left, Left, Down");
    //    Direction[] directions = {
    //    Direction.Right, Direction.Left, Direction.Left, Direction.Down
    //};
    //    c.Go(directions);

    //    Console.WriteLine("\n* LRL");
    //    c.Go("LRL");

    //    Console.WriteLine("\n* xxxdR lyyLTyu");
    //    c.Go("xxxdR lyyLTyu");
    //}

    static void TestElfsAndOrcs()
    {
        Console.WriteLine("HUNT TEST\n");
        var o = new Orc() { Name = "Gorbag", Rage = 7 };
        o.SayHi();
        for (int i = 0; i < 10; i++)
        {
            o.Hunt();
            o.SayHi();
        }

        Console.WriteLine("\nSING TEST\n");
        var e = new Elf("Legolas", agility: 2);
        e.SayHi();
        for (int i = 0; i < 10; i++)
        {
            e.Sing();
            e.SayHi();
        }

        Console.WriteLine("\nPOWER TEST\n");
        Creature[] creatures = {
        o,
        e,
        new Orc("Morgash", 3, 8),
        new Elf("Elandor", 5, 3)
        };
        foreach (Creature creature in creatures)
        {
            Console.WriteLine($"{creature.Name,-15}: {creature.Power}");
        }
    }

    static void TestValidators()
    {
        Creature c = new Orc() { Name = "   Shrek    ", Level = 20 };
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Elf("  ", -5);
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("  donkey ") { Level = 7 };
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Elf("Puss in Boots – a clever and brave cat.");
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Orc("a                            troll name", 5);
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        var a = new Animals() { Description = "   Cats " };
        Console.WriteLine(a.Info);

        a = new() { Description = "Mice           are great", Size = 40 };
        Console.WriteLine(a.Info);
    }

    static void TestObjectsToString()
    {
        object[] myObjects = {
        new Animals() { Description = "dogs"},
        new Birds { Description = "  eagles ", Size = 10 },
        new Elf("e", 15, -3),
        new Orc("morgash", 6, 4)
    };
        Console.WriteLine("\nMy objects:");
        foreach (var o in myObjects) Console.WriteLine(o);
        /*
            My objects:
            ANIMALS: Dogs <3>
            BIRDS: Eagles (fly+) <10>
            ELF: E## [10][0]
            ORC: Morgash [6][4]
        */
    }

}
