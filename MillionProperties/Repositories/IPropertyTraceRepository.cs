using MillionProperties.Models;

namespace MillionProperties.Repositories;

public interface IPropertyTraceRepository
{
    Task<IEnumerable<PropertyTrace>> GetAllAsync();
    Task<PropertyTrace?> GetByIdAsync(string id);
    Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId);
    Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace);
    Task<bool> UpdateAsync(string id, PropertyTrace propertyTrace);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteByPropertyIdAsync(string propertyId);
}
