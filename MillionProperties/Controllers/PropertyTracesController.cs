using Microsoft.AspNetCore.Mvc;
using MillionProperties.DTOs;
using MillionProperties.Models;
using MillionProperties.Repositories;

namespace MillionProperties.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyTracesController : ControllerBase
{
    private readonly IPropertyTraceRepository _propertyTraceRepository;
    private readonly IPropertyRepository _propertyRepository;

    public PropertyTracesController(IPropertyTraceRepository propertyTraceRepository, IPropertyRepository propertyRepository)
    {
        _propertyTraceRepository = propertyTraceRepository;
        _propertyRepository = propertyRepository;
    }

    /// <summary>
    /// Get all property traces
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyTraceResponseDto>>> GetAll()
    {
        var propertyTraces = await _propertyTraceRepository.GetAllAsync();
        var response = propertyTraces.Select(trace => new PropertyTraceResponseDto
        {
            Id = trace.Id,
            PropertyId = trace.PropertyId,
            DateSale = trace.DateSale,
            Name = trace.Name,
            Value = trace.Value,
            Tax = trace.Tax,
            CreatedAt = trace.CreatedAt,
            UpdatedAt = trace.UpdatedAt
        });

        return Ok(response);
    }

    /// <summary>
    /// Get property trace by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyTraceResponseDto>> GetById(string id)
    {
        var propertyTrace = await _propertyTraceRepository.GetByIdAsync(id);
        if (propertyTrace == null)
            return NotFound();

        var response = new PropertyTraceResponseDto
        {
            Id = propertyTrace.Id,
            PropertyId = propertyTrace.PropertyId,
            DateSale = propertyTrace.DateSale,
            Name = propertyTrace.Name,
            Value = propertyTrace.Value,
            Tax = propertyTrace.Tax,
            CreatedAt = propertyTrace.CreatedAt,
            UpdatedAt = propertyTrace.UpdatedAt
        };

        return Ok(response);
    }

    /// <summary>
    /// Get property traces by property ID
    /// </summary>
    [HttpGet("property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<PropertyTraceResponseDto>>> GetByPropertyId(string propertyId)
    {
        // Validate that the property exists
        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property == null)
            return NotFound("Property not found");

        var propertyTraces = await _propertyTraceRepository.GetByPropertyIdAsync(propertyId);
        var response = propertyTraces.Select(trace => new PropertyTraceResponseDto
        {
            Id = trace.Id,
            PropertyId = trace.PropertyId,
            DateSale = trace.DateSale,
            Name = trace.Name,
            Value = trace.Value,
            Tax = trace.Tax,
            CreatedAt = trace.CreatedAt,
            UpdatedAt = trace.UpdatedAt
        });

        return Ok(response);
    }

    /// <summary>
    /// Create a new property trace
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PropertyTraceResponseDto>> Create(CreatePropertyTraceDto createPropertyTraceDto)
    {
        // Validate that the property exists
        var property = await _propertyRepository.GetByIdAsync(createPropertyTraceDto.PropertyId);
        if (property == null)
            return BadRequest("Property not found");

        var propertyTrace = new PropertyTrace
        {
            PropertyId = createPropertyTraceDto.PropertyId,
            DateSale = createPropertyTraceDto.DateSale,
            Name = createPropertyTraceDto.Name,
            Value = createPropertyTraceDto.Value,
            Tax = createPropertyTraceDto.Tax
        };

        var createdPropertyTrace = await _propertyTraceRepository.CreateAsync(propertyTrace);

        var response = new PropertyTraceResponseDto
        {
            Id = createdPropertyTrace.Id,
            PropertyId = createdPropertyTrace.PropertyId,
            DateSale = createdPropertyTrace.DateSale,
            Name = createdPropertyTrace.Name,
            Value = createdPropertyTrace.Value,
            Tax = createdPropertyTrace.Tax,
            CreatedAt = createdPropertyTrace.CreatedAt,
            UpdatedAt = createdPropertyTrace.UpdatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = createdPropertyTrace.Id }, response);
    }

    /// <summary>
    /// Update an existing property trace
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdatePropertyTraceDto updatePropertyTraceDto)
    {
        var existingPropertyTrace = await _propertyTraceRepository.GetByIdAsync(id);
        if (existingPropertyTrace == null)
            return NotFound();

        // Validate that the property exists
        var property = await _propertyRepository.GetByIdAsync(updatePropertyTraceDto.PropertyId);
        if (property == null)
            return BadRequest("Property not found");

        var propertyTrace = new PropertyTrace
        {
            Id = id,
            PropertyId = updatePropertyTraceDto.PropertyId,
            DateSale = updatePropertyTraceDto.DateSale,
            Name = updatePropertyTraceDto.Name,
            Value = updatePropertyTraceDto.Value,
            Tax = updatePropertyTraceDto.Tax,
            CreatedAt = existingPropertyTrace.CreatedAt
        };

        var updated = await _propertyTraceRepository.UpdateAsync(id, propertyTrace);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Delete a property trace
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _propertyTraceRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Delete all property traces for a specific property
    /// </summary>
    [HttpDelete("property/{propertyId}")]
    public async Task<IActionResult> DeleteByPropertyId(string propertyId)
    {
        var property = await _propertyRepository.GetByIdAsync(propertyId);
        if (property == null)
            return NotFound("Property not found");

        var deleted = await _propertyTraceRepository.DeleteByPropertyIdAsync(propertyId);
        if (!deleted)
            return NotFound("No property traces found for this property");

        return NoContent();
    }
}
