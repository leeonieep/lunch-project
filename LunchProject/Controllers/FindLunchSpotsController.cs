using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject.Controllers;

[ApiController]
[Route("lunchSpot/find")]
public class FindLunchSpotsController(IFindLunchSpotService findLunchSpotService) : ControllerBase
{
    [SwaggerOperation(Summary = "Finds lunch spots that match the provided criteria")]
    [HttpPost]
    public async Task<ObjectResult> FindLunchSpots([FromBody] RequestLunchSpot request)
    {
        try
        {
            var matchingSpots = await findLunchSpotService.FindLunchSpot(request);

            if (matchingSpots.Count <= 0)
            {
                return NotFound("No lunch spots match the provided criteria.");
            }

            return Ok(matchingSpots);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}