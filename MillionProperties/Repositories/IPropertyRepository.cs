using MillionProperties.Models;
using MillionProperties.DTOs;

namespace MillionProperties.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(string id);
    Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId);
    Task<Property> CreateAsync(Property property);
    Task<bool> UpdateAsync(string id, Property property);
    Task<bool> DeleteAsync(string id);
    Task<(IEnumerable<Property> Properties, int TotalCount)> GetFilteredAsync(PropertyFilterDto filter);
}
