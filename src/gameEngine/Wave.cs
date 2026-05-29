public class Wave
{
    public List<Monster> monsters;
    public List<Wave> futureWaves;
    public string openingText = "";
    public string victoryText = "";

    public Wave(List<Monster> monsters, List<Wave> futureWaves, string openingText = "", string victoryText = "")
    {
        this.monsters = monsters;
        this.futureWaves = futureWaves;
        this.openingText = openingText;
        this.victoryText = victoryText;
    }

    public Wave? GetNextWave()
    {
        return futureWaves.FirstOrDefault() ?? null;
    }
}