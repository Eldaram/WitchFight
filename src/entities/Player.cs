public class Player : Entity
{
    public Move? SpecialMove;
    public Move? HealMove;
    public double dodgeChance = 0.0;
    public string? playerClass;

    private double ENDWAVE_HEAL_PERCENTAGE = 0.2;

    public override void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true)
    {
        if (canDodge && Random.Shared.NextDouble() < dodgeChance)
            return;

        health -= amount;
        if (health < 0)
            health = 0;
    }

    public override Entity? ChooseAction(IEnumerable<Entity> possibleTargets)
    {
        // TODO: Implement player input for choosing actions and targets
        return null;
    }

    public void Heal()
    {
        HealMove?.Use(this);
    }

    public void SpecialAttack(Entity target)
    {
        SpecialMove?.Use(this, target);
    }

    public void EndWaveHeal()
    {
        int healAmount = (int)(maxHealth * ENDWAVE_HEAL_PERCENTAGE);
        health = Math.Min(health + healAmount, maxHealth);
    }
}