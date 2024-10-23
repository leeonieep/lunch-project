using LunchProject.Models;

namespace LunchProject.Repositories;

public interface ILunchSpotRepository
{
    public Task<List<LunchSpot>> LoadFromFile();
    public void SaveToFile(List<LunchSpot> spots);
}