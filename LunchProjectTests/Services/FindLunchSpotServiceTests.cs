using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services;
using Moq;
using Shouldly;

namespace LunchProjectTests.Services;

public class FindLunchSpotServiceTests
{
    private readonly Mock<ILunchSpotRepository> _lunchSpotRepositoryMock;
    private readonly FindLunchSpotService _subjectUnderTest;

    public FindLunchSpotServiceTests()
    {
        _lunchSpotRepositoryMock = new Mock<ILunchSpotRepository>();
        _subjectUnderTest = new FindLunchSpotService(_lunchSpotRepositoryMock.Object);
    }

    [Fact]
    public async Task FindLunchSpot_ShouldReturnMatchingSpotsWhenPriceRangeIsProvided()
    {
        var request = new RequestLunchSpot
        {
            PriceRange = "$"
        };

        var allLunchSpots = new List<LunchSpot>
        {
            new() { Name = "spot one", PriceRange = "$" },
            new() { Name = "spot two", PriceRange = "$$" },
            new() { Name = "spot three", PriceRange = "$$$" }
        };

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(allLunchSpots);

        var result = await _subjectUnderTest.FindLunchSpot(request);

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("spot one");
    }

    [Fact]
    public async Task FindLunchSpot_ShouldReturnMatchingSpotsWhenAveragePortionSizeIsProvided()
    {
        var request = new RequestLunchSpot
        {
            AveragePortionSize = "medium"
        };

        var allLunchSpots = new List<LunchSpot>
        {
            new() { Name = "spot one", AveragePortionSize = "small" },
            new() { Name = "spot two", AveragePortionSize = "medium" },
            new() { Name = "spot three", AveragePortionSize = "large" }
        };

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(allLunchSpots);

        var result = await _subjectUnderTest.FindLunchSpot(request);

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("spot two");
    }

    [Fact]
    public async Task FindLunchSpot_ShouldReturnMatchingSpotsWhenMinutesWalkAwayIsProvided()
    {
        var request = new RequestLunchSpot
        {
            MinutesWalkAway = 10
        };

        var allLunchSpots = new List<LunchSpot>
        {
            new() { Name = "spot one", MinutesWalkAway = 5 },
            new() { Name = "spot two", MinutesWalkAway = 7 },
            new() { Name = "spot three", MinutesWalkAway = 15 }
        };

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(allLunchSpots);

        var result = await _subjectUnderTest.FindLunchSpot(request);

        result.Count.ShouldBe(2);
        result[0].Name.ShouldBe("spot one");
        result[1].Name.ShouldBe("spot two");
    }

    [Fact]
    public async Task FindLunchSpot_ShouldReturnMatchingSpotsWhenSuitabilityForIndividualIsProvided()
    {
        var request = new RequestLunchSpot
        {
            SuitableForJanet = true
        };

        var allLunchSpots = new List<LunchSpot>
        {
            new() { Name = "spot one", SuitableForJanet = true },
            new() { Name = "spot two", SuitableForJanet = false },
            new() { Name = "spot three", SuitableForJanet = false }
        };

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(allLunchSpots);

        var result = await _subjectUnderTest.FindLunchSpot(request);

        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("spot one");
    }

    [Fact]
    public async Task FindLunchSpot_ShouldReturnEmptyWhenThereAreNoMatchingSpots()
    {
        var request = new RequestLunchSpot
        {
            MinutesWalkAway = 5
        };

        var allLunchSpots = new List<LunchSpot>
        {
            new() { Name = "spot one", MinutesWalkAway = 10 },
            new() { Name = "spot two", MinutesWalkAway = 11 },
            new() { Name = "spot three", MinutesWalkAway = 20 }
        };

        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(allLunchSpots);

        var result = await _subjectUnderTest.FindLunchSpot(request);

        result.Count.ShouldBe(0);
    }
}