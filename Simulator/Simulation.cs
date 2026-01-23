using Simulator.Maps;
using Simulator.Maps.StaticObjects;
using System;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// Creatures moving on the map.
    /// </summary>
    public List<IMappable> Beings { get; }

    /// <summary>
    /// Starting positions of creatures.
    /// </summary>
    public List<Point> Positions { get; }
    /// <summary>
    /// Objects static on the map.
    /// </summary>
    public List<StaticObject> StaticObjects { get; }
    /// <summary>
    /// Static objects positions.
    /// </summary>
    public List<Point> StaticObjectsPositions { get; }


    /// <summary>
    /// Cyclic list of creatures moves.
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first creature, second for second and so on.
    /// When all creatures make moves,
    /// next move is again for first creature and so on.
    /// </summary>
    /// 
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Index of current turn in moves list.
    /// </summary>
    private int _currentTurnIndex = 0;

    /// <summary>
    /// index of current creature to move.
    /// </summary>
    private int _currentBeingIndex = 0;

    /// <summary>
    /// Creature which will be moving current turn.
    /// </summary>
    public IMappable CurrentBeing
    {
        get { return Beings[_currentBeingIndex]; }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get { return _parsedMoves[_currentTurnIndex].ToString().ToLower(); }
    }

    /// <summary>
    /// parsed list of moves.
    /// </summary>
    private readonly List<Direction> _parsedMoves;
    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if creatures' list is empty,
    /// if number of creatures differs from
    /// number of starting positions.
    /// </summary>
    public Simulation(
        Map map,
        List<IMappable> beings,
        List<Point> positions,
        List<StaticObject> staticObjects,
        List<Point> staticObjectsPositions,
        string moves)
    {
        if (beings.Count == 0)
            throw new ArgumentException("Creatures list is empty", nameof(beings));
        if (beings.Count != positions.Count)
            throw new ArgumentException("Number of creatures differs from number of starting positions");

        Map = map;
        Beings = beings;
        Positions = positions;
        StaticObjects = staticObjects;
        StaticObjectsPositions = staticObjectsPositions;
        Moves = moves ?? "";
        _parsedMoves = DirectionParser.Parse(moves);

        for (int i = 0; i < StaticObjects.Count; i++)
        {
            StaticObjects[i].InitMapAndPosition(Map, StaticObjectsPositions[i]);
        }

        for (int i = 0; i < Beings.Count; i++)
        {
            Beings[i].InitMapAndPosition(Map, Positions[i]);
        }

    }

    private int _movesDone = 0;

    /// <summary>
    /// Additional messages from actions taken during the turn.
    /// </summary>
    public List<string> ActionMessages { get; } = new();

    /// <summary>
    /// Makes one move of current creature in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation has finished.");
        ActionMessages.Clear();

        Direction dir = _parsedMoves[_currentTurnIndex];
        Point target = CurrentBeing.GetDestination(dir);
        bool isMountain = Map.At(target)?.Any(o => o is Mountain) ?? false;

        if (isMountain)
        {
            ActionMessages.Add($"{CurrentBeing.GetType().Name} can't stay on the Mountain!");
        }
        else
        {
            // RUCH: Wykonujemy tylko jeśli nie ma góry
                CurrentBeing.Go(dir);

            // AKTUALIZACJA BUFFÓW: Zawsze
            if (CurrentBeing is Creature cr)
            {
                cr.UpdateBuffs();
            }

            // INTERAKCJE I WALKA: Wykonujemy ZAWSZE na polu, na którym postać wylądowała
            // (nawet jeśli to to samo pole co wcześniej)
            if (CurrentBeing is IMappable being)
            {
                var objectsOnCurrentSquare = Map.At(being.Position);
                bool isInInn = objectsOnCurrentSquare?.Any(o => o is Inn) ?? false;

                // WALKA
                if (!isInInn && CurrentBeing is IAttacker attacker)
                {
                    var enemies = objectsOnCurrentSquare?
                        .Where(o => o != attacker && o is IDamageable)
                        .Cast<IDamageable>()
                        .ToList();

                    if (enemies != null)
                    {
                        foreach (var enemy in enemies)
                        {
                            int hpBefore = (enemy as Creature)?.CurrentHealth ?? 0;

                            attacker.Attack(enemy);

                            // Sprawdzamy stan po ataku
                            if (enemy is Creature creature)
                            {
                                int damage = hpBefore - creature.CurrentHealth;
                                if (damage > 0)
                                    ActionMessages.Add($"{attacker} attacked {enemy} dealing {damage} damage!");
                                else
                                    ActionMessages.Add($"{attacker} attacked {enemy} but missed!");

                            }

                            // DODANY IF: Sprawdzamy czy cel (target) umarł po ataku
                            if (enemy is IDeletable targetDeletable && targetDeletable.IsDeleted)
                            {
                                ActionMessages.Add($"{enemy} has been defeated!");

                                // Musimy zrzutować na IMappable, żeby Twoja metoda Map.Remove zadziałała
                                if (enemy is IMappable mappableEnemy)
                                {
                                    Map.Remove(mappableEnemy);

                                    // Usuwamy z listy Beings, aby postać nie miała już swojej tury
                                    if (Beings.Contains(mappableEnemy))
                                    {
                                        int removedIndex = Beings.IndexOf(mappableEnemy);
                                        Beings.RemoveAt(removedIndex);

                                        // Jeśli usunięty wróg był w kolejce przed aktualną postacią,
                                        // musimy cofnąć indeks, żeby nie "przeskoczyć" następnej istoty.
                                        if (removedIndex <= _currentBeingIndex)
                                        {
                                            _currentBeingIndex--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // OBIEKTY STATYCZNE (Plaga itp.)
                var interactions = objectsOnCurrentSquare?.OfType<StaticObject>();
                if (interactions != null)
                {
                    foreach (var obj in interactions)
                    {
                        if (obj is Inn)
                        {
                            if (being is Animals animal)
                            {
                                ActionMessages.Add($"{animal.Description} is waiting outside the Inn.");
                                continue;
                            }

                            // Zapamiętujemy HP przed leczeniem (tylko dla istot z HP)
                            int hpBefore = (being as Creature)?.CurrentHealth ?? 0;

                            obj.OnEntry(being);

                            int hpAfter = (being as Creature)?.CurrentHealth ?? 0;
                            int healed = hpAfter - hpBefore;

                            if (healed > 0)
                            {
                                ActionMessages.Add($"{being} got heal {healed} HP in the Inn.");
                            }
                            else
                            {
                                ActionMessages.Add($"{being} is resting in the Inn.");
                            }
                        }
                        else if (obj is Plague plague)
                        {
                            int sizeBefore = (int)((being as Animals)?.Size ?? 0);
                            obj.OnEntry(being);
                            if (being is Creature creature)
                            {
                                ActionMessages.Add($"{being} got infected by Plague and lost 5HP!");
                            }
                            else if (being is Animals animal)
                            {
                                int sizeAfter = (int)animal.Size;
                                int lostSize = sizeBefore - sizeAfter;
                                ActionMessages.Add($"{being} got infected by Plague and decreased size by {lostSize}!");
                            }
                        }
                        else if (obj is MagicSource)
                        {
                            if (being is Animals)
                            {
                                ActionMessages.Add($"{being} ignores the hum of the Magic Source.");
                                continue;
                            }
                            obj.OnEntry(being);
                            ActionMessages.Add($"{being} felt a magical surge!");
                        }
                    }
                }

                // ŚMIERĆ POSTACI
                if (CurrentBeing is IDeletable cdeletable && cdeletable.IsDeleted)
                {
                    Map.Remove(CurrentBeing);
                    Beings.Remove(CurrentBeing);
                    _currentBeingIndex--;
                }
            }
        }

        // zmiana tury
        _currentBeingIndex = (_currentBeingIndex + 1) % Beings.Count;
        _currentTurnIndex = (_currentTurnIndex + 1) % _parsedMoves.Count;
        _movesDone++;
        if (_movesDone >= _parsedMoves.Count || Beings.Count == 0)
                Finished = true;
    }
}