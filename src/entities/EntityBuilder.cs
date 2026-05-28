abstract public class EntityBuilder<TBuilder, TEntity>
    where TBuilder : EntityBuilder<TBuilder, TEntity>
    where TEntity : Entity, new()
{
    protected TEntity entity = null!;
    
    public TBuilder Reset()
    {
        entity = new TEntity();
        return (TBuilder)this;
    }
    public TBuilder SetName(string name)
    {
        entity.name = name;
        return (TBuilder)this;
    }
    public TBuilder SetHealth(int maxHealth)
    {
        entity.maxHealth = maxHealth;
        entity.health = maxHealth;
        return (TBuilder)this;
    }
    public TBuilder SetPower(int power)
    {
        entity.power = power;
        return (TBuilder)this;
    }
    public TBuilder SetAttack(Move attack)
    {
        entity.Attack = attack;
        return (TBuilder)this;
    }

    public TBuilder SetStandardAttack()
    {
        return SetAttack(new Move(
            "Basic Attack", //TODO: Link to JSON text
            MoveLibrary.BasicAttack
        ));
    }

    public TEntity GetResult()
    {
        return entity;
    }
}