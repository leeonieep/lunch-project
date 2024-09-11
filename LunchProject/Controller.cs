using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace LunchProject;

[ApiController]
[Route("spots")]
public class Controller : ControllerBase
{
    private static readonly string FilePath = "lunchspots.json";
    public static List<LunchSpot> LunchSpots = LoadFromFile();

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


    public class LunchSpot
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\$|\$\$|\$\$\$$", ErrorMessage = "Price range must be $, $$, or $$$")]
        public string PriceRange { get; set; }

        [Required]
        [RegularExpression(@"small|medium|large", ErrorMessage = "Portion size must be 'small', 'medium', or 'large'")]
        public string AveragePortionSize { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minutes walk away must be a positive number")]
        public int MinutesWalkAway { get; set; }

        [Required]
        public bool SuitableForLeonie { get; set; }

        [Required]
        public bool SuitableForSahir { get; set; }
        
        [Required]
        public bool SuitableForJanet { get; set; }
    }

