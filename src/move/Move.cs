public class Move
{
    public string Name;
    public int cooldown;
    public int maxUses;

    public int remainingUses;
    public int remainingCooldown;

    public bool isSelfTargeting;

    public Action<Entity, Entity?> Action;

    public Move(string name, Action<Entity, Entity?> action, int cooldown = 0, int maxUses = -1, bool isSelfTargeting = false)
    {
        Name = name;
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

    //Quand un move est utilisé, on vérifie s'il est disponible
    //(pas en cooldown et avec des utilisations restantes)
    // puis on l'exécute et on met à jour les compteurs de cooldown et d'utilisations.

    //On doit pouvoir abonner un move au système de tour pour réduire le cooldown à chaque tour.

    //on doit pouvoir abonner un move à un système de tour pour savoir quand la partie
    //est fini pour reset les cooldowns et les utilisations restantes.
}