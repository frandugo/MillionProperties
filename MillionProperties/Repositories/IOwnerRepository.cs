using MillionProperties.Models;

namespace MillionProperties.Repositories;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner?> GetByIdAsync(string id);
    Task<Owner> CreateAsync(Owner owner);
    Task<bool> UpdateAsync(string id, Owner owner);
    Task<bool> DeleteAsync(string id);
}
