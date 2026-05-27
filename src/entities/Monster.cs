public class Monster : Entity
{
    public int armor;
    public override void TakeDamage(int amount, bool isTrueDamage = false, bool canDodge = true)
    {
         Health -= isTrueDamage ? amount :Math.Max(0, amount - armor);
        
        if (Health < 0)
            Health = 0;
    }
}