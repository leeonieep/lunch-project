using LunchProject.Models;

namespace LunchProject.Services;

public interface IFindLunchSpotService
{
    List<LunchSpot> FindLunchSpot(RequestLunchSpot request);
}