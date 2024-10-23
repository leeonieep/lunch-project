using System.ComponentModel.DataAnnotations;

namespace LunchProject.Models;

public class RequestLunchSpot
{
    [RegularExpression(@"^\${1,3}$", ErrorMessage = "Price range must be $, $$, or $$$")]
    public string? PriceRange { get; set; }
    
    [RegularExpression("small|medium|large", ErrorMessage = "Portion size must be 'small', 'medium', or 'large'")]
    public string? AveragePortionSize { get; set; }
    
    [Range(0, 30, ErrorMessage = "Minutes walk away must be between 0 and 30")]
    public int? MinutesWalkAway { get; set; }

    public bool? SuitableForLeonie { get; set; } 

    public bool? SuitableForSahir { get; set; } 

    public bool? SuitableForJanet { get; set; } 
}