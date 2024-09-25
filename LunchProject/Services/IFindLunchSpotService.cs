using LunchProject.Models;

namespace LunchProject.Services;

public interface IFindLunchSpotService
{
    Task<List<LunchSpot>> FindLunchSpot(RequestLunchSpot request);
}