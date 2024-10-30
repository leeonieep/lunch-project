using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

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
        await repository.Add(existingSpots);

        return true;
    }
}