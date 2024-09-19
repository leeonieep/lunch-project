using LunchProject;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Controller = LunchProject.Controller;

namespace LunchProjectTests;

public class ControllerTests
{
    private readonly Controller _subjectUnderTest = new();


    [Fact]
    public void AddLunchSpot_Should()
    {
        
      var result = _subjectUnderTest.AddLunchSpot(new LunchSpot
        {
            Name = "testName",
            PriceRange = "$",
            AveragePortionSize = "small",
            MinutesWalkAway = 0,
            SuitableForLeonie = true,
            SuitableForSahir = true,
            SuitableForJanet = true
        });
      
        result.ShouldBeOfType<CreatedAtActionResult>();
    }
}