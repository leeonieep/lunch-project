using LunchProject.Models;

namespace LunchProject.Services;

public class AddLunchSpotService(ILunchSpotRepository repository) : IAddLunchSpotService
{
    public async void AddLunchSpot(LunchSpot spot)
    {
        var spots = await repository.LoadFromFile();
        
        spot.Id = Guid.NewGuid().ToString();
        
        spots.Add(spot);
        
        //try catch?
        repository.SaveToFile(spots);
    }
}