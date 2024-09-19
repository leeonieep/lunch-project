using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LunchProject;

[ApiController]
[Route("spots")]
public class Controller : ControllerBase
{
    private const string FilePath = "lunchSpotsDatabase.json";
    private static readonly List<LunchSpot> LunchSpots = LoadFromFile();

    [HttpPost]
    public IActionResult AddLunchSpot([FromBody] LunchSpot spot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        spot.Id = Guid.NewGuid().ToString();
        LunchSpots.Add(spot);
        
        SaveToFile(LunchSpots);

        return CreatedAtAction(nameof(AddLunchSpot), new { id = spot.Id }, spot);
}
    
    private static void SaveToFile(List<LunchSpot> spots)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(spots, options);
        System.IO.File.WriteAllText(FilePath, jsonString);
    }

    private static List<LunchSpot> LoadFromFile()
    {
        if (!System.IO.File.Exists(FilePath))
        {
            return new List<LunchSpot>();
        }
        
        var jsonString = System.IO.File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<LunchSpot>>(jsonString) ?? new List<LunchSpot>();
    }
}

