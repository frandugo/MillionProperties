using System.ComponentModel.DataAnnotations;

namespace MillionProperties.DTOs;

public class PropertyFilterDto
{
    /// <summary>
    /// Filter by property name (partial match, case-insensitive)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Filter by address (partial match, case-insensitive)
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Minimum price filter
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "MinPrice must be greater than or equal to 0")]
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Maximum price filter
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "MaxPrice must be greater than or equal to 0")]
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Minimum year filter
    /// </summary>
    [Range(1800, 3000, ErrorMessage = "MinYear must be between 1800 and 3000")]
    public int? MinYear { get; set; }

    /// <summary>
    /// Maximum year filter
    /// </summary>
    [Range(1800, 3000, ErrorMessage = "MaxYear must be between 1800 and 3000")]
    public int? MaxYear { get; set; }

    /// <summary>
    /// Filter by owner ID
    /// </summary>
    public string? OwnerId { get; set; }

    /// <summary>
    /// Filter by owner name (partial match, case-insensitive)
    /// </summary>
    public string? OwnerName { get; set; }

    /// <summary>
    /// Filter by internal code (partial match, case-insensitive)
    /// </summary>
    public string? CodeInternal { get; set; }

    /// <summary>
    /// Filter properties created after this date
    /// </summary>
    public DateTime? CreatedAfter { get; set; }

    /// <summary>
    /// Filter properties created before this date
    /// </summary>
    public DateTime? CreatedBefore { get; set; }

    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (max 100)
    /// </summary>
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Sort field (name, price, year, address, createdAt)
    /// </summary>
    public string SortBy { get; set; } = "createdAt";

    /// <summary>
    /// Sort direction (asc, desc)
    /// </summary>
    public string SortDirection { get; set; } = "desc";

    /// <summary>
    /// Validates the filter parameters
    /// </summary>
    public bool IsValid(out List<string> errors)
    {
        errors = new List<string>();

        if (MinPrice.HasValue && MaxPrice.HasValue && MinPrice > MaxPrice)
        {
            errors.Add("MinPrice cannot be greater than MaxPrice");
        }

        if (MinYear.HasValue && MaxYear.HasValue && MinYear > MaxYear)
        {
            errors.Add("MinYear cannot be greater than MaxYear");
        }

        if (CreatedAfter.HasValue && CreatedBefore.HasValue && CreatedAfter > CreatedBefore)
        {
            errors.Add("CreatedAfter cannot be greater than CreatedBefore");
        }

        var validSortFields = new[] { "name", "price", "year", "address", "createdAt" };
        if (!validSortFields.Contains(SortBy.ToLower()))
        {
            errors.Add($"SortBy must be one of: {string.Join(", ", validSortFields)}");
        }

        var validSortDirections = new[] { "asc", "desc" };
        if (!validSortDirections.Contains(SortDirection.ToLower()))
        {
            errors.Add("SortDirection must be either 'asc' or 'desc'");
        }

        return errors.Count == 0;
    }
}
