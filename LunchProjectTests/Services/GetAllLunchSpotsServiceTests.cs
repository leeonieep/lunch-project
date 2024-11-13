using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services;
using Moq;
using Shouldly;

namespace LunchProjectTests.Services;

public class GetAllLunchSpotsServiceTests
{
    private readonly Mock<ILunchSpotRepository> _lunchSpotRepositoryMock;
    private readonly GetAllLunchSpotsService _subjectUnderTest;

    public GetAllLunchSpotsServiceTests()
    {
        _lunchSpotRepositoryMock = new Mock<ILunchSpotRepository>();
        _subjectUnderTest = new GetAllLunchSpotsService(_lunchSpotRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnEmptyList_WhenNoSpotsExist()
    {
        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync([]);

        var result = await _subjectUnderTest.GetAllLunchSpots(1, 5);

        result.Data.ShouldBeEmpty();
        result.CurrentPage.ShouldBe(0);
        result.TotalPages.ShouldBe(0);
        result.TotalItems.ShouldBe(0);
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnFirstPageOfResults_WithCorrectPageSize()
    {
        var existingSpots = Enumerable.Range(1, 20).Select(i => new LunchSpot { Name = $"Spot {i}" }).ToList();

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);

        var result = await _subjectUnderTest.GetAllLunchSpots(1, 5);

        result.CurrentPage.ShouldBe(1);
        result.TotalPages.ShouldBe(4);
        result.TotalItems.ShouldBe(20);
        result.Data.Count.ShouldBe(5);
        result.Data.ShouldAllBe(s => s.Name.StartsWith("Spot"));
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnCorrectPage_WhenRequestedPageIsInRange()
    {
        var existingSpots = Enumerable.Range(1, 20).Select(i => new LunchSpot { Name = $"Spot {i}" }).ToList();

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);

        var result = await _subjectUnderTest.GetAllLunchSpots(2, 5);

        result.CurrentPage.ShouldBe(2);
        result.TotalPages.ShouldBe(4);
        result.TotalItems.ShouldBe(20);
        result.Data.Count.ShouldBe(5);
        result.Data.ShouldAllBe(s => s.Name.StartsWith("Spot"));
        result.Data.First().Name.ShouldBe("Spot 6");
        result.Data.Last().Name.ShouldBe("Spot 10");
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnLastPage_WhenPageExceedsTotalPages()
    {
        var existingSpots = Enumerable.Range(1, 15).Select(i => new LunchSpot { Name = $"Spot {i}" }).ToList();

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);

        var result = await _subjectUnderTest.GetAllLunchSpots(5, 5);

        result.TotalItems.ShouldBe(15);
        result.TotalPages.ShouldBe(3);
        result.CurrentPage.ShouldBe(3);
        result.Data.Count.ShouldBe(5);
    }

    [Fact]
    public async Task GetAllLunchSpots_ShouldReturnCorrectMetadata_WhenMultiplePagesExist()
    {
        var existingSpots = Enumerable.Range(1, 50).Select(i => new LunchSpot { Name = $"Spot {i}" }).ToList();

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);

        var result = await _subjectUnderTest.GetAllLunchSpots(1, 10);

        result.TotalItems.ShouldBe(50);
        result.TotalPages.ShouldBe(5);
        result.CurrentPage.ShouldBe(1);
        result.Data.Count.ShouldBe(10);
    }
}