public class Monster : Entity
{
    public int armor;
    public override void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true)
    {
         health -= isTrueDamage ? amount :Math.Max(0, amount - armor);
        
        if (health < 0)
            health = 0;
    }

    public override Entity? ChooseAction(IEnumerable<Entity> possibleTargets)
    {
        if (possibleTargets.Any())
        {
            var target = possibleTargets.First();

            Attack?.Use(this, target);

            bool hasDied = target.health <= 0;
            if (hasDied)
                return target;
        }
        return null;
    }
}