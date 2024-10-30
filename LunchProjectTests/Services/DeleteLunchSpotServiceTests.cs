using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services;
using Moq;
using Shouldly;

namespace LunchProjectTests.Services;

public class DeleteLunchSpotServiceTests
{
    private readonly Mock<ILunchSpotRepository> _lunchSpotRepositoryMock;
    private readonly DeleteLunchSpotService _subjectUnderTest;

    public DeleteLunchSpotServiceTests()
    {
        _lunchSpotRepositoryMock = new Mock<ILunchSpotRepository>();
        _subjectUnderTest = new DeleteLunchSpotService(_lunchSpotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task DeleteLunchSpot_ShouldNotRemoveLunchSpot_WhenRequestSpotNameDoesNotMatch()
    {
        var existingSpots = new List<LunchSpot>
        {
            new() { Name = "spot one" },
            new() { Name = "spot two" },
            new() { Name = "spot three" }
        };
        
        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);
        
        var result = await _subjectUnderTest.DeleteLunchSpot("spot four");
        
        result.ShouldBeFalse();
        existingSpots.ShouldBeEquivalentTo(existingSpots);
        _lunchSpotRepositoryMock.Verify(r => r.Add(existingSpots), Times.Never);
    }

    [Fact]
    public async Task DeleteLunchSpot_ShouldRemoveLunchSpot_WhenRequestSpotNameMatches()
    {
        var existingSpots = new List<LunchSpot>
        {
            new() { Name = "spot one" },
            new() { Name = "spot two" },
            new() { Name = "spot three" }
        };
        
        _lunchSpotRepositoryMock.Setup(r => r.Get()).ReturnsAsync(existingSpots);
        
        var result = await _subjectUnderTest.DeleteLunchSpot("spot two");
        
        result.ShouldBeTrue();
        existingSpots.ShouldNotContain(x => x.Name == "spot two");
        _lunchSpotRepositoryMock.Verify(r => r.Add(existingSpots), Times.Once);
    }
}