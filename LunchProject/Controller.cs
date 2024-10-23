using LunchProject.Models;
using LunchProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace LunchProject;

[ApiController]
[Route("spots")]
public class Controller(IAddLunchSpotService addLunchSpotService, IFindLunchSpotService findLunchSpotService)
    : ControllerBase
{
    [HttpPost]
    public async Task<ObjectResult> AddLunchSpot([FromBody] LunchSpot spot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var success = await addLunchSpotService.AddLunchSpot(spot);
        
        if (!success)
        {
            return Conflict($"A lunch spot with the name '{spot.Name}' already exists.");
        }

        return CreatedAtAction(nameof(AddLunchSpot), new { name = spot.Name }, spot);
    }

    [HttpPost("find")]
    public async Task<ObjectResult> FindLunchSpots([FromBody] RequestLunchSpot request)
    {
        var matchingSpots = await findLunchSpotService.FindLunchSpot(request);

        if (matchingSpots.Count <= 0)
        {
            return NotFound("No lunch spots match the provided criteria.");
        }

        return Ok(matchingSpots);
    }
}