using LunchProject.Controllers;
using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace LunchProjectTests.Controllers;

public class AddLunchSpotControllerTests
{
    private readonly Mock<IAddLunchSpotService> _addLunchSpotServiceMock;
    private readonly AddLunchSpotController _subjectUnderTest;

    public AddLunchSpotControllerTests()
    {
        _addLunchSpotServiceMock = new Mock<IAddLunchSpotService>();
        _subjectUnderTest = new AddLunchSpotController(_addLunchSpotServiceMock.Object);
    }

    [Fact]
    public async Task AddLunchSpot_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var lunchSpot = new LunchSpot { Name = "Test Spot" };

        _subjectUnderTest.ModelState.AddModelError("Error", "Invalid model state");

        var result = await _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task AddLunchSpot_ShouldReturnConflict_WhenRequestLunchSpotNameAlreadyExists()
    {
        var lunchSpot = new LunchSpot { Name = "Test Spot" };

        _addLunchSpotServiceMock.Setup(s => s.AddLunchSpot(lunchSpot)).ReturnsAsync(false);

        var result = await _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<ConflictObjectResult>();
        result.Value.ShouldBe($"A lunch spot with the name '{lunchSpot.Name}' already exists.");
    }

    [Fact]
    public async Task AddLunchSpot_ShouldReturnResponse_WhenRequestIsSuccessful()
    {
        var lunchSpot = new LunchSpot
        {
            Name = "Test Spot",
            AveragePortionSize = "large",
            PriceRange = "$$",
            MinutesWalkAway = 5,
            SuitableForJanet = true,
            SuitableForLeonie = false,
            SuitableForSahir = true
        };

        _addLunchSpotServiceMock.Setup(s => s.AddLunchSpot(lunchSpot)).ReturnsAsync(true);

        var result = await _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<CreatedAtActionResult>();
        result.Value.ShouldBeEquivalentTo(lunchSpot);
        _addLunchSpotServiceMock.Verify(s => s.AddLunchSpot(lunchSpot), Times.Once);
    }

    [Fact]
    public async Task AddLunchSpot_ShouldReturnException_WhenServiceThrowsException()
    {
        var lunchSpot = new LunchSpot { Name = "Test Spot" };

        _addLunchSpotServiceMock.Setup(s => s.AddLunchSpot(lunchSpot)).ThrowsAsync(new Exception());

        var result = await _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<ObjectResult>();
        result.Value.ShouldBe("An unexpected error occurred.");

    }
}