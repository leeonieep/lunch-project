using LunchProject.Models;
using LunchProject.Repositories;

namespace LunchProject.Services;

public class AddLunchSpotService(ILunchSpotRepository repository) : IAddLunchSpotService
{
    public async Task<bool> AddLunchSpot(LunchSpot requestSpot)
    {
        var existingSpots = await repository.Get();

        if (existingSpots.Any(spot => requestSpot.Name == spot.Name))
        {
            return false;
        }

        existingSpots.Add(requestSpot);
        repository.Add(existingSpots);

        return true;
    }
}