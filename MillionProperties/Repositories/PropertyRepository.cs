using MillionProperties.Data;
using MillionProperties.Models;
using MillionProperties.DTOs;
using MongoDB.Driver;

namespace MillionProperties.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly MongoDbContext _context;

    public PropertyRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _context.Properties.Find(_ => true).ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        return await _context.Properties.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId)
    {
        return await _context.Properties.Find(x => x.OwnerId == ownerId).ToListAsync();
    }

    public async Task<Property> CreateAsync(Property property)
    {
        property.CreatedAt = DateTime.UtcNow;
        property.UpdatedAt = DateTime.UtcNow;
        await _context.Properties.InsertOneAsync(property);
        return property;
    }

    public async Task<bool> UpdateAsync(string id, Property property)
    {
        property.UpdatedAt = DateTime.UtcNow;
        var result = await _context.Properties.ReplaceOneAsync(x => x.Id == id, property);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Properties.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<(IEnumerable<Property> Properties, int TotalCount)> GetFilteredAsync(PropertyFilterDto filter)
    {
        var filterBuilder = Builders<Property>.Filter;
        var filters = new List<FilterDefinition<Property>>();

        // Name filter (case-insensitive partial match)
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            filters.Add(filterBuilder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(filter.Name, "i")));
        }

        // Address filter (case-insensitive partial match)
        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            filters.Add(filterBuilder.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(filter.Address, "i")));
        }

        // Price range filters
        if (filter.MinPrice.HasValue)
        {
            filters.Add(filterBuilder.Gte(x => x.Price, filter.MinPrice.Value));
        }
        if (filter.MaxPrice.HasValue)
        {
            filters.Add(filterBuilder.Lte(x => x.Price, filter.MaxPrice.Value));
        }

        // Year range filters
        if (filter.MinYear.HasValue)
        {
            filters.Add(filterBuilder.Gte(x => x.Year, filter.MinYear.Value));
        }
        if (filter.MaxYear.HasValue)
        {
            filters.Add(filterBuilder.Lte(x => x.Year, filter.MaxYear.Value));
        }

        // Owner ID filter
        if (!string.IsNullOrWhiteSpace(filter.OwnerId))
        {
            filters.Add(filterBuilder.Eq(x => x.OwnerId, filter.OwnerId));
        }

        // Internal code filter (case-insensitive partial match)
        if (!string.IsNullOrWhiteSpace(filter.CodeInternal))
        {
            filters.Add(filterBuilder.Regex(x => x.CodeInternal, new MongoDB.Bson.BsonRegularExpression(filter.CodeInternal, "i")));
        }

        // Created date range filters
        if (filter.CreatedAfter.HasValue)
        {
            filters.Add(filterBuilder.Gte(x => x.CreatedAt, filter.CreatedAfter.Value));
        }
        if (filter.CreatedBefore.HasValue)
        {
            filters.Add(filterBuilder.Lte(x => x.CreatedAt, filter.CreatedBefore.Value));
        }

        // Owner name filter (requires lookup)
        FilterDefinition<Property>? ownerNameFilter = null;
        if (!string.IsNullOrWhiteSpace(filter.OwnerName))
        {
            var ownerIds = await _context.Owners
                .Find(o => o.Name.ToLower().Contains(filter.OwnerName.ToLower()))
                .Project(o => o.Id)
                .ToListAsync();
            
            if (ownerIds.Any())
            {
                ownerNameFilter = filterBuilder.In(x => x.OwnerId, ownerIds);
            }
            else
            {
                // No owners found with that name, return empty result
                return (new List<Property>(), 0);
            }
        }

        // Combine all filters
        FilterDefinition<Property> combinedFilter;
        if (filters.Any() && ownerNameFilter != null)
        {
            filters.Add(ownerNameFilter);
            combinedFilter = filterBuilder.And(filters);
        }
        else if (filters.Any())
        {
            combinedFilter = filterBuilder.And(filters);
        }
        else if (ownerNameFilter != null)
        {
            combinedFilter = ownerNameFilter;
        }
        else
        {
            combinedFilter = filterBuilder.Empty;
        }

        // Get total count
        var totalCount = await _context.Properties.CountDocumentsAsync(combinedFilter);

        // Build sort definition
        SortDefinition<Property> sortDefinition;
        var isAscending = filter.SortDirection.ToLower() == "asc";
        
        switch (filter.SortBy.ToLower())
        {
            case "name":
                sortDefinition = isAscending ? Builders<Property>.Sort.Ascending(x => x.Name) : Builders<Property>.Sort.Descending(x => x.Name);
                break;
            case "price":
                sortDefinition = isAscending ? Builders<Property>.Sort.Ascending(x => x.Price) : Builders<Property>.Sort.Descending(x => x.Price);
                break;
            case "year":
                sortDefinition = isAscending ? Builders<Property>.Sort.Ascending(x => x.Year) : Builders<Property>.Sort.Descending(x => x.Year);
                break;
            case "address":
                sortDefinition = isAscending ? Builders<Property>.Sort.Ascending(x => x.Address) : Builders<Property>.Sort.Descending(x => x.Address);
                break;
            case "createdat":
            default:
                sortDefinition = isAscending ? Builders<Property>.Sort.Ascending(x => x.CreatedAt) : Builders<Property>.Sort.Descending(x => x.CreatedAt);
                break;
        }

        // Apply pagination and sorting
        var skip = (filter.Page - 1) * filter.PageSize;
        var properties = await _context.Properties
            .Find(combinedFilter)
            .Sort(sortDefinition)
            .Skip(skip)
            .Limit(filter.PageSize)
            .ToListAsync();

        return (properties, (int)totalCount);
    }
}
