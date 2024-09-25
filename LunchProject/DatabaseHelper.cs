using System.Text.Json;
using LunchProject.Models;

namespace LunchProject;

public static class DatabaseHelper
{
    private const string FilePath = "lunchSpotsDatabase.json";
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static async Task<List<LunchSpot>> LoadFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new List<LunchSpot>();
        }

        var jsonString = await File.ReadAllTextAsync(FilePath);
        return JsonSerializer.Deserialize<List<LunchSpot>>(jsonString) ?? new List<LunchSpot>();
    }

    public static async void SaveToFile(List<LunchSpot> spots)
    {
        var jsonString = JsonSerializer.Serialize(spots, JsonOptions);
        await File.WriteAllTextAsync(FilePath, jsonString);
    }
}