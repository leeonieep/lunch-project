using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Controller = LunchProject.Controller;

namespace LunchProjectTests;

public class ControllerTests
{
    private readonly Mock<IAddLunchSpotService> _addLunchSpotServiceMock;
    private readonly Mock<IFindLunchSpotService> _findLunchSpotServiceMock;
    private readonly Mock<IDeleteLunchSpotService> _deleteLunchSpotServiceMock;
    private readonly Controller _subjectUnderTest;

    public ControllerTests()
    {
        _addLunchSpotServiceMock = new Mock<IAddLunchSpotService>();
        _findLunchSpotServiceMock = new Mock<IFindLunchSpotService>();
        _deleteLunchSpotServiceMock = new Mock<IDeleteLunchSpotService>();
        _subjectUnderTest = new Controller(_addLunchSpotServiceMock.Object, _findLunchSpotServiceMock.Object,
            _deleteLunchSpotServiceMock.Object);
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

    [Fact]
    public async Task FindLunchSpots_ShouldNotFound_WhenNoMatchingLunchSpotsAreFound()
    {
        var lunchSpot = new RequestLunchSpot
        {
            AveragePortionSize = "large",
            PriceRange = "$$",
            MinutesWalkAway = 5,
            SuitableForJanet = true,
            SuitableForLeonie = false,
            SuitableForSahir = true
        };

        _findLunchSpotServiceMock.Setup(s => s.FindLunchSpot(lunchSpot)).ReturnsAsync([]);

        var result = await _subjectUnderTest.FindLunchSpots(lunchSpot);

        result.ShouldBeOfType<NotFoundObjectResult>();
        result.Value.ShouldBe("No lunch spots match the provided criteria.");
    }

    [Fact]
    public async Task FindLunchSpots_ShouldReturnResponse_WhenRequestIsSuccessful()
    {
        var lunchSpot = new RequestLunchSpot
        {
            AveragePortionSize = "large",
            PriceRange = "$$",
            MinutesWalkAway = 5,
            SuitableForJanet = true,
            SuitableForLeonie = false,
            SuitableForSahir = true
        };

        _findLunchSpotServiceMock.Setup(s => s.FindLunchSpot(lunchSpot))
            .ReturnsAsync([new LunchSpot { Name = "Test Spot" }]);

        var result = await _subjectUnderTest.FindLunchSpots(lunchSpot);

        result.ShouldBeOfType<OkObjectResult>();
        _findLunchSpotServiceMock.Verify(s => s.FindLunchSpot(lunchSpot), Times.Once);
    }
    
    [Fact]
    public async Task FindLunchSpots_ShouldReturnException_WhenServiceThrowsException()
    {
        var lunchSpot = new RequestLunchSpot { AveragePortionSize = "large" };
        
        _findLunchSpotServiceMock.Setup(s => s.FindLunchSpot(lunchSpot)).ThrowsAsync(new Exception());

        var result = await _subjectUnderTest.FindLunchSpots(lunchSpot);

        result.ShouldBeOfType<ObjectResult>();
        result.Value.ShouldBe("An unexpected error occurred.");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    public async Task DeleteLunchSpot_ShouldReturnNotFound_WhenNoLunchSpotNameIsProvided(string name)
    {
        var result = await _subjectUnderTest.DeleteLunchSpot(name);

        result.ShouldBeOfType<NotFoundObjectResult>();
        result.Value.ShouldBe($"No lunch spot with the name '{name}' exists.");
    }

    [Fact]
    public async Task DeleteLunchSpot_ShouldReturnNotFound_WhenNoMatchingLunchSpotsAreFound()
    {
        const string name = "Test Spot";

        _deleteLunchSpotServiceMock.Setup(s => s.DeleteLunchSpot(name)).ReturnsAsync(false);

        var result = await _subjectUnderTest.DeleteLunchSpot(name);

        result.ShouldBeOfType<NotFoundObjectResult>();
        result.Value.ShouldBe($"No lunch spot with the name '{name}' exists.");
    }

    [Fact]
    public async Task DeleteLunchSpot_ShouldReturnResponse_WhenRequestIsSuccessful()
    {
        const string name = "Test Spot";

        _deleteLunchSpotServiceMock.Setup(s => s.DeleteLunchSpot(name)).ReturnsAsync(true);

        var result = await _subjectUnderTest.DeleteLunchSpot(name);

        result.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldBe("Lunch spot deleted successfully.");
    }
    
    [Fact]
    public async Task DeleteLunchSpot_ShouldReturnException_WhenServiceThrowsException()
    {
        const string name = "Test Spot";

        _deleteLunchSpotServiceMock.Setup(s => s.DeleteLunchSpot(name)).ThrowsAsync(new Exception());

        var result = await _subjectUnderTest.DeleteLunchSpot(name);

        result.ShouldBeOfType<ObjectResult>();
        result.Value.ShouldBe("An unexpected error occurred.");
    }
}