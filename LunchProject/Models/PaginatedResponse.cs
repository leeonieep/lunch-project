namespace LunchProject.Models;

public class PaginatedResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public List<LunchSpot> Data { get; set; } = null!;
}