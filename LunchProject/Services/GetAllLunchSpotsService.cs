using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services.Interfaces;

namespace LunchProject.Services;

public class GetAllLunchSpotsService(ILunchSpotRepository repository) : IGetAllLunchSpotsService
{
    public async Task<PaginatedResponse<LunchSpot>> GetAllLunchSpots(int page, int pageSize)
    {
        var spots = await repository.Get();
        var numberOfSpots = spots.Count;
        
        var paginatedSpots = spots
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResponse<LunchSpot>(paginatedSpots, numberOfSpots, page, pageSize);
    }
}

public class PaginatedResponse<T>(List<T> items, int count, int page, int pageSize)
{
    public int CurrentPage { get; set; } = page;
    public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
    public int PageSize { get; set; } = pageSize;
    public int TotalItems { get; set; } = count;
    public List<T> Items { get; set; } = items;
}