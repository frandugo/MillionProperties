using Microsoft.AspNetCore.Mvc;
using MillionProperties.DTOs;
using MillionProperties.Models;
using MillionProperties.Repositories;

namespace MillionProperties.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnersController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerResponseDto>>> GetAll()
    {
        var owners = await _ownerRepository.GetAllAsync();
        var response = owners.Select(o => new OwnerResponseDto
        {
            Id = o.Id,
            Name = o.Name,
            Address = o.Address,
            Photo = o.Photo,
            Birthday = o.Birthday,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OwnerResponseDto>> GetById(string id)
    {
        var owner = await _ownerRepository.GetByIdAsync(id);
        if (owner == null)
            return NotFound();

        var response = new OwnerResponseDto
        {
            Id = owner.Id,
            Name = owner.Name,
            Address = owner.Address,
            Photo = owner.Photo,
            Birthday = owner.Birthday,
            CreatedAt = owner.CreatedAt,
            UpdatedAt = owner.UpdatedAt
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OwnerResponseDto>> Create(CreateOwnerDto createOwnerDto)
    {
        var owner = new Owner
        {
            Name = createOwnerDto.Name,
            Address = createOwnerDto.Address,
            Photo = createOwnerDto.Photo,
            Birthday = createOwnerDto.Birthday
        };

        var createdOwner = await _ownerRepository.CreateAsync(owner);

        var response = new OwnerResponseDto
        {
            Id = createdOwner.Id,
            Name = createdOwner.Name,
            Address = createdOwner.Address,
            Photo = createdOwner.Photo,
            Birthday = createdOwner.Birthday,
            CreatedAt = createdOwner.CreatedAt,
            UpdatedAt = createdOwner.UpdatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = createdOwner.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UpdateOwnerDto updateOwnerDto)
    {
        var existingOwner = await _ownerRepository.GetByIdAsync(id);
        if (existingOwner == null)
            return NotFound();

        var owner = new Owner
        {
            Id = id,
            Name = updateOwnerDto.Name,
            Address = updateOwnerDto.Address,
            Photo = updateOwnerDto.Photo,
            Birthday = updateOwnerDto.Birthday,
            CreatedAt = existingOwner.CreatedAt
        };

        var updated = await _ownerRepository.UpdateAsync(id, owner);
        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _ownerRepository.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
