using LunchProject.Models;

namespace LunchProject.Services.Interfaces;

public interface IGetAllLunchSpotsService
{
    Task<PaginatedResponse<LunchSpot>> GetAllLunchSpots(int page, int pageSize);
}