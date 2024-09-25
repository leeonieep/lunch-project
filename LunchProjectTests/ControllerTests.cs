using LunchProject;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Controller = LunchProject.Controller;

namespace LunchProjectTests;

public class ControllerTests
{
    private readonly Mock<IAddLunchSpotService> _lunchSpotServiceMock;
    private readonly Controller _subjectUnderTest;
    
    public ControllerTests()
    {
        _lunchSpotServiceMock = new Mock<IAddLunchSpotService>();
        _subjectUnderTest = new Controller(_lunchSpotServiceMock.Object);
    }

    [Fact]
    public void AddLunchSpot_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var lunchSpot = new LunchSpot { Name = "Test Spot" };
        
        _subjectUnderTest.ModelState.AddModelError("Error", "Invalid model state");

        var result = _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void AddLunchSpot_ShouldReturnResponse_WhenRequestIsSuccessful()
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

        _lunchSpotServiceMock.Setup(s => s.AddLunchSpot(lunchSpot));

        var result = _subjectUnderTest.AddLunchSpot(lunchSpot);

        result.ShouldBeOfType<CreatedAtActionResult>();
        _lunchSpotServiceMock.Verify(s => s.AddLunchSpot(It.IsAny<LunchSpot>()), Times.Once);
    }
}