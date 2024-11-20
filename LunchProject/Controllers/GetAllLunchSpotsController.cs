using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject.Controllers;

[ApiController]
[Route("lunchSpot")]
public class GetAllLunchSpotsController(IGetAllLunchSpotsService getAllLunchSpotsService) : ControllerBase
{
    [SwaggerOperation(Summary = "Retrieves all lunch spots")]
    [HttpGet]
    public async Task<ObjectResult> GetAllLunchSpots(int page = 1, int pageSize = 10)
    {
        try
        {
            var spots = await getAllLunchSpotsService.GetAllLunchSpots(page, pageSize);

            return Ok(spots);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}