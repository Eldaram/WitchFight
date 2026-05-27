public class PlayerBuilder
{
    private Player player = null!;

    public PlayerBuilder()
    {
        Reset();
    }

    public PlayerBuilder Reset()
    {
        player = new Player();
        return this;
    }

    public PlayerBuilder SetName(string name)
    {
        player.Name = name;
        return this;
    }

    public PlayerBuilder SetHealth(int maxHealth)
    {
        player.MaxHealth = maxHealth;
        player.Health = maxHealth;
        return this;
    }

    public PlayerBuilder SetPower(int power)
    {
        player.power = power;
        return this;
    }

    public PlayerBuilder SetAttack(Move attack)
    {
        player.Attack = attack;
        return this;
    }

    public PlayerBuilder SetStandardAttack()
    {
        SetAttack(new Move(
            "Standard Attack", //TODO: Link to JSON text
            MoveLibrary.BasicAttack
        ));
        return this;
    }

    public PlayerBuilder SetSpecialMove(Move specialMove)
    {
        player.SpecialMove = specialMove;
        return this;
    }

    public PlayerBuilder SetHealMove(Move healMove)
    {
        player.HealMove = healMove;
        return this;
    }

    public PlayerBuilder SetStandardHealMove()
    {
        SetHealMove(new Move(
            "Heal", //TODO: Link to JSON text
            (user, _) => MoveLibrary.HealStandard(user),
            maxUses: 3,
            isSelfTargeting: true
        ));
        return this;
    }

    public PlayerBuilder SetDodgeChance(double dodgeChance)
    {
        player.dodgeChance = dodgeChance;
        return this;
    }

    public PlayerBuilder SetClass(string className)
    {
        player.Class = className;
        return this;
    }

    public Player GetResult()
    {
        return player;
    }
}