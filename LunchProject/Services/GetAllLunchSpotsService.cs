using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

namespace LunchProject.Services;

public class GetAllLunchSpotsService(ILunchSpotRepository repository) : IGetAllLunchSpotsService
{
    public async Task<PaginatedResponse> GetAllLunchSpots(int page, int pageSize)
    {
        var existingSpots = await repository.Get();
        
        var numberOfSpots = existingSpots.Count;
        
        var totalPages = (int)Math.Ceiling(numberOfSpots / (double)pageSize);

        if (page > totalPages)
        {
            page = totalPages;
        }
        
        var paginatedSpots = existingSpots
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return new PaginatedResponse
        {
            Data = paginatedSpots,
            CurrentPage = page,
            TotalPages = totalPages,
            PageSize = pageSize,
            TotalItems = numberOfSpots
        };
    }
}
