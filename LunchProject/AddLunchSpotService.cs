using System.Text.Json;

namespace LunchProject;

public class AddLunchSpotService : IAddLunchSpotService
{
    private const string FilePath = "lunchSpotsDatabase.json";
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    // public List<LunchSpot> GetAllSpots()
    // {
    //     return LoadFromFile();
    // }

    public void AddLunchSpot(LunchSpot spot)
    {
        var spots = LoadFromFile();
        spot.Id = Guid.NewGuid().ToString();
        spots.Add(spot);
        SaveToFile(spots);
    }

    private static void SaveToFile(List<LunchSpot> spots)
    {
        var jsonString = JsonSerializer.Serialize(spots, JsonOptions);
        File.WriteAllText(FilePath, jsonString);
    }

    private static List<LunchSpot> LoadFromFile()
    {
        if (!File.Exists(FilePath))
        {
            return new List<LunchSpot>();
        }

        var jsonString = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<LunchSpot>>(jsonString) ?? new List<LunchSpot>();
    }
}