// A turn can be either 
// An entity's turn to act
// The intialisation
// The end of a wave

public class Turn : IGameEngine
{
    public TerminalManager terminal = TerminalManager.Instance;
    public GameEngine gameEngine;
    public Entity entity;

    public Turn(GameEngine gameEngine,Entity entity)
    {
        this.gameEngine = gameEngine;
        this.entity = entity;
    }

    public void RunTurn()
    {
        Entity? killedEntity;

        if (entity is Player)
        {
            PrintGameState();
            ((Player)entity).UpdateCooldowns();
            killedEntity = entity.ChooseAction(gameEngine.monsters);
        }
        else
            killedEntity = entity.ChooseAction(new List<Entity> { gameEngine.player });

        if (killedEntity != null)
            handleEntityDeath(killedEntity);
    }

    private void handleEntityDeath(Entity entity)
    {
        if (entity is Player)
        {
            gameEngine.EndWave(true);
        }
        else
        {
            gameEngine.monsters.Remove((Monster)entity);
            if (gameEngine.monsters.Count == 0)
                gameEngine.EndWave(false);
        }
    }

    public void SetNextTurns()
    {
        gameEngine.turnIndex++;

        if (gameEngine.currentMonsterIndex < gameEngine.monsters.Count)
        {
            gameEngine.nextTurns = new Turn(gameEngine, gameEngine.monsters[gameEngine.currentMonsterIndex]);
            gameEngine.currentMonsterIndex++;
        }
        else
        {
            gameEngine.nextTurns = new Turn(gameEngine, gameEngine.player);
            gameEngine.currentMonsterIndex = 0;
        }
    }

    private void PrintGameState()
    {
        terminal.PrintText("sectionSeparator");
        terminal.PrintLine(terminal.GetText("wave") + $"{gameEngine.waveNumber}");
        terminal.PrintText("sectionSeparator");

        terminal.PrintEntity(gameEngine.player);
        terminal.PrintText("enemies");
        terminal.PrintEntityList(gameEngine.monsters);
    }
    
}
