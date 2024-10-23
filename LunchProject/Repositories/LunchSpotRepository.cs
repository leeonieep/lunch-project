using System.Text.Json;
using LunchProject.Models;

namespace LunchProject.Repositories;

public class LunchSpotRepository : ILunchSpotRepository
{
    private const string FilePath = "Repositories/lunchSpotsDatabase.json";
    
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public async Task<List<LunchSpot>> Get()
    {
        if (!File.Exists(FilePath))
        {
            return new List<LunchSpot>();
        }

        var jsonString = await File.ReadAllTextAsync(FilePath);
        return JsonSerializer.Deserialize<List<LunchSpot>>(jsonString) ?? new List<LunchSpot>();
    }

    public async void Add(List<LunchSpot> spots)
    {
        var jsonString = JsonSerializer.Serialize(spots, JsonOptions);
        await File.WriteAllTextAsync(FilePath, jsonString);
    }
}