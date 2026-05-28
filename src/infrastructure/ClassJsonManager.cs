public sealed class ClassJsonManager
{
    public static ClassJsonManager Instance { get; } = new ClassJsonManager();
    private readonly Dictionary<string, object> classDescriptions;

    public Dictionary<string, object> ClassDescriptions => classDescriptions;

    private ClassJsonManager()
    {
        classDescriptions = JsonLoader.LoadObjectJson("data/class.json");
    }
    
    public double GetDouble(string classKey, string key)
    {
        if (ClassDescriptions.TryGetValue(classKey, out object? classDataObj) && classDataObj is Dictionary<string, object> classData)
        {
            if (classData.TryGetValue(key, out object? valueObj))
            {
                if (valueObj is double doubleValue)
                    return doubleValue;
                else if (valueObj is long longValue)
                    return longValue;
                else if (valueObj is int intValue)
                    return intValue;
            }
        }
        throw new Exception($"Failed to extract {key} for class {classKey}");
    }

    public int GetInt(string classKey, string key)
    {
        if (ClassDescriptions.TryGetValue(classKey, out object? classDataObj) && classDataObj is Dictionary<string, object> classData)
        {
            if (classData.TryGetValue(key, out object? valueObj))
            {
                if (valueObj is long longValue)
                    return (int)longValue;
                else if (valueObj is double doubleValue)
                    return (int)doubleValue;
                else if (valueObj is int intValue)
                    return intValue;
            }
        }
        throw new Exception($"Failed to extract {key} for class {classKey}");
    }

    public string GetString(string classKey, string key)
    {
        if (ClassDescriptions.TryGetValue(classKey, out object? classDataObj) && classDataObj is Dictionary<string, object> classData)
        {
            if (classData.TryGetValue(key, out object? valueObj) && valueObj is string stringValue)
            {
                return stringValue;
            }
        }
        throw new Exception($"Failed to extract {key} for class {classKey}");
    }
}