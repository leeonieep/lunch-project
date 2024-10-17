using LunchProject;
using LunchProject.Models;
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
            new() { Name = "spot two",PriceRange = "$$" },
            new() { Name = "spot three",PriceRange = "$$$" }
        };
        
        _lunchSpotRepositoryMock.Setup(r => r.LoadFromFile()).ReturnsAsync(allLunchSpots);
        
        var result = await _subjectUnderTest.FindLunchSpot(request);
        
        result.Count.ShouldBe(1);
        result[0].Name.ShouldBe("spot one");
    }
}