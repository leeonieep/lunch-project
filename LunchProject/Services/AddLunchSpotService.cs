using LunchProject.Models;
using LunchProject.Repositories;

namespace LunchProject.Services;

public class AddLunchSpotService(ILunchSpotRepository repository) : IAddLunchSpotService
{
    public async void AddLunchSpot(LunchSpot spot)
    {
        var spots = await repository.LoadFromFile();
        
        spots.Add(spot);
        
        //try catch?
        repository.SaveToFile(spots);
    }
}