namespace MillionProperties.DTOs;

public class CreatePropertyImageDto
{
    public string PropertyId { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
}

public class UpdatePropertyImageDto
{
    public string PropertyId { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
}

public class PropertyImageResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string PropertyId { get; set; } = string.Empty;
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class PropertyWithImagesDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = string.Empty;
    public int Year { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<PropertyImageResponseDto> Images { get; set; } = new();
    public List<PropertyTraceResponseDto> PropertyTraces { get; set; } = new();
    public OwnerResponseDto? Owner { get; set; }
}
