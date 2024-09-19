using Microsoft.AspNetCore.Mvc;

namespace LunchProject;

[ApiController]
[Route("spots")]
public class Controller : ControllerBase
{
    private readonly AddLunchSpotService _lunchSpotService = new();

    [HttpPost]
    public IActionResult AddLunchSpot([FromBody] LunchSpot spot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _lunchSpotService.AddLunchSpot(spot);

        return CreatedAtAction(nameof(AddLunchSpot), new { id = spot.Id }, spot);
    }
}