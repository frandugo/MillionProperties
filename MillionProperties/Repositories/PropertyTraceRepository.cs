using MillionProperties.Data;
using MillionProperties.Models;
using MongoDB.Driver;

namespace MillionProperties.Repositories;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly MongoDbContext _context;

    public PropertyTraceRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PropertyTrace>> GetAllAsync()
    {
        return await _context.PropertyTraces.Find(_ => true).ToListAsync();
    }

    public async Task<PropertyTrace?> GetByIdAsync(string id)
    {
        return await _context.PropertyTraces.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string propertyId)
    {
        return await _context.PropertyTraces.Find(x => x.PropertyId == propertyId).ToListAsync();
    }

    public async Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace)
    {
        propertyTrace.CreatedAt = DateTime.UtcNow;
        propertyTrace.UpdatedAt = DateTime.UtcNow;
        await _context.PropertyTraces.InsertOneAsync(propertyTrace);
        return propertyTrace;
    }

    public async Task<bool> UpdateAsync(string id, PropertyTrace propertyTrace)
    {
        propertyTrace.UpdatedAt = DateTime.UtcNow;
        var result = await _context.PropertyTraces.ReplaceOneAsync(x => x.Id == id, propertyTrace);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.PropertyTraces.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteByPropertyIdAsync(string propertyId)
    {
        var result = await _context.PropertyTraces.DeleteManyAsync(x => x.PropertyId == propertyId);
        return result.DeletedCount > 0;
    }
}
