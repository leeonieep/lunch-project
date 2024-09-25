using LunchProject.Models;

namespace LunchProject.Services;

public class FindLunchSpotService : IFindLunchSpotService
{
    public List<LunchSpot> FindLunchSpot(RequestLunchSpot request)
    {
        var results = new List<LunchSpot>();   
      

        // // Load all lunch spots from the JSON "database"
        // var allLunchSpots = _lunchSpotService.GetAllLunchSpots();
        //
        // // public List<LunchSpot> GetAllSpots()
        // // {
        // //     return LoadFromFile();
        // // }
        //
        // // Filter based on the input criteria
        // var matchingSpots = allLunchSpots.Where(spot =>
        //     spot.PriceRange == input.PriceRange &&
        //     spot.AveragePortionSize == input.AveragePortionSize &&
        //     spot.MinutesWalkAway <= input.MinutesWalkAway &&
        //     spot.SuitableForLeonie == input.SuitableForLeonie &&
        //     spot.SuitableForSahir == input.SuitableForSahir &&
        //     spot.SuitableForJanet == input.SuitableForJanet).ToList();
        //
        // // If no spots match, return a 404 response
        // if (!matchingSpots.Any())
        // {
        //     return NotFound("No lunch spots match the provided criteria.");
        // }
        //
        // // Return the matching spots
      //  return Ok(matchingSpots);
      return results;
    }
}