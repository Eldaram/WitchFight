public class MonsterBuilder : EntityBuilder<MonsterBuilder, Monster>
{
    public MonsterBuilder()
    {
        Reset();
    }
    public MonsterBuilder SetArmor(int armor)
    {
        entity.armor = armor;
        return this;
    }
}