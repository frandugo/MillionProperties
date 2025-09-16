using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MillionProperties.Models;

namespace MillionProperties.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection") ?? config.GetConnectionString("MongoDb");
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase("MillionPropertiesDB");
    }

    public IMongoCollection<Owner> Owners => _database.GetCollection<Owner>("Owners");
    public IMongoCollection<Property> Properties => _database.GetCollection<Property>("Properties");
    public IMongoCollection<PropertyImage> PropertyImages => _database.GetCollection<PropertyImage>("PropertyImages");
    public IMongoCollection<PropertyTrace> PropertyTraces => _database.GetCollection<PropertyTrace>("PropertyTraces");
}