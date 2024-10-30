using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

namespace LunchProject.Services;

public class DeleteLunchSpotService(ILunchSpotRepository repository) : IDeleteLunchSpotService
{
   public async Task<bool> DeleteLunchSpot(string requestName)
   {
      var existingSpots = await repository.Get();

      var removedCount = existingSpots.RemoveAll(x => x.Name == requestName);

      if (removedCount == 0)
      {
         return false;
      }

      await repository.Add(existingSpots);

      return true;
   }
}