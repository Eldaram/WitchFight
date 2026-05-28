public class GameEngine : IGameEngine
{
    private static Dictionary<string, Func<string, Player>> classFactory = new Dictionary<string, Func<string, Player>>()
    {
        { "warrior", (name) => ClassBuilder.CreateWarrior(name) },
        { "mage", (name) => ClassBuilder.CreateMage(name) },
        { "rogue", (name) => ClassBuilder.CreateRogue(name) }
    };

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
    public TerminalManager terminal = TerminalManager.Instance;

    public GameEngine(Wave wave)
    {
        terminal.PrintText("welcomeMessage");

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
        nextTurns = new EndingWave(this);
        playerLost = isPlayerDead;
    }

    public void InitGame()
    {
        playerLost = false;
        PlayerConfiguration();
        currentWave = initialWave;

        nextTurns = new BeginWave(this, currentWave);
    }

    private void PlayerConfiguration()
    {
        string name = terminal.AskString("namePrompt");
        int classChoice = terminal.AskOption("classPrompt", classFactory.Keys.ToList());
        player = classFactory.Values.ElementAt(classChoice)(name);
    }
}