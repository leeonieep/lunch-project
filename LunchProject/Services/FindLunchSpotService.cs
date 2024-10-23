using LunchProject.Models;
using LunchProject.Repositories;

namespace LunchProject.Services;

public class FindLunchSpotService(ILunchSpotRepository repository) : IFindLunchSpotService
{
    public async Task<List<LunchSpot>> FindLunchSpot(RequestLunchSpot request)
    {
        var allLunchSpots = await repository.LoadFromFile();
        
        var matchingSpots = new List<LunchSpot>();
        
        foreach (var spot in allLunchSpots)
        {
            var match = true;
            
            if (!string.IsNullOrEmpty(request.PriceRange))
            {
                if (spot.PriceRange != request.PriceRange)
                {
                    continue; 
                }
            }
            
            if (!string.IsNullOrEmpty(request.AveragePortionSize))
            {
                if (spot.AveragePortionSize != request.AveragePortionSize)
                {
                    continue;
                }
            }
            
            if (request.MinutesWalkAway.HasValue)
            {
                if (spot.MinutesWalkAway > request.MinutesWalkAway)
                {
                    continue;
                }
            }
            
            if (request.SuitableForLeonie.HasValue)
            {
                if (spot.SuitableForLeonie != request.SuitableForLeonie)
                {
                    continue;
                }
            }
            
            if (request.SuitableForSahir.HasValue)
            {
                if (spot.SuitableForSahir != request.SuitableForSahir)
                {
                    continue;
                }
            }
            
            if (request.SuitableForJanet.HasValue)
            {
                if (spot.SuitableForJanet != request.SuitableForJanet)
                {
                    continue;
                }
            }
            
            if (match)
            {
                matchingSpots.Add(spot);
            }
        }
        
        return matchingSpots;
    }
}