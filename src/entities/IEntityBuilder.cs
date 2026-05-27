public interface IEntityBuilder
{
    IEntityBuilder Reset();
    IEntityBuilder SetName(string name);
    IEntityBuilder SetHealth(int maxHealth);
    IEntityBuilder SetPower(int power);
    IEntityBuilder SetAttack(Move attack);
}