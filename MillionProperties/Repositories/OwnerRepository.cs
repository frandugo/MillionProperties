using MillionProperties.Data;
using MillionProperties.Models;
using MongoDB.Driver;

namespace MillionProperties.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly MongoDbContext _context;

    public OwnerRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        return await _context.Owners.Find(_ => true).ToListAsync();
    }

    public async Task<Owner?> GetByIdAsync(string id)
    {
        return await _context.Owners.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Owner> CreateAsync(Owner owner)
    {
        owner.CreatedAt = DateTime.UtcNow;
        owner.UpdatedAt = DateTime.UtcNow;
        await _context.Owners.InsertOneAsync(owner);
        return owner;
    }

    public async Task<bool> UpdateAsync(string id, Owner owner)
    {
        owner.UpdatedAt = DateTime.UtcNow;
        var result = await _context.Owners.ReplaceOneAsync(x => x.Id == id, owner);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Owners.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }
}
