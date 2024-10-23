using LunchProject.Models;

namespace LunchProject.Services;

public interface IAddLunchSpotService
{
    Task<bool> AddLunchSpot(LunchSpot spot);
}