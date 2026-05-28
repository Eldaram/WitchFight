public sealed class TerminalManager
{
    public static TerminalManager Instance { get; } = new TerminalManager();
    private Dictionary<string, string> texts;

    private TerminalManager()
    {
        texts = JsonLoader.LoadTextJson("data/texts.json");
    }

    public string GetText(string key)
    {
        if (texts.TryGetValue(key, out string? value))
        {
            return value;
        }
        else
        {
            PrintLine($"Error: Text key '{key}' not found in texts.json");
            Environment.Exit(1);
            return "";
        }
    }

    public void PrintEntity(Entity entity)
    {
        string nomination;
        if (entity is Player)
            nomination = $"{entity.name} the {GetText(((Player)entity).playerClass ?? "unknownClass")}";
        else
            nomination = entity.name;
        
        PrintLine($"{nomination} {GetText("health")}: {entity.health}/{entity.maxHealth}");
    }

    public void PrintEntityList(IEnumerable<Entity> entities)
    {
        int index = 1;

        foreach (var entity in entities)
        {
            Print($"[{index}] ");
            PrintEntity(entity);
            index++;
        }
    }

    public void PrintMove(Move move)
    {
        string cooldownText = move.cooldown > 0 ? $" ({GetText("cooldown")}: {move.cooldown})" : "";
        string usesText = move.maxUses >= 0 ? $" ({GetText("remainingUses")}: {move.maxUses})" : "";
        PrintLine($"{GetText(move.name)} {cooldownText} {usesText}");
    }

    public void PrintMoveList(IEnumerable<Move> moves)
    {
        int index = 1;

        foreach (var move in moves)
        {
            Print($"[{index}] ");
            PrintMove(move);
            index++;
        }
    }

    public void Print(string message)
    {
        Console.Write(message);
    }

    public void PrintLine(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintText(string key)
    {
        PrintLine(GetText(key));
    }

    public Move AskMove(List<Move> moves)
    {
        PrintText("chooseMove");
        PrintMoveList(moves);

        int index = GetNumberBetween(1, moves.Count) - 1;
        return moves[index];
    }

    public Entity AskTarget(List<Entity> targets)
    {
        PrintText("chooseTarget");
        PrintEntityList(targets);

        int index = GetNumberBetween(1, targets.Count) - 1;
        return targets[index];
    }

    public int AskOption(string question, List<string> options)
    {
        PrintLine(GetText(question) + ":");
        for (int i = 0; i < options.Count; i++)
        {
            PrintLine($"{i + 1}. {GetText(options[i])}");
        }
        int choice = GetNumberBetween(1, options.Count) - 1;
        return choice;
    }

    public int GetNumberBetween(int min, int max)
    {
        while (true)
        {
            string answer = ReadLine().Trim();
            if (int.TryParse(answer, out int number) && number >= min && number <= max)
                return number;
            else
                PrintLine(GetText("invalidNumber"));
        }
    }

    public bool AskYesNo(string question)
    {
        Print(GetText(question) + " (y/n): ");
        string answer = ReadLine().Trim().ToLower();
        return answer == "y" || answer == "yes";
    }

    public string AskString(string question)
    {
        Print(GetText(question) + ":");
        return ReadLine();
    }

    public string ReadLine()
    {
        string input = Console.ReadLine() ?? "";
        PrintLine("");
        return input;
    }
}