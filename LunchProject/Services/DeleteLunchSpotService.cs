using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

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