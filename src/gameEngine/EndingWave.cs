public class EndingWave : IGameEngine
{
    public GameEngine gameEngine;

    public EndingWave(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
    }

    public void RunTurn()
    {
        return;
    }

    public void SetNextTurns()
    {
        if(gameEngine.playerLost)
        {
            //ask if he wants to leave or restart the game
            gameEngine.InitGame();
        }
        else
        {
            // if the player had won, read the winning line
            Wave? nextWave = gameEngine.currentWave.GetNextWave();
            if (nextWave == null)
            {
                // ask the player if he wants to restart the game
                gameEngine.InitGame();
                return;
            }
            gameEngine.currentWave = nextWave;
            gameEngine.nextTurns = new BeginWave(gameEngine, gameEngine.currentWave);
        }
    }
}