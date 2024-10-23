using LunchProject.Models;

namespace LunchProject.Repositories;

public interface ILunchSpotRepository
{
    public Task<List<LunchSpot>> Get();
    public void Add(List<LunchSpot> spots);
}