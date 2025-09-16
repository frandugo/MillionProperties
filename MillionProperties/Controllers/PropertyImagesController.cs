using Microsoft.AspNetCore.Mvc;
using MillionProperties.DTOs;
using MillionProperties.Models;
using MillionProperties.Repositories;

namespace MillionProperties.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyImagesController : ControllerBase
{
    private readonly IPropertyImageRepository _propertyImageRepository;

    public PropertyImagesController(IPropertyImageRepository propertyImageRepository)
    {
        _propertyImageRepository = propertyImageRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyImageResponseDto>>> GetAll()
    {
        var propertyImages = await _propertyImageRepository.GetAllAsync();
        var response = propertyImages.Select(pi => new PropertyImageResponseDto
        {
            Id = pi.Id,
            PropertyId = pi.PropertyId,
            File = pi.File,
            Enabled = pi.Enabled,
            CreatedAt = pi.CreatedAt,
            UpdatedAt = pi.UpdatedAt
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyImageResponseDto>> GetById(string id)
    {
        var propertyImage = await _propertyImageRepository.GetByIdAsync(id);
        if (propertyImage == null)
            return NotFound();

        var response = new PropertyImageResponseDto
        {
            Id = propertyImage.Id,
            PropertyId = propertyImage.PropertyId,
            File = propertyImage.File,
            Enabled = propertyImage.Enabled,
            CreatedAt = propertyImage.CreatedAt,
            UpdatedAt = propertyImage.UpdatedAt
        };

        return Ok(response);
    }

    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<PropertyImageResponseDto>>> GetByPropertyId(string propertyId)
    {
        var propertyImages = await _propertyImageRepository.GetByPropertyIdAsync(propertyId);
        var response = propertyImages.Select(pi => new PropertyImageResponseDto
        {
            Id = pi.Id,
            PropertyId = pi.PropertyId,
            File = pi.File,
            Enabled = pi.Enabled,
            CreatedAt = pi.CreatedAt,
            UpdatedAt = pi.UpdatedAt
        });
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PropertyImageResponseDto>> Create(CreatePropertyImageDto createPropertyImageDto)
    {
        var propertyImage = new PropertyImage
        {
            PropertyId = createPropertyImageDto.PropertyId,
            File = createPropertyImageDto.File,
            Enabled = createPropertyImageDto.Enabled
        };

        var createdPropertyImage = await _propertyImageRepository.CreateAsync(propertyImage);

        var response = new PropertyImageResponseDto
        {
            Id = createdPropertyImage.Id,
            PropertyId = createdPropertyImage.PropertyId,
            File = createdPropertyImage.File,
            Enabled = createdPropertyImage.Enabled,
            CreatedAt = createdPropertyImage.CreatedAt,
            UpdatedAt = createdPropertyImage.UpdatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = createdPropertyImage.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdatePropertyImageDto updatePropertyImageDto)
    {
        var existingPropertyImage = await _propertyImageRepository.GetByIdAsync(id);
        if (existingPropertyImage == null)
            return NotFound();

        var propertyImage = new PropertyImage
        {
            Id = id,
            PropertyId = updatePropertyImageDto.PropertyId,
            File = updatePropertyImageDto.File,
            Enabled = updatePropertyImageDto.Enabled,
            CreatedAt = existingPropertyImage.CreatedAt
        };

        var updated = await _propertyImageRepository.UpdateAsync(id, propertyImage);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _propertyImageRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
