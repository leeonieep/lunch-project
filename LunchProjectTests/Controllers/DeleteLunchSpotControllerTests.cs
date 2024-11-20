using LunchProject.Controllers;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace LunchProjectTests.Controllers;

public class DeleteLunchSpotControllerTests
{
    private readonly Mock<IDeleteLunchSpotService> _deleteLunchSpotServiceMock;
    private readonly DeleteLunchSpotController _subjectUnderTest;

    public DeleteLunchSpotControllerTests()
    {
        _deleteLunchSpotServiceMock = new Mock<IDeleteLunchSpotService>();
        _subjectUnderTest = new DeleteLunchSpotController(_deleteLunchSpotServiceMock.Object);
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