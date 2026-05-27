public class Player : Entity
{
    public Move? SpecialMove;
    public Move? HealMove;
    public double dodgeChance = 0.0;
    public string? Class;

    public override void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true)
    {
        if (canDodge && Random.Shared.NextDouble() < dodgeChance)
            return;

        Health -= amount;
        if (Health < 0)
            Health = 0;
    }

    public void Heal()
    {
        HealMove?.Use(this);
    }

    public void SpecialAttack(Entity target)
    {
        SpecialMove?.Use(this, target);
    }
}