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
            AskRestart("playerDefeated");
        else
        {
            gameEngine.terminal.PrintLine(gameEngine.currentWave.victoryText);
            Wave? nextWave = gameEngine.currentWave.GetNextWave();
            if (nextWave == null)
            {
                AskRestart("gameCompleted");
                return;
            }
            gameEngine.currentWave = nextWave;
            gameEngine.nextTurns = new BeginWave(gameEngine, gameEngine.currentWave);
        }
    }

    private void AskRestart(string messageKey)
    {
        bool playAgain = gameEngine.terminal.AskYesNo(messageKey);
        if (playAgain)
            gameEngine.InitGame();
        else
            Environment.Exit(0);
    }
}