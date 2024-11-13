using System.ComponentModel.DataAnnotations;
using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject;

[ApiController]
[Route("lunchSpot")]
public class Controller(
    IAddLunchSpotService addLunchSpotService,
    IFindLunchSpotService findLunchSpotService,
    IDeleteLunchSpotService deleteLunchSpotService,
    IGetAllLunchSpotsService getAllLunchSpotsService) : ControllerBase
{
    [SwaggerOperation(Summary = "Adds a new lunch spot")]
    [HttpPost("add")]
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
    
    [SwaggerOperation(Summary = "Finds lunch spots that match the provided criteria")]
    [HttpPost("find")]
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
    
    [SwaggerOperation(Summary = "Deletes a lunch spot")]
    [HttpDelete("delete/{name?}")]
    public async Task<ObjectResult> DeleteLunchSpot(
        [FromRoute] [Required(ErrorMessage = "No Lunch Spot Name Provided")]
        string name)
    {
        try
        {
            var success = await deleteLunchSpotService.DeleteLunchSpot(name);

            if (!success)
            {
                return NotFound($"No lunch spot with the name '{name}' exists.");
            }

            return new OkObjectResult("Lunch spot deleted successfully.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
    
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