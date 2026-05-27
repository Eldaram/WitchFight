static class ClassBuilder
{
    public static Player CreateWarrior(string name)
    {
        return new PlayerBuilder()
            .SetName(name)
            .SetClass("Warrior") //TODO: Link to JSON text
            .SetHealth(120)
            .SetPower(18)
            .SetDodgeChance(0.1)
            .SetStandardAttack()
            .SetStandardHealMove()
            .SetSpecialMove(new Move(
                "Warrior Special", //TODO: Link to JSON text
                MoveLibrary.WarriorSpecial,
                cooldown: 2
            ))
            .GetResult();
    }

    public static Player CreateMage(string name)
    {
        return new PlayerBuilder()
            .SetName(name)
            .SetClass("Mage") //TODO: Link to JSON text
            .SetHealth(80)
            .SetPower(12)
            .SetDodgeChance(0.05)
            .SetStandardAttack()
            .SetStandardHealMove()
            .SetSpecialMove(new Move(
                "Mage Special", //TODO: Link to JSON text
                MoveLibrary.MageSpecial,
                cooldown: 3
            ))
            .GetResult();
    }

    public static Player CreateRogue(string name)
    {
        return new PlayerBuilder()
            .SetName(name)
            .SetClass("Rogue") //TODO: Link to JSON text
            .SetHealth(90)
            .SetPower(14)
            .SetDodgeChance(0.25)
            .SetStandardAttack()
            .SetStandardHealMove()
            .SetSpecialMove(new Move(
                "Rogue Special", //TODO: Link to JSON text
                MoveLibrary.RogueSpecial,
                cooldown: 2
            ))
            .GetResult();
    }
}