public class BeginWave : IGameEngine
{
    public GameEngine gameEngine;
    public Wave wave;

    public BeginWave(GameEngine gameEngine, Wave wave)
    {
        this.gameEngine = gameEngine;
        this.wave = wave;
    }

    public void RunTurn()
    {
        // Add a line to launch the begining of the wave

        gameEngine.monsters = wave.monsters;
        gameEngine.currentMonsterIndex = 0;
        gameEngine.turnIndex = 0;
        gameEngine.waveNumber++;
    }

    public void SetNextTurns()
    {
        gameEngine.nextTurns = new Turn(gameEngine, gameEngine.player);
    }
}