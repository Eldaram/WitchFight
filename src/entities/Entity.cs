abstract public class Entity
{
    public string Name = string.Empty;
    public int Health;
    public int MaxHealth;
    public int power;
    public Move? Attack;

    public void dammage(Entity target)
    {
        Attack?.Use(this, target);
    }

    public abstract void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true);
}