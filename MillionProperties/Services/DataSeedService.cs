using MillionProperties.Data;
using MillionProperties.Models;
using MongoDB.Driver;

namespace MillionProperties.Services;

public class DataSeedService
{
    private readonly MongoDbContext _context;

    public DataSeedService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        var ownerCount = await _context.Owners.CountDocumentsAsync(FilterDefinition<Owner>.Empty);
        if (ownerCount > 0) return;

        var owners = await SeedOwnersAsync();
        
        var properties = await SeedPropertiesAsync(owners);
        
        await SeedPropertyImagesAsync(properties);
        
        await SeedPropertyTracesAsync(properties);
    }

    private async Task<List<Owner>> SeedOwnersAsync()
    {
        var owners = new List<Owner>
        {
            new Owner
            {
                Name = "John Smith",
                Address = "123 Oak Street, Beverly Hills, CA 90210",
                Photo = "/images/owners/owner1.jpg",
                Birthday = new DateTime(1975, 3, 15),
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Owner
            {
                Name = "Sarah Johnson",
                Address = "456 Pine Avenue, Manhattan, NY 10001",
                Photo = "/images/owners/owner2.jpg",
                Birthday = new DateTime(1982, 7, 22),
                CreatedAt = DateTime.UtcNow.AddDays(-25),
                UpdatedAt = DateTime.UtcNow.AddDays(-25)
            },
            new Owner
            {
                Name = "Michael Davis",
                Address = "789 Maple Drive, Miami, FL 33101",
                Photo = "/images/owners/owner3.jpg",
                Birthday = new DateTime(1968, 11, 8),
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                UpdatedAt = DateTime.UtcNow.AddDays(-20)
            },
            new Owner
            {
                Name = "Emily Wilson",
                Address = "321 Cedar Lane, Seattle, WA 98101",
                Photo = "/images/owners/owner4.jpg",
                Birthday = new DateTime(1990, 5, 12),
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Owner
            {
                Name = "Robert Chen",
                Address = "567 Broadway, San Francisco, CA 94133",
                Photo = "/images/owners/owner5.jpg",
                Birthday = new DateTime(1985, 9, 3),
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                UpdatedAt = DateTime.UtcNow.AddDays(-12)
            },
            new Owner
            {
                Name = "Maria Rodriguez",
                Address = "890 Sunset Strip, Los Angeles, CA 90069",
                Photo = "/images/owners/owner6.jpg",
                Birthday = new DateTime(1978, 12, 18),
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Owner
            {
                Name = "David Thompson",
                Address = "432 Park Avenue, New York, NY 10016",
                Photo = "/images/owners/owner7.jpg",
                Birthday = new DateTime(1972, 4, 25),
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-8)
            },
            new Owner
            {
                Name = "Lisa Anderson",
                Address = "654 Michigan Avenue, Chicago, IL 60611",
                Photo = "/images/owners/owner8.jpg",
                Birthday = new DateTime(1987, 6, 14),
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                UpdatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new Owner
            {
                Name = "James Miller",
                Address = "789 Peachtree Street, Atlanta, GA 30309",
                Photo = "/images/owners/owner9.jpg",
                Birthday = new DateTime(1980, 8, 30),
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Owner
            {
                Name = "Jennifer Garcia",
                Address = "321 Riverfront Drive, Austin, TX 78701",
                Photo = "/images/owners/owner10.jpg",
                Birthday = new DateTime(1992, 2, 7),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            }
        };

        await _context.Owners.InsertManyAsync(owners);
        return owners;
    }

    private async Task<List<Property>> SeedPropertiesAsync(List<Owner> owners)
    {
        var properties = new List<Property>
        {
            new Property
            {
                Name = "Luxury Villa Beverly Hills",
                Address = "1001 Rodeo Drive, Beverly Hills, CA 90210",
                Price = 2500000,
                CodeInternal = "PROP001",
                Year = 2019,
                OwnerId = owners[0].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-28),
                UpdatedAt = DateTime.UtcNow.AddDays(-28)
            },
            new Property
            {
                Name = "Modern Penthouse Manhattan",
                Address = "555 Fifth Avenue, Manhattan, NY 10017",
                Price = 3200000,
                CodeInternal = "PROP002",
                Year = 2021,
                OwnerId = owners[1].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-24),
                UpdatedAt = DateTime.UtcNow.AddDays(-24)
            },
            new Property
            {
                Name = "Oceanfront Condo Miami",
                Address = "888 Ocean Drive, Miami Beach, FL 33139",
                Price = 1800000,
                CodeInternal = "PROP003",
                Year = 2020,
                OwnerId = owners[2].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-22),
                UpdatedAt = DateTime.UtcNow.AddDays(-22)
            },
            new Property
            {
                Name = "Contemporary House Seattle",
                Address = "777 Lake View Drive, Seattle, WA 98109",
                Price = 950000,
                CodeInternal = "PROP004",
                Year = 2018,
                OwnerId = owners[3].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-18),
                UpdatedAt = DateTime.UtcNow.AddDays(-18)
            },
            new Property
            {
                Name = "Classic Estate Beverly Hills",
                Address = "1234 Sunset Boulevard, Beverly Hills, CA 90210",
                Price = 4500000,
                CodeInternal = "PROP005",
                Year = 2017,
                OwnerId = owners[0].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-16),
                UpdatedAt = DateTime.UtcNow.AddDays(-16)
            },
            new Property
            {
                Name = "Waterfront Villa Miami",
                Address = "999 Bay Shore Drive, Miami, FL 33154",
                Price = 2800000,
                CodeInternal = "PROP006",
                Year = 2022,
                OwnerId = owners[2].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-12),
                UpdatedAt = DateTime.UtcNow.AddDays(-12)
            },
            new Property
            {
                Name = "Tech Hub Loft San Francisco",
                Address = "123 Market Street, San Francisco, CA 94105",
                Price = 1650000,
                CodeInternal = "PROP007",
                Year = 2020,
                OwnerId = owners[4].Id, // Robert Chen
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                UpdatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new Property
            {
                Name = "Hollywood Hills Mansion",
                Address = "456 Mulholland Drive, Los Angeles, CA 90210",
                Price = 5200000,
                CodeInternal = "PROP008",
                Year = 2021,
                OwnerId = owners[5].Id, // Maria Rodriguez
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-8)
            },
            new Property
            {
                Name = "Central Park Apartment",
                Address = "789 Central Park West, New York, NY 10024",
                Price = 3800000,
                CodeInternal = "PROP009",
                Year = 2019,
                OwnerId = owners[6].Id, // David Thompson
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                UpdatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new Property
            {
                Name = "Lakefront Penthouse Chicago",
                Address = "321 Lake Shore Drive, Chicago, IL 60611",
                Price = 2100000,
                CodeInternal = "PROP010",
                Year = 2022,
                OwnerId = owners[7].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new Property
            {
                Name = "Historic Brownstone Atlanta",
                Address = "654 Piedmont Avenue, Atlanta, GA 30309",
                Price = 875000,
                CodeInternal = "PROP011",
                Year = 2018,
                OwnerId = owners[8].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new Property
            {
                Name = "Modern Ranch Austin",
                Address = "987 South Lamar, Austin, TX 78704",
                Price = 1350000,
                CodeInternal = "PROP012",
                Year = 2023,
                OwnerId = owners[9].Id, // Jennifer Garcia
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new Property
            {
                Name = "Golden Gate View Condo",
                Address = "555 Lombard Street, San Francisco, CA 94133",
                Price = 2250000,
                CodeInternal = "PROP013",
                Year = 2020,
                OwnerId = owners[4].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new Property
            {
                Name = "Beverly Hills Estate",
                Address = "777 Benedict Canyon Drive, Beverly Hills, CA 90210",
                Price = 6800000,
                CodeInternal = "PROP014",
                Year = 2021,
                OwnerId = owners[5].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new Property
            {
                Name = "Tribeca Luxury Loft",
                Address = "888 Greenwich Street, New York, NY 10014",
                Price = 4200000,
                CodeInternal = "PROP015",
                Year = 2022,
                OwnerId = owners[6].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new Property
            {
                Name = "Hill Country Retreat",
                Address = "111 Ranch Road, Austin, TX 78738",
                Price = 950000,
                CodeInternal = "PROP016",
                Year = 2019,
                OwnerId = owners[9].Id,
                CreatedAt = DateTime.UtcNow.AddDays(-9),
                UpdatedAt = DateTime.UtcNow.AddDays(-9)
            }
        };

        await _context.Properties.InsertManyAsync(properties);
        return properties;
    }

    private async Task SeedPropertyImagesAsync(List<Property> properties)
    {
        var propertyImages = new List<PropertyImage>
        {
            new PropertyImage
            {
                PropertyId = properties[0].Id,
                File = "/images/properties/house1.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-27),
                UpdatedAt = DateTime.UtcNow.AddDays(-27)
            },
            new PropertyImage
            {
                PropertyId = properties[0].Id,
                File = "/images/properties/house2.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-27),
                UpdatedAt = DateTime.UtcNow.AddDays(-27)
            },
            
            new PropertyImage
            {
                PropertyId = properties[1].Id,
                File = "/images/properties/house3.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-23),
                UpdatedAt = DateTime.UtcNow.AddDays(-23)
            },
            
            new PropertyImage
            {
                PropertyId = properties[2].Id,
                File = "/images/properties/house4.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-21),
                UpdatedAt = DateTime.UtcNow.AddDays(-21)
            },
            new PropertyImage
            {
                PropertyId = properties[2].Id,
                File = "/images/properties/house5.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-21),
                UpdatedAt = DateTime.UtcNow.AddDays(-21)
            },
            
            new PropertyImage
            {
                PropertyId = properties[3].Id,
                File = "/images/properties/house1.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-17),
                UpdatedAt = DateTime.UtcNow.AddDays(-17)
            },
            
            new PropertyImage
            {
                PropertyId = properties[4].Id,
                File = "/images/properties/house2.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new PropertyImage
            {
                PropertyId = properties[4].Id,
                File = "/images/properties/house3.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow.AddDays(-15)
            },
            
            new PropertyImage
            {
                PropertyId = properties[5].Id,
                File = "/images/properties/house4.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-11),
                UpdatedAt = DateTime.UtcNow.AddDays(-11)
            },
            
            new PropertyImage
            {
                PropertyId = properties[6].Id,
                File = "/images/properties/house6.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-9),
                UpdatedAt = DateTime.UtcNow.AddDays(-9)
            },
            new PropertyImage
            {
                PropertyId = properties[6].Id,
                File = "/images/properties/house7.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-9),
                UpdatedAt = DateTime.UtcNow.AddDays(-9)
            },
            
            new PropertyImage
            {
                PropertyId = properties[7].Id,
                File = "/images/properties/house8.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new PropertyImage
            {
                PropertyId = properties[7].Id,
                File = "/images/properties/house9.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new PropertyImage
            {
                PropertyId = properties[7].Id,
                File = "/images/properties/house10.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
                UpdatedAt = DateTime.UtcNow.AddDays(-7)
            },
            
            new PropertyImage
            {
                PropertyId = properties[8].Id,
                File = "/images/properties/house11.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new PropertyImage
            {
                PropertyId = properties[8].Id,
                File = "/images/properties/house12.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            },
            
            new PropertyImage
            {
                PropertyId = properties[9].Id,
                File = "/images/properties/house13.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = DateTime.UtcNow.AddDays(-3)
            },
            
            new PropertyImage
            {
                PropertyId = properties[10].Id,
                File = "/images/properties/house14.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            new PropertyImage
            {
                PropertyId = properties[10].Id,
                File = "/images/properties/house15.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = DateTime.UtcNow.AddDays(-1)
            },
            
            new PropertyImage
            {
                PropertyId = properties[11].Id,
                File = "/images/properties/house16.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            
            new PropertyImage
            {
                PropertyId = properties[12].Id,
                File = "/images/properties/house17.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddDays(-4)
            },
            new PropertyImage
            {
                PropertyId = properties[12].Id,
                File = "/images/properties/house18.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-4),
                UpdatedAt = DateTime.UtcNow.AddDays(-4)
            },
            
            new PropertyImage
            {
                PropertyId = properties[13].Id,
                File = "/images/properties/house19.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new PropertyImage
            {
                PropertyId = properties[13].Id,
                File = "/images/properties/house20.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            new PropertyImage
            {
                PropertyId = properties[13].Id,
                File = "/images/properties/house21.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow.AddDays(-2)
            },
            
            new PropertyImage
            {
                PropertyId = properties[14].Id,
                File = "/images/properties/house22.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                UpdatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new PropertyImage
            {
                PropertyId = properties[14].Id,
                File = "/images/properties/house23.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                UpdatedAt = DateTime.UtcNow.AddDays(-6)
            },
            
            new PropertyImage
            {
                PropertyId = properties[15].Id,
                File = "/images/properties/house24.jpg",
                Enabled = true,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                UpdatedAt = DateTime.UtcNow.AddDays(-8)
            }
        };

        await _context.PropertyImages.InsertManyAsync(propertyImages);
    }

    private async Task SeedPropertyTracesAsync(List<Property> properties)
    {
        var propertyTraces = new List<PropertyTrace>
        {
            // Traces for Luxury Villa Beverly Hills
            new PropertyTrace
            {
                PropertyId = properties[0].Id,
                DateSale = DateTime.UtcNow.AddDays(-365),
                Name = "Initial Purchase",
                Value = 2200000,
                Tax = 44000,
                CreatedAt = DateTime.UtcNow.AddDays(-365),
                UpdatedAt = DateTime.UtcNow.AddDays(-365)
            },
            new PropertyTrace
            {
                PropertyId = properties[0].Id,
                DateSale = DateTime.UtcNow.AddDays(-180),
                Name = "Property Appraisal",
                Value = 2500000,
                Tax = 50000,
                CreatedAt = DateTime.UtcNow.AddDays(-180),
                UpdatedAt = DateTime.UtcNow.AddDays(-180)
            },
            
            // Traces for Modern Penthouse Manhattan
            new PropertyTrace
            {
                PropertyId = properties[1].Id,
                DateSale = DateTime.UtcNow.AddDays(-400),
                Name = "Initial Purchase",
                Value = 2800000,
                Tax = 56000,
                CreatedAt = DateTime.UtcNow.AddDays(-400),
                UpdatedAt = DateTime.UtcNow.AddDays(-400)
            },
            new PropertyTrace
            {
                PropertyId = properties[1].Id,
                DateSale = DateTime.UtcNow.AddDays(-200),
                Name = "Renovation Assessment",
                Value = 3200000,
                Tax = 64000,
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                UpdatedAt = DateTime.UtcNow.AddDays(-200)
            },
            
            // Traces for Oceanfront Condo Miami
            new PropertyTrace
            {
                PropertyId = properties[2].Id,
                DateSale = DateTime.UtcNow.AddDays(-300),
                Name = "Initial Purchase",
                Value = 1600000,
                Tax = 32000,
                CreatedAt = DateTime.UtcNow.AddDays(-300),
                UpdatedAt = DateTime.UtcNow.AddDays(-300)
            },
            new PropertyTrace
            {
                PropertyId = properties[2].Id,
                DateSale = DateTime.UtcNow.AddDays(-100),
                Name = "Market Revaluation",
                Value = 1800000,
                Tax = 36000,
                CreatedAt = DateTime.UtcNow.AddDays(-100),
                UpdatedAt = DateTime.UtcNow.AddDays(-100)
            },
            
            // Traces for Contemporary House Seattle
            new PropertyTrace
            {
                PropertyId = properties[3].Id,
                DateSale = DateTime.UtcNow.AddDays(-450),
                Name = "Initial Purchase",
                Value = 850000,
                Tax = 17000,
                CreatedAt = DateTime.UtcNow.AddDays(-450),
                UpdatedAt = DateTime.UtcNow.AddDays(-450)
            },
            new PropertyTrace
            {
                PropertyId = properties[3].Id,
                DateSale = DateTime.UtcNow.AddDays(-150),
                Name = "Property Improvement",
                Value = 950000,
                Tax = 19000,
                CreatedAt = DateTime.UtcNow.AddDays(-150),
                UpdatedAt = DateTime.UtcNow.AddDays(-150)
            },
            
            // Traces for Classic Estate Beverly Hills
            new PropertyTrace
            {
                PropertyId = properties[4].Id,
                DateSale = DateTime.UtcNow.AddDays(-500),
                Name = "Initial Purchase",
                Value = 4000000,
                Tax = 80000,
                CreatedAt = DateTime.UtcNow.AddDays(-500),
                UpdatedAt = DateTime.UtcNow.AddDays(-500)
            },
            new PropertyTrace
            {
                PropertyId = properties[4].Id,
                DateSale = DateTime.UtcNow.AddDays(-250),
                Name = "Estate Valuation",
                Value = 4500000,
                Tax = 90000,
                CreatedAt = DateTime.UtcNow.AddDays(-250),
                UpdatedAt = DateTime.UtcNow.AddDays(-250)
            },
            
            // Traces for Waterfront Villa Miami
            new PropertyTrace
            {
                PropertyId = properties[5].Id,
                DateSale = DateTime.UtcNow.AddDays(-120),
                Name = "Initial Purchase",
                Value = 2600000,
                Tax = 52000,
                CreatedAt = DateTime.UtcNow.AddDays(-120),
                UpdatedAt = DateTime.UtcNow.AddDays(-120)
            },
            new PropertyTrace
            {
                PropertyId = properties[5].Id,
                DateSale = DateTime.UtcNow.AddDays(-60),
                Name = "Luxury Upgrade Assessment",
                Value = 2800000,
                Tax = 56000,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                UpdatedAt = DateTime.UtcNow.AddDays(-60)
            },
            
            // Traces for Tech Hub Loft San Francisco
            new PropertyTrace
            {
                PropertyId = properties[6].Id,
                DateSale = DateTime.UtcNow.AddDays(-280),
                Name = "Initial Purchase",
                Value = 1450000,
                Tax = 29000,
                CreatedAt = DateTime.UtcNow.AddDays(-280),
                UpdatedAt = DateTime.UtcNow.AddDays(-280)
            },
            new PropertyTrace
            {
                PropertyId = properties[6].Id,
                DateSale = DateTime.UtcNow.AddDays(-140),
                Name = "Tech Renovation Assessment",
                Value = 1650000,
                Tax = 33000,
                CreatedAt = DateTime.UtcNow.AddDays(-140),
                UpdatedAt = DateTime.UtcNow.AddDays(-140)
            },
            
            // Traces for Hollywood Hills Mansion
            new PropertyTrace
            {
                PropertyId = properties[7].Id,
                DateSale = DateTime.UtcNow.AddDays(-350),
                Name = "Initial Purchase",
                Value = 4800000,
                Tax = 96000,
                CreatedAt = DateTime.UtcNow.AddDays(-350),
                UpdatedAt = DateTime.UtcNow.AddDays(-350)
            },
            new PropertyTrace
            {
                PropertyId = properties[7].Id,
                DateSale = DateTime.UtcNow.AddDays(-180),
                Name = "Celebrity Home Appraisal",
                Value = 5200000,
                Tax = 104000,
                CreatedAt = DateTime.UtcNow.AddDays(-180),
                UpdatedAt = DateTime.UtcNow.AddDays(-180)
            },
            
            // Traces for Central Park Apartment
            new PropertyTrace
            {
                PropertyId = properties[8].Id,
                DateSale = DateTime.UtcNow.AddDays(-420),
                Name = "Initial Purchase",
                Value = 3400000,
                Tax = 68000,
                CreatedAt = DateTime.UtcNow.AddDays(-420),
                UpdatedAt = DateTime.UtcNow.AddDays(-420)
            },
            new PropertyTrace
            {
                PropertyId = properties[8].Id,
                DateSale = DateTime.UtcNow.AddDays(-210),
                Name = "Park View Premium Assessment",
                Value = 3800000,
                Tax = 76000,
                CreatedAt = DateTime.UtcNow.AddDays(-210),
                UpdatedAt = DateTime.UtcNow.AddDays(-210)
            },
            
            // Traces for Lakefront Penthouse Chicago
            new PropertyTrace
            {
                PropertyId = properties[9].Id,
                DateSale = DateTime.UtcNow.AddDays(-160),
                Name = "Initial Purchase",
                Value = 1900000,
                Tax = 38000,
                CreatedAt = DateTime.UtcNow.AddDays(-160),
                UpdatedAt = DateTime.UtcNow.AddDays(-160)
            },
            new PropertyTrace
            {
                PropertyId = properties[9].Id,
                DateSale = DateTime.UtcNow.AddDays(-80),
                Name = "Lakefront Premium Valuation",
                Value = 2100000,
                Tax = 42000,
                CreatedAt = DateTime.UtcNow.AddDays(-80),
                UpdatedAt = DateTime.UtcNow.AddDays(-80)
            },
            
            // Traces for Historic Brownstone Atlanta
            new PropertyTrace
            {
                PropertyId = properties[10].Id,
                DateSale = DateTime.UtcNow.AddDays(-320),
                Name = "Initial Purchase",
                Value = 750000,
                Tax = 15000,
                CreatedAt = DateTime.UtcNow.AddDays(-320),
                UpdatedAt = DateTime.UtcNow.AddDays(-320)
            },
            new PropertyTrace
            {
                PropertyId = properties[10].Id,
                DateSale = DateTime.UtcNow.AddDays(-160),
                Name = "Historic Restoration Assessment",
                Value = 875000,
                Tax = 17500,
                CreatedAt = DateTime.UtcNow.AddDays(-160),
                UpdatedAt = DateTime.UtcNow.AddDays(-160)
            },
            
            // Traces for Modern Ranch Austin
            new PropertyTrace
            {
                PropertyId = properties[11].Id,
                DateSale = DateTime.UtcNow.AddDays(-90),
                Name = "Initial Purchase",
                Value = 1200000,
                Tax = 24000,
                CreatedAt = DateTime.UtcNow.AddDays(-90),
                UpdatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new PropertyTrace
            {
                PropertyId = properties[11].Id,
                DateSale = DateTime.UtcNow.AddDays(-45),
                Name = "Modern Upgrade Valuation",
                Value = 1350000,
                Tax = 27000,
                CreatedAt = DateTime.UtcNow.AddDays(-45),
                UpdatedAt = DateTime.UtcNow.AddDays(-45)
            },
            
            // Traces for Golden Gate View Condo
            new PropertyTrace
            {
                PropertyId = properties[12].Id,
                DateSale = DateTime.UtcNow.AddDays(-240),
                Name = "Initial Purchase",
                Value = 2000000,
                Tax = 40000,
                CreatedAt = DateTime.UtcNow.AddDays(-240),
                UpdatedAt = DateTime.UtcNow.AddDays(-240)
            },
            new PropertyTrace
            {
                PropertyId = properties[12].Id,
                DateSale = DateTime.UtcNow.AddDays(-120),
                Name = "Golden Gate Premium Assessment",
                Value = 2250000,
                Tax = 45000,
                CreatedAt = DateTime.UtcNow.AddDays(-120),
                UpdatedAt = DateTime.UtcNow.AddDays(-120)
            },
            
            // Traces for Beverly Hills Estate
            new PropertyTrace
            {
                PropertyId = properties[13].Id,
                DateSale = DateTime.UtcNow.AddDays(-380),
                Name = "Initial Purchase",
                Value = 6200000,
                Tax = 124000,
                CreatedAt = DateTime.UtcNow.AddDays(-380),
                UpdatedAt = DateTime.UtcNow.AddDays(-380)
            },
            new PropertyTrace
            {
                PropertyId = properties[13].Id,
                DateSale = DateTime.UtcNow.AddDays(-190),
                Name = "Luxury Estate Appraisal",
                Value = 6800000,
                Tax = 136000,
                CreatedAt = DateTime.UtcNow.AddDays(-190),
                UpdatedAt = DateTime.UtcNow.AddDays(-190)
            },
            
            // Traces for Tribeca Luxury Loft
            new PropertyTrace
            {
                PropertyId = properties[14].Id,
                DateSale = DateTime.UtcNow.AddDays(-200),
                Name = "Initial Purchase",
                Value = 3800000,
                Tax = 76000,
                CreatedAt = DateTime.UtcNow.AddDays(-200),
                UpdatedAt = DateTime.UtcNow.AddDays(-200)
            },
            new PropertyTrace
            {
                PropertyId = properties[14].Id,
                DateSale = DateTime.UtcNow.AddDays(-100),
                Name = "Tribeca Premium Valuation",
                Value = 4200000,
                Tax = 84000,
                CreatedAt = DateTime.UtcNow.AddDays(-100),
                UpdatedAt = DateTime.UtcNow.AddDays(-100)
            },
            
            // Traces for Hill Country Retreat
            new PropertyTrace
            {
                PropertyId = properties[15].Id,
                DateSale = DateTime.UtcNow.AddDays(-270),
                Name = "Initial Purchase",
                Value = 850000,
                Tax = 17000,
                CreatedAt = DateTime.UtcNow.AddDays(-270),
                UpdatedAt = DateTime.UtcNow.AddDays(-270)
            },
            new PropertyTrace
            {
                PropertyId = properties[15].Id,
                DateSale = DateTime.UtcNow.AddDays(-135),
                Name = "Country Property Assessment",
                Value = 950000,
                Tax = 19000,
                CreatedAt = DateTime.UtcNow.AddDays(-135),
                UpdatedAt = DateTime.UtcNow.AddDays(-135)
            }
        };

        await _context.PropertyTraces.InsertManyAsync(propertyTraces);
    }
}
