using LunchProject.Controllers;
using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace LunchProjectTests.Controllers;

public class FindLunchSpotsControllerTests
{
    private readonly Mock<IFindLunchSpotService> _findLunchSpotServiceMock;
    private readonly FindLunchSpotsController _subjectUnderTest;
    
    public FindLunchSpotsControllerTests()
    {
        _findLunchSpotServiceMock = new Mock<IFindLunchSpotService>();
        _subjectUnderTest = new FindLunchSpotsController(_findLunchSpotServiceMock.Object);
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
}