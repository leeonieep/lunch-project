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
    public IActionResult AddLunchSpot([FromBody] LunchSpot spot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        addLunchSpotService.AddLunchSpot(spot);

        return CreatedAtAction(nameof(AddLunchSpot), new { id = spot.Id }, spot);
    }

    [HttpPost("find")]
    public async Task<IActionResult>  FindLunchSpots([FromBody] RequestLunchSpot request)
    {
        var matchingSpots = await findLunchSpotService.FindLunchSpot(request);

        if (matchingSpots.Count <= 0)
        {
            return NotFound("No lunch spots match the provided criteria.");
        }

        return Ok(matchingSpots);
    }
}