using System.Text.Json;

public class JsonLoader
{
    public static Dictionary<string, string> LoadTextJson(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        Dictionary<string, string>? result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
        if (result == null || result.Count == 0)
        {
            Console.WriteLine("Error loading json file: " + filePath);
            Environment.Exit(1);
        }
        return result ?? new Dictionary<string, string>();
    }

    public static Dictionary<string, object> LoadObjectJson(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        Dictionary<string, object>? result = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
        if (result == null || result.Count == 0)
        {
            Console.WriteLine("Error loading json file: " + filePath);
            Environment.Exit(1);
        }
        return ConvertDictionary(result);
    }

    private static Dictionary<string, object> ConvertDictionary(Dictionary<string, object> source)
    {
        Dictionary<string, object> converted = new Dictionary<string, object>();

        foreach (KeyValuePair<string, object> entry in source)
        {
            converted[entry.Key] = ConvertValue(entry.Value) ?? string.Empty;
        }

        return converted;
    }

    private static object? ConvertValue(object? value)
    {
        if (value is JsonElement jsonElement)
        {
            return ConvertJsonElement(jsonElement);
        }

        return value;
    }

    private static object? ConvertJsonElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.Object => ConvertObject(element),
            JsonValueKind.Array => element.EnumerateArray().Select(ConvertJsonElement).ToList(),
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number when element.TryGetInt64(out long longValue) => longValue,
            JsonValueKind.Number => element.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }

    private static Dictionary<string, object> ConvertObject(JsonElement element)
    {
        Dictionary<string, object> converted = new Dictionary<string, object>();

        foreach (JsonProperty property in element.EnumerateObject())
        {
            converted[property.Name] = ConvertJsonElement(property.Value) ?? string.Empty;
        }

        return converted;
    }
}