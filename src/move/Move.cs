public class Move
{
    public string name;
    public int cooldown;
    public int maxUses;
    const int INFINITE_USES = -1;

    public int remainingUses;
    public int remainingCooldown;

    public bool isSelfTargeting;

    public Action<Entity, Entity?> Action;

    public Move(string name, Action<Entity, Entity?> action, int cooldown = 0, int maxUses = INFINITE_USES, bool isSelfTargeting = false)
    {
        this.name = name;
        this.cooldown = cooldown;
        this.maxUses = maxUses;
        this.isSelfTargeting = isSelfTargeting;
        remainingUses = maxUses;
        remainingCooldown = 0;
        Action = action;
    }
    
    public bool Use(Entity user, Entity? target = null)
    {
        if (remainingCooldown > 0 || remainingUses == 0)
            return false;

        if (isSelfTargeting)
            Action(user, null);
        else if (target != null)
            Action(user, target);
        else
            return false;

        remainingCooldown = cooldown;

        if (remainingUses > 0)
            remainingUses--;

        return true;
    }
}