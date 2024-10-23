using LunchProject.Models;
using LunchProject.Repositories;

namespace LunchProject.Services;

public class AddLunchSpotService(ILunchSpotRepository repository) : IAddLunchSpotService
{
    public async Task<bool> AddLunchSpot(LunchSpot spot)
    {
        var allLunchSpots = await repository.LoadFromFile();

        if (allLunchSpots.Any(lunchSpot => spot.Name == lunchSpot.Name))
        {
            return false;
        }

        allLunchSpots.Add(spot);
        repository.SaveToFile(allLunchSpots);

        return true;
    }
}