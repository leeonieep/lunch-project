using LunchProject.Repositories;
using LunchProject.Services;
using Moq;

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
    public async Task DeleteLunchSpot_Should()
    {
   
    
    }

}