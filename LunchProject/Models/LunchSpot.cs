using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LunchProject.Models;

public class LunchSpot
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [RegularExpression(@"^\${1,3}$", ErrorMessage = "Price range must be $, $$, or $$$")]
    public string? PriceRange { get; set; }

    [Required]
    [RegularExpression(@"small|medium|large", ErrorMessage = "Portion size must be 'small', 'medium', or 'large'")]
    public string? AveragePortionSize { get; set; }

    [Required]
    [Range(0, 30, ErrorMessage = "Minutes walk away must be between 0 and 30")]
    public int MinutesWalkAway { get; set; }

    [Required]
    public bool SuitableForLeonie { get; set; }

    [Required]
    public bool SuitableForSahir { get; set; }
        
    [Required]
    public bool SuitableForJanet { get; set; }
}