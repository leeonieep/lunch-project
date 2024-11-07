using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

namespace LunchProject.Services;

public class GetAllLunchSpotsService(ILunchSpotRepository repository) : IGetAllLunchSpotsService
{
    public async Task<List<LunchSpot>> GetAllLunchSpots()
    {
        return await repository.Get();
    }
}