public class GameEngine : IGameEngine
{
    public IGameEngine currentTurn;
    public IGameEngine nextTurns = null!; 
    public List<Monster> monsters;
    public Wave initialWave;
    public Wave currentWave;
    public int currentMonsterIndex = 0;
    public Player player;
    public int turnIndex = 0;
    public int waveNumber = 0;
    public bool playerLost = false;
    public GameEngine(Wave wave)
    {
        initialWave = wave;
        InitGame();
        currentTurn = nextTurns;

        while (true)
        {
            SetNextTurns();
            RunTurn();
            currentTurn = nextTurns;
        }
    }

    public void RunTurn()
    {
        currentTurn.RunTurn();
    }

    public void SetNextTurns()
    {
        currentTurn.SetNextTurns();
    }

    public void EndWave(bool isPlayerDead)
    {
        currentTurn = new EndingWave(this);
        playerLost = isPlayerDead;
    }

    public void InitGame()
    {
        //Launch the dialog for the game
        playerLost = false;
        player = new Player(); //Init a new player here via builder... The builder should ask the user for name and class.
        currentWave = initialWave;

        nextTurns = new BeginWave(this, currentWave);
    }
}