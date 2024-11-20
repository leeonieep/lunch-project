using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject.Controllers;

[ApiController]
[Route("lunchSpot/add")]
public class AddLunchSpotController(IAddLunchSpotService addLunchSpotService) : ControllerBase
{
    [SwaggerOperation(Summary = "Adds a new lunch spot")]
    [HttpPost]
    public async Task<ObjectResult> AddLunchSpot([FromBody] LunchSpot requestSpot)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var success = await addLunchSpotService.AddLunchSpot(requestSpot);

            if (!success)
            {
                return Conflict($"A lunch spot with the name '{requestSpot.Name}' already exists.");
            }

            return CreatedAtAction(nameof(AddLunchSpot), new { name = requestSpot.Name }, requestSpot);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}