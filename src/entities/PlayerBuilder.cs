public class PlayerBuilder : EntityBuilder<PlayerBuilder, Player>
{
    public PlayerBuilder()
    {
        Reset();
    }

    public PlayerBuilder SetSpecialMove(Move specialMove)
    {
        entity.SpecialMove = specialMove;
        return this;
    }

    public PlayerBuilder SetHealMove(Move healMove)
    {
        entity.HealMove = healMove;
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
        entity.dodgeChance = dodgeChance;
        return this;
    }

    public PlayerBuilder SetClass(string className)
    {
        entity.playerClass = className;
        return this;
    }
}