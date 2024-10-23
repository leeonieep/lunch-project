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
            Name = "test spot", 
            MinutesWalkAway = 5, 
            PriceRange = "$"
        };
        
        var spots = new List<LunchSpot>();
        
        _lunchSpotRepositoryMock.Setup(r => r.LoadFromFile()).ReturnsAsync(spots);
        
        await _subjectUnderTest.AddLunchSpot(request);
        
        _lunchSpotRepositoryMock.Verify(x => x.LoadFromFile(), Times.Once);
        _lunchSpotRepositoryMock.Verify(x => x.SaveToFile(It.IsAny<List<LunchSpot>>()), Times.Once);
        
        spots.Count.ShouldBe(1);
        spots[0].Name.ShouldBeEquivalentTo(request.Name);
        spots[0].MinutesWalkAway.ShouldBeEquivalentTo(request.MinutesWalkAway);
        spots[0].PriceRange.ShouldBeEquivalentTo(request.PriceRange);
    }
    
    [Fact]
    public async Task AddLunchSpot_ShouldNotAddASpotThatAlreadyExists()
    {
        var request = new LunchSpot
        {
            Name = "test spot", 
            MinutesWalkAway = 5, 
            PriceRange = "$"
        };
        
        var spots = new List<LunchSpot>();
        
        _lunchSpotRepositoryMock.Setup(r => r.LoadFromFile()).ReturnsAsync(spots);
        
        await _subjectUnderTest.AddLunchSpot(request);
        
        _lunchSpotRepositoryMock.Verify(x => x.LoadFromFile(), Times.Once);
        _lunchSpotRepositoryMock.Verify(x => x.SaveToFile(It.IsAny<List<LunchSpot>>()), Times.Once);
        
        spots.Count.ShouldBe(1);
        spots[0].Name.ShouldBeEquivalentTo(request.Name);
        spots[0].MinutesWalkAway.ShouldBeEquivalentTo(request.MinutesWalkAway);
        spots[0].PriceRange.ShouldBeEquivalentTo(request.PriceRange);
    }
}