// Wave is a recursive object that contain
// the current wave and every possible future wave.

public class Wave
{
    public List<Monster> monsters;
    public List<Wave> futureWaves;

    public Wave(List<Monster> monsters, List<Wave> futureWaves)
    {
        this.monsters = monsters;
        this.futureWaves = futureWaves;
    }

    public void addMonster(Monster monster)
    {
        monsters.Add(monster);
    }

    public Wave? GetNextWave()
    {
        return futureWaves.First();
    }
}