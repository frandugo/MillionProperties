namespace MillionProperties.DTOs;

public class PropertyFilterResultDto
{
    public IEnumerable<PropertyWithImagesDto> Properties { get; set; } = new List<PropertyWithImagesDto>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}
