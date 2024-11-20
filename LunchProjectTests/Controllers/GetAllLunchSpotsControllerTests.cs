using LunchProject.Controllers;
using LunchProject.Models;
using LunchProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace LunchProjectTests.Controllers;

public class GetAllLunchSpotsControllerTests
{
    private readonly Mock<IGetAllLunchSpotsService> _getAllLunchSpotsServiceMock;
    private readonly GetAllLunchSpotsController _subjectUnderTest;

    public GetAllLunchSpotsControllerTests()
    {
        _getAllLunchSpotsServiceMock = new Mock<IGetAllLunchSpotsService>();
        _subjectUnderTest = new GetAllLunchSpotsController(_getAllLunchSpotsServiceMock.Object);
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnResponse_WhenRequestIsSuccessful()
    {
        const int page = 1;
        const int pageSize = 10;

        var expectedSpots = new List<LunchSpot>
        {
            new() { Name = "Spot 1" },
            new() { Name = "Spot 2" }
        };

        var paginatedResponse = new PaginatedResponse()
        {
            CurrentPage = page,
            TotalPages = 1,
            TotalItems = expectedSpots.Count,
            PageSize = pageSize,
            Data = expectedSpots
        };

        _getAllLunchSpotsServiceMock.Setup(s => s.GetAllLunchSpots(1, 10)).ReturnsAsync(paginatedResponse);

        var result = await _subjectUnderTest.GetAllLunchSpots(page, pageSize);

        result.ShouldBeOfType<OkObjectResult>();
        result.Value.ShouldBe(paginatedResponse);
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnException_WhenServiceThrowsException()
    {
        _getAllLunchSpotsServiceMock.Setup(s => s.GetAllLunchSpots(1, 10)).ThrowsAsync(new Exception());

        var result = await _subjectUnderTest.GetAllLunchSpots();

        result.ShouldBeOfType<ObjectResult>();
        result.Value.ShouldBe("An unexpected error occurred.");
    }
}