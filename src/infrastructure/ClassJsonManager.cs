using System;
using System.Collections.Generic;
using System.Globalization;

public sealed class ClassJsonManager
{
    public static ClassJsonManager Instance { get; } = new ClassJsonManager();
    private readonly Dictionary<string, object> classDescriptions;

    public Dictionary<string, object> ClassDescriptions => classDescriptions;

    private ClassJsonManager()
    {
        classDescriptions = JsonLoader.LoadObjectJson("data/class.json");
    }

    private bool TryGetClassData(string classKey, out Dictionary<string, object>? classData)
    {
        if (classDescriptions.TryGetValue(classKey, out object? classDataObj) && classDataObj is Dictionary<string, object> dict)
        {
            classData = dict;
            return true;
        }

        classData = null;
        return false;
    }

    //This function is long because we want to reunite all type conversion logic in one place
    private T GetValue<T>(string classKey, string key)
    {
        if (!TryGetClassData(classKey, out var classData))
            ThrowError(classKey, key);
        if (!classData!.TryGetValue(key, out object? valueObj))
            ThrowError(classKey, key);
        if (valueObj is T t)
            return t;

        try
        {
            if (typeof(T) == typeof(double))
            {
                double v = Convert.ToDouble(valueObj, CultureInfo.InvariantCulture);
                return (T)(object)v;
            }
            if (typeof(T) == typeof(int))
            {
                int v = Convert.ToInt32(valueObj, CultureInfo.InvariantCulture);
                return (T)(object)v;
            }
            if (typeof(T) == typeof(string))
            {
                string v = Convert.ToString(valueObj, CultureInfo.InvariantCulture) ?? string.Empty;
                return (T)(object)v;
            }
            if (valueObj is IConvertible)
            {
                var converted = Convert.ChangeType(valueObj, typeof(T), CultureInfo.InvariantCulture);
                return (T)converted!;
            }
        }
        catch {}

        ThrowError(classKey, key);
        return default!;
    }

    private void ThrowError(string classKey, string key)
    {
        throw new Exception($"Failed to extract {key} for class {classKey}");
    }

    public double GetDouble(string classKey, string key) => GetValue<double>(classKey, key);

    public int GetInt(string classKey, string key) => GetValue<int>(classKey, key);

    public string GetString(string classKey, string key) => GetValue<string>(classKey, key);
}