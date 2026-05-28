abstract public class Entity
{
    public string name = string.Empty;
    public int health;
    public int maxHealth;
    public int power;
    public Move? Attack;

    public abstract void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true);

    public abstract Entity? ChooseAction(IEnumerable<Entity> possibleTargets);
}