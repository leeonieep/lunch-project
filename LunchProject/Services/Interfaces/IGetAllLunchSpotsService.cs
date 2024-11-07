using LunchProject.Models;

namespace LunchProject.Services.Interfaces;

public interface IGetAllLunchSpotsService
{
    Task<List<LunchSpot>> GetAllLunchSpots();
}