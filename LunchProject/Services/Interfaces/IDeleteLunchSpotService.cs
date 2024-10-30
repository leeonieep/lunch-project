namespace LunchProject.Services.Interfaces;

public interface IDeleteLunchSpotService
{
    Task<bool> DeleteLunchSpot(string requestName);
}