using System.ComponentModel.DataAnnotations;

namespace MillionProperties.DTOs;

public class CreatePropertyTraceDto
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;

    [Required]
    public DateTime DateSale { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to 0")]
    public decimal Value { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Tax must be greater than or equal to 0")]
    public decimal Tax { get; set; }
}

public class UpdatePropertyTraceDto
{
    [Required]
    public string PropertyId { get; set; } = string.Empty;

    [Required]
    public DateTime DateSale { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to 0")]
    public decimal Value { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Tax must be greater than or equal to 0")]
    public decimal Tax { get; set; }
}

public class PropertyTraceResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string PropertyId { get; set; } = string.Empty;
    public DateTime DateSale { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
