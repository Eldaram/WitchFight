static class MoveLibrary
{
    static double WARRIOR_MULTIPLIER = 1.5;
    static int MAGE_DAMAGE = 20;
    static double ROGUE_CRIT_CHANCE = 0.3;
    static int HEAL_AMOUNT = 25;

    public static void BasicAttack(Entity user, Entity? target)
    {
        target?.TakeDamage(user.power);
    }

    public static void WarriorSpecial(Entity user, Entity? target)
    {
        int damage = (int)(user.power * WARRIOR_MULTIPLIER);
        target?.TakeDamage(damage);
    }

    public static void MageSpecial(Entity user, Entity? target)
    {
        target?.TakeDamage(MAGE_DAMAGE, isTrueDamage: true);
    }

    public static void RogueSpecial(Entity user, Entity? target)
    {
        int damage = user.power * (Random.Shared.NextDouble() < ROGUE_CRIT_CHANCE ? 2 : 1);
        target?.TakeDamage(damage);
    }

    public static void HealStandard(Entity user)
    {
        user.Health = Math.Min(user.Health + HEAL_AMOUNT, user.MaxHealth);
    }

    public static void HealCustom(int amount, Entity user)
    {
        user.Health = Math.Min(user.Health + amount, user.MaxHealth);
    }
}