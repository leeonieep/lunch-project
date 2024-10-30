using LunchProject.Models;

namespace LunchProject.Repositories;

public interface ILunchSpotRepository
{
    public Task<List<LunchSpot>> Get();
    public Task Add(List<LunchSpot> spots);
}