using System.ComponentModel.DataAnnotations;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject.Controllers;

[ApiController]
public class DeleteLunchSpotController(IDeleteLunchSpotService deleteLunchSpotService) : ControllerBase
{
    [SwaggerOperation(Summary = "Deletes a lunch spot")]
    [HttpDelete("lunchSpot/delete/{name?}")]
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
}