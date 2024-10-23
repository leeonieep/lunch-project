using LunchProject.Repositories;

namespace LunchProject.Services;

public class DeleteLunchSpotService(ILunchSpotRepository repository) : IDeleteLunchSpotService
{
   public async Task<bool> DeleteLunchSpot()
   {
      var existingSpots = await repository.Get();
      
      // match spot name ? delete : return false

      return true;
   }
}