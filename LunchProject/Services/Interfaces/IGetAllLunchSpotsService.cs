using LunchProject.Models;

namespace LunchProject.Services.Interfaces;

public interface IGetAllLunchSpotsService
{
    Task<PaginatedResponse> GetAllLunchSpots(int page, int pageSize);
}