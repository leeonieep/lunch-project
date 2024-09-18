using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace LunchProject;

public class LunchSpot
{
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"^\${1,3}$", ErrorMessage = "Price range must be $, $$, or $$$")]
    public string PriceRange { get; set; }

    [Required]
    [RegularExpression(@"small|medium|large", ErrorMessage = "Portion size must be 'small', 'medium', or 'large'")]
    public string AveragePortionSize { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Minutes walk away must be a positive number")]
    public int MinutesWalkAway { get; set; }

    [Required]
    public bool SuitableForLeonie { get; set; }

    [Required]
    public bool SuitableForSahir { get; set; }
        
    [Required]
    public bool SuitableForJanet { get; set; }
}