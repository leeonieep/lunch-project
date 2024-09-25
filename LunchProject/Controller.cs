using Microsoft.AspNetCore.Mvc;

namespace LunchProject;

[ApiController]
[Route("spots")]
public class Controller(IAddLunchSpotService lunchSpotService) : ControllerBase
{
    [HttpPost]
    public IActionResult AddLunchSpot([FromBody] LunchSpot spot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        lunchSpotService.AddLunchSpot(spot);

        return CreatedAtAction(nameof(AddLunchSpot), new { id = spot.Id }, spot);
    }
}