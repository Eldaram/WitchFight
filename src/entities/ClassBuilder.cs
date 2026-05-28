static class ClassBuilder
{
    private static Player CreateFromClassData(string classKey, string name)
    {
        ClassJsonManager classJsonManager = ClassJsonManager.Instance;

        return new PlayerBuilder()
            .SetName(name)
            .SetClass(classKey)
            .SetHealth(classJsonManager.GetInt(classKey, "health"))
            .SetPower(classJsonManager.GetInt(classKey, "power"))
            .SetDodgeChance(classJsonManager.GetDouble(classKey, "dodgeChance"))
            .SetStandardAttack()
            .SetStandardHealMove()
            .SetSpecialMove(new Move(
                classJsonManager.GetString(classKey, "specialMoveName"),
                GetSpecialMoveAction(classKey),
                cooldown: classJsonManager.GetInt(classKey, "specialMoveCooldown")
            ))
            .GetResult();
    }

    private static Action<Entity, Entity?> GetSpecialMoveAction(string classKey)
    {
        return classKey switch
        {
            "warrior" => MoveLibrary.WarriorSpecial,
            "mage" => MoveLibrary.MageSpecial,
            "rogue" => MoveLibrary.RogueSpecial,
            _ => throw new Exception($"Unknown class {classKey}")
        };
    }

    public static Player CreateWarrior(string name)
    {
        return CreateFromClassData("warrior", name);
    }

    public static Player CreateMage(string name)
    {
        return CreateFromClassData("mage", name);
    }

    public static Player CreateRogue(string name)
    {
        return CreateFromClassData("rogue", name);
    }
}