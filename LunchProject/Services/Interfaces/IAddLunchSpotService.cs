using LunchProject.Models;

namespace LunchProject.Services.Interfaces;

public interface IAddLunchSpotService
{
    Task<bool> AddLunchSpot(LunchSpot requestSpot);
}