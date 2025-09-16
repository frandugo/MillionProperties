using Microsoft.AspNetCore.Mvc;
using MillionProperties.DTOs;
using MillionProperties.Models;
using MillionProperties.Repositories;

namespace MillionProperties.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IPropertyImageRepository _propertyImageRepository;
    private readonly IPropertyTraceRepository _propertyTraceRepository;
    private readonly IOwnerRepository _ownerRepository;

    public PropertiesController(IPropertyRepository propertyRepository, IPropertyImageRepository propertyImageRepository, IPropertyTraceRepository propertyTraceRepository, IOwnerRepository ownerRepository)
    {
        _propertyRepository = propertyRepository;
        _propertyImageRepository = propertyImageRepository;
        _propertyTraceRepository = propertyTraceRepository;
        _ownerRepository = ownerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyWithImagesDto>>> GetAll()
    {
        var properties = await _propertyRepository.GetAllAsync();
        var response = new List<PropertyWithImagesDto>();

        foreach (var property in properties)
        {
            var images = await _propertyImageRepository.GetByPropertyIdAsync(property.Id);
            var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(property.Id);
            var owner = await _ownerRepository.GetByIdAsync(property.OwnerId);
            
            var propertyWithImages = new PropertyWithImagesDto
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                CodeInternal = property.CodeInternal,
                Year = property.Year,
                OwnerId = property.OwnerId,
                CreatedAt = property.CreatedAt,
                UpdatedAt = property.UpdatedAt,
                Images = images.Select(img => new PropertyImageResponseDto
                {
                    Id = img.Id,
                    PropertyId = img.PropertyId,
                    File = img.File,
                    Enabled = img.Enabled,
                    CreatedAt = img.CreatedAt,
                    UpdatedAt = img.UpdatedAt
                }).ToList(),
                PropertyTraces = propertyTraces.Select(trace => new PropertyTraceResponseDto
                {
                    Id = trace.Id,
                    PropertyId = trace.PropertyId,
                    DateSale = trace.DateSale,
                    Name = trace.Name,
                    Value = trace.Value,
                    Tax = trace.Tax,
                    CreatedAt = trace.CreatedAt,
                    UpdatedAt = trace.UpdatedAt
                }).ToList(),
                Owner = owner != null ? new OwnerResponseDto
                {
                    Id = owner.Id,
                    Name = owner.Name,
                    Address = owner.Address,
                    Photo = owner.Photo,
                    Birthday = owner.Birthday,
                    CreatedAt = owner.CreatedAt,
                    UpdatedAt = owner.UpdatedAt
                } : null
            };
            response.Add(propertyWithImages);
        }
        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyWithImagesDto>> GetById(string id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        if (property == null)
            return NotFound();

        var images = await _propertyImageRepository.GetByPropertyIdAsync(property.Id);
        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(property.Id);
        var owner = await _ownerRepository.GetByIdAsync(property.OwnerId);

        var response = new PropertyWithImagesDto
        {
            Id = property.Id,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
            OwnerId = property.OwnerId,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt,
            Images = images.Select(img => new PropertyImageResponseDto
            {
                Id = img.Id,
                PropertyId = img.PropertyId,
                File = img.File,
                Enabled = img.Enabled,
                CreatedAt = img.CreatedAt,
                UpdatedAt = img.UpdatedAt
            }).ToList(),
            PropertyTraces = propertyTraces.Select(trace => new PropertyTraceResponseDto
            {
                Id = trace.Id,
                PropertyId = trace.PropertyId,
                DateSale = trace.DateSale,
                Name = trace.Name,
                Value = trace.Value,
                Tax = trace.Tax,
                CreatedAt = trace.CreatedAt,
                UpdatedAt = trace.UpdatedAt
            }).ToList(),
            Owner = owner != null ? new OwnerResponseDto
            {
                Id = owner.Id,
                Name = owner.Name,
                Address = owner.Address,
                Photo = owner.Photo,
                Birthday = owner.Birthday,
                CreatedAt = owner.CreatedAt,
                UpdatedAt = owner.UpdatedAt
            } : null
        };

        return Ok(response);
    }

    [HttpGet("owner/{ownerId}")]
    public async Task<ActionResult<IEnumerable<PropertyResponseDto>>> GetByOwnerId(string ownerId)
    {
        var properties = await _propertyRepository.GetByOwnerIdAsync(ownerId);
        var response = properties.Select(p => new PropertyResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            Price = p.Price,
            CodeInternal = p.CodeInternal,
            Year = p.Year,
            OwnerId = p.OwnerId,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyResponseDto>> Create(CreatePropertyDto createPropertyDto)
    {
        var property = new Property
        {
            Name = createPropertyDto.Name,
            Address = createPropertyDto.Address,
            Price = createPropertyDto.Price,
            CodeInternal = createPropertyDto.CodeInternal,
            Year = createPropertyDto.Year,
            OwnerId = createPropertyDto.OwnerId
        };

        var createdProperty = await _propertyRepository.CreateAsync(property);

        var response = new PropertyResponseDto
        {
            Id = createdProperty.Id,
            Name = createdProperty.Name,
            Address = createdProperty.Address,
            Price = createdProperty.Price,
            CodeInternal = createdProperty.CodeInternal,
            Year = createdProperty.Year,
            OwnerId = createdProperty.OwnerId,
            CreatedAt = createdProperty.CreatedAt,
            UpdatedAt = createdProperty.UpdatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = createdProperty.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdatePropertyDto updatePropertyDto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        if (existingProperty == null)
            return NotFound();

        var property = new Property
        {
            Id = id,
            Name = updatePropertyDto.Name,
            Address = updatePropertyDto.Address,
            Price = updatePropertyDto.Price,
            CodeInternal = updatePropertyDto.CodeInternal,
            Year = updatePropertyDto.Year,
            OwnerId = updatePropertyDto.OwnerId,
            CreatedAt = existingProperty.CreatedAt
        };

        var updated = await _propertyRepository.UpdateAsync(id, property);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _propertyRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Get filtered properties with advanced search capabilities
    /// </summary>
    /// <param name="Name">Filter by property name (partial match, case-insensitive)</param>
    /// <param name="Address">Filter by address (partial match, case-insensitive)</param>
    /// <param name="MinPrice">Minimum price filter</param>
    /// <param name="MaxPrice">Maximum price filter</param>
    /// <param name="MinYear">Minimum construction year filter</param>
    /// <param name="MaxYear">Maximum construction year filter</param>
    /// <param name="OwnerId">Filter by specific owner ID</param>
    /// <param name="OwnerName">Filter by owner name (partial match, case-insensitive)</param>
    /// <param name="CodeInternal">Filter by internal code (partial match, case-insensitive)</param>
    /// <param name="CreatedAfter">Filter properties created after this date</param>
    /// <param name="CreatedBefore">Filter properties created before this date</param>
    /// <param name="Page">Page number for pagination (default: 1)</param>
    /// <param name="PageSize">Number of items per page (1-100, default: 10)</param>
    /// <param name="SortBy">Sort field: name, price, year, address, createdAt (default: createdAt)</param>
    /// <param name="SortDirection">Sort direction: asc or desc (default: desc)</param>
    /// <returns>Paginated filtered properties with metadata</returns>
    /// <response code="200">Returns the filtered properties with pagination metadata</response>
    /// <response code="400">If filter parameters are invalid</response>
    /// <response code="500">If an error occurs during filtering</response>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PropertyFilterResultDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PropertyFilterResultDto>> GetFiltered([FromQuery] PropertyFilterDto filter)
    {
        // Validate filter parameters
        if (!filter.IsValid(out var errors))
        {
            return BadRequest(new { errors });
        }

        try
        {
            var (properties, totalCount) = await _propertyRepository.GetFilteredAsync(filter);
            
            // Convert to DTOs with owner information and images
            var propertyDtos = new List<PropertyWithImagesDto>();
            foreach (var property in properties)
            {
                var images = await _propertyImageRepository.GetByPropertyIdAsync(property.Id);
                var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(property.Id);
                var owner = await _ownerRepository.GetByIdAsync(property.OwnerId);
                
                var propertyDto = new PropertyWithImagesDto
                {
                    Id = property.Id,
                    Name = property.Name,
                    Address = property.Address,
                    Price = property.Price,
                    CodeInternal = property.CodeInternal,
                    Year = property.Year,
                    OwnerId = property.OwnerId,
                    CreatedAt = property.CreatedAt,
                    UpdatedAt = property.UpdatedAt,
                    Images = images.Select(img => new PropertyImageResponseDto
                    {
                        Id = img.Id,
                        PropertyId = img.PropertyId,
                        File = img.File,
                        Enabled = img.Enabled,
                        CreatedAt = img.CreatedAt,
                        UpdatedAt = img.UpdatedAt
                    }).ToList(),
                    PropertyTraces = propertyTraces.Select(trace => new PropertyTraceResponseDto
                    {
                        Id = trace.Id,
                        PropertyId = trace.PropertyId,
                        DateSale = trace.DateSale,
                        Name = trace.Name,
                        Value = trace.Value,
                        Tax = trace.Tax,
                        CreatedAt = trace.CreatedAt,
                        UpdatedAt = trace.UpdatedAt
                    }).ToList(),
                    Owner = owner != null ? new OwnerResponseDto
                    {
                        Id = owner.Id,
                        Name = owner.Name,
                        Address = owner.Address,
                        Photo = owner.Photo,
                        Birthday = owner.Birthday,
                        CreatedAt = owner.CreatedAt,
                        UpdatedAt = owner.UpdatedAt
                    } : null
                };
                propertyDtos.Add(propertyDto);
            }

            // Calculate pagination metadata
            var totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize);
            
            var result = new PropertyFilterResultDto
            {
                Properties = propertyDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalPages = totalPages,
                HasNextPage = filter.Page < totalPages,
                HasPreviousPage = filter.Page > 1
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while filtering properties", error = ex.Message });
        }
    }
}
