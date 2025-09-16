using MillionProperties.Models;

namespace MillionProperties.Repositories;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImage>> GetAllAsync();
    Task<PropertyImage?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyImage> CreateAsync(PropertyImage propertyImage);
    Task<bool> UpdateAsync(string id, PropertyImage propertyImage);
    Task<bool> DeleteAsync(string id);
}
