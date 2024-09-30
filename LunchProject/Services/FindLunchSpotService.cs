using LunchProject.Models;

namespace LunchProject.Services;

public class FindLunchSpotService(ILunchSpotRepository repository) : IFindLunchSpotService
{
    public async Task<List<LunchSpot>> FindLunchSpot(RequestLunchSpot request)
    {
        var allLunchSpots = await repository.LoadFromFile();

        var matchingSpots = allLunchSpots.Where(spot =>
            spot.PriceRange == request.PriceRange ||
            spot.AveragePortionSize == request.AveragePortionSize ||
            spot.MinutesWalkAway <= request.MinutesWalkAway ||
            spot.SuitableForLeonie == request.SuitableForLeonie ||
            spot.SuitableForSahir == request.SuitableForSahir ||
            spot.SuitableForJanet == request.SuitableForJanet).ToList();

        //TODO basic validation of matching, improve this


        return matchingSpots;
    }
}