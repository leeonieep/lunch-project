using LunchProject.Models;
using LunchProject.Repositories;
using LunchProject.Services;
using Moq;
using Shouldly;

namespace LunchProjectTests.Services;

public class AddLunchSpotServiceTests
{
    private readonly Mock<ILunchSpotRepository> _lunchSpotRepositoryMock;
    private readonly AddLunchSpotService _subjectUnderTest;
    
    public AddLunchSpotServiceTests()
    {
        _lunchSpotRepositoryMock = new Mock<ILunchSpotRepository>();
        _subjectUnderTest = new AddLunchSpotService(_lunchSpotRepositoryMock.Object);
    }
    
    [Fact]
    public async Task AddLunchSpot_ShouldAddSpotToRepository()
    {
        var request = new LunchSpot
        {
            Name = "Test Spot", 
            MinutesWalkAway = 5, 
            PriceRange = "$"
        };
        
        var existingSpots = new List<LunchSpot>();
        
        _lunchSpotRepositoryMock.Setup(r => r.LoadFromFile()).ReturnsAsync(existingSpots);
        
        var result = await _subjectUnderTest.AddLunchSpot(request);
        
        _lunchSpotRepositoryMock.Verify(x => x.LoadFromFile(), Times.Once);
        _lunchSpotRepositoryMock.Verify(x => x.SaveToFile(It.IsAny<List<LunchSpot>>()), Times.Once);
        
        result.ShouldBeTrue();
        existingSpots.Count.ShouldBe(1);
        existingSpots[0].Name.ShouldBeEquivalentTo(request.Name);
        existingSpots[0].MinutesWalkAway.ShouldBeEquivalentTo(request.MinutesWalkAway);
        existingSpots[0].PriceRange.ShouldBeEquivalentTo(request.PriceRange);
    }
    
    [Fact]
    public async Task AddLunchSpot_ShouldNotAddASpotThatAlreadyExists()
    {
        var request = new LunchSpot
        {
            Name = "Test Spot", 
        };
        
        var existingSpots = new List<LunchSpot>
        {
            new() { Name = "Test Spot" }  
        };
        
        _lunchSpotRepositoryMock.Setup(r => r.LoadFromFile()).ReturnsAsync(existingSpots);
        
       var result = await _subjectUnderTest.AddLunchSpot(request);
        
        _lunchSpotRepositoryMock.Verify(x => x.SaveToFile(It.IsAny<List<LunchSpot>>()), Times.Never);
        
       result.ShouldBeFalse();
    }
}