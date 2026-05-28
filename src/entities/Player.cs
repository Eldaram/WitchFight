public class Player : Entity
{
    public Move? SpecialMove;
    public Move? HealMove;
    public double dodgeChance = 0.0;
    public string? playerClass;
    public TerminalManager terminal = TerminalManager.Instance;

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
        Move playerMove;
        List<Move> moveList = GetAllMoves();
        while (true)
        {
            playerMove = terminal.AskMove(moveList);
            if (playerMove != null && playerMove.maxUses != 0 && playerMove.cooldown == 0) // TODO review
                break;
            terminal.PrintText("invalidMove");
        }
        Entity? target = playerMove.isSelfTargeting ? null : terminal.AskTarget(possibleTargets.ToList());
        playerMove.Use(this, target);

        if (target != null && target.health <= 0)
            return target;

        return null;
    }

    public void EndWaveHeal()
    {
        int healAmount = (int)(maxHealth * ENDWAVE_HEAL_PERCENTAGE);
        health = Math.Min(health + healAmount, maxHealth);
    }

    public List<Move> GetAllMoves()
    {
        List<Move> moves = new List<Move>();

        if (Attack != null)
            moves.Add(Attack);
        if (SpecialMove != null)
            moves.Add(SpecialMove);
        if (HealMove != null)
            moves.Add(HealMove);

        return moves;
    }

    public void UpdateCooldowns()
    {
        foreach (var move in GetAllMoves())
        {
            if (move.cooldown > 0)
                move.cooldown--;
        }
    }

    public void ResetMoves()
    {
        foreach (var move in GetAllMoves())
        {
            move.cooldown = 0;
            move.remainingUses = move.maxUses;
        }
    }
}