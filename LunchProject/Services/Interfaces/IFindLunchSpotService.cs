using LunchProject.Models;

namespace LunchProject.Services.Interfaces;

public interface IFindLunchSpotService
{
    Task<List<LunchSpot>> FindLunchSpot(RequestLunchSpot request);
}