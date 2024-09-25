using LunchProject.Models;

namespace LunchProject.Services;

public class AddLunchSpotService : IAddLunchSpotService
{
    public async void AddLunchSpot(LunchSpot spot)
    {
        var spots = await DatabaseHelper.LoadFromFile();
        
        spot.Id = Guid.NewGuid().ToString();
        
        spots.Add(spot);
        
        DatabaseHelper.SaveToFile(spots);
    }
}