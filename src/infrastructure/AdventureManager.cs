using System.Collections;

public sealed class AdventureManager
{
	public static AdventureManager Instance { get; } = new AdventureManager();

	private readonly Dictionary<string, object> adventureData;

	private AdventureManager()
	{
		adventureData = JsonLoader.LoadObjectJson("data/adventure.json");
	}

	public Wave LoadAdventure()
	{
		return BuildWave(adventureData);
	}

	private static Wave BuildWave(Dictionary<string, object> waveData)
	{
		string openingText = GetString(waveData, "openingText");
		string victoryText = GetString(waveData, "victoryText");
		List<Monster> monsters = BuildMonsters(waveData);
		List<Wave> futureWaves = BuildFutureWaves(waveData);

		return new Wave(monsters, futureWaves, openingText, victoryText);
	}

	private static List<Monster> BuildMonsters(Dictionary<string, object> waveData)
	{
		object monstersValue = GetRequiredValue(waveData, "monsters");

		if (monstersValue is not IEnumerable monstersList)
		{
			throw new Exception("The 'monsters' field must be an array in adventure.json.");
		}

		List<Monster> monsters = new List<Monster>();

		foreach (object? monsterValue in monstersList)
		{
			if (monsterValue is not Dictionary<string, object> monsterData)
			{
				throw new Exception("Each monster entry must be a JSON object.");
			}

			monsters.Add(BuildMonster(monsterData));
		}

		return monsters;
	}

	private static List<Wave> BuildFutureWaves(Dictionary<string, object> waveData)
	{
		object futureWavesValue = GetRequiredValue(waveData, "futureWaves");

		if (futureWavesValue is not IEnumerable futureWavesList)
		{
			throw new Exception("The 'futureWaves' field must be an array in adventure.json.");
		}

		List<Wave> futureWaves = new List<Wave>();

		foreach (object? futureWaveValue in futureWavesList)
		{
			if (futureWaveValue is not Dictionary<string, object> futureWaveData)
			{
				throw new Exception("Each future wave entry must be a JSON object.");
			}

			futureWaves.Add(BuildWave(futureWaveData));
		}

		return futureWaves;
	}

	private static Monster BuildMonster(Dictionary<string, object> monsterData)
	{
		return new MonsterBuilder()
			.SetName(GetString(monsterData, "name"))
			.SetHealth(GetInt(monsterData, "health"))
			.SetPower(GetInt(monsterData, "power"))
			.SetArmor(GetInt(monsterData, "armor"))
			.SetStandardAttack()
			.GetResult();
	}

	private static object GetRequiredValue(Dictionary<string, object> data, string key)
	{
		if (data.TryGetValue(key, out object? value) && value != null)
		{
			return value;
		}

		throw new Exception($"Missing required key '{key}' in adventure.json.");
	}

	private static string GetString(Dictionary<string, object> data, string key)
	{
		object value = GetRequiredValue(data, key);

		if (value is string stringValue)
		{
			return stringValue;
		}

		throw new Exception($"The '{key}' field must be a string in adventure.json.");
	}

	private static int GetInt(Dictionary<string, object> data, string key)
	{
		object value = GetRequiredValue(data, key);

		return value switch
		{
			int intValue => intValue,
			long longValue => (int)longValue,
			double doubleValue => (int)doubleValue,
			_ => throw new Exception($"The '{key}' field must be a number in adventure.json.")
		};
	}
}
