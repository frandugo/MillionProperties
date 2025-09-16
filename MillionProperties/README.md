# MillionProperties API

A .NET 9.0 Web API for managing properties and owners with MongoDB integration and advanced filtering capabilities.

## Features

- **Property Management**: Full CRUD operations for properties
- **Owner Management**: Full CRUD operations for property owners
- **Advanced Property Filtering**: Comprehensive search and filter capabilities
- **Property Images**: Support for property image management
- **MongoDB Integration**: NoSQL database with optimized queries
- **Docker Support**: Containerized deployment with Docker Compose

## Advanced Property Filter Endpoint

The `/api/properties/filter` endpoint provides powerful filtering capabilities:

### Filter Parameters

- **name**: Filter by property name (partial match, case-insensitive)
- **address**: Filter by address (partial match, case-insensitive)
- **minPrice/maxPrice**: Price range filtering
- **minYear/maxYear**: Construction year range filtering
- **ownerId**: Filter by specific owner ID
- **ownerName**: Filter by owner name (partial match, case-insensitive)
- **codeInternal**: Filter by internal code (partial match, case-insensitive)
- **createdAfter/createdBefore**: Filter by creation date range
- **page**: Page number for pagination (default: 1)
- **pageSize**: Items per page (1-100, default: 10)
- **sortBy**: Sort field (name, price, year, address, createdAt)
- **sortDirection**: Sort direction (asc, desc)

### Example Filter Requests

```http
# Basic name search with pagination
GET /api/properties/filter?name=house&page=1&pageSize=10

# Price range filter
GET /api/properties/filter?minPrice=200000&maxPrice=500000

# Complex filter with multiple parameters
GET /api/properties/filter?name=villa&minPrice=300000&maxPrice=800000&minYear=2015&ownerName=john&sortBy=price&sortDirection=desc&page=1&pageSize=20

# Date range filter
GET /api/properties/filter?createdAfter=2024-01-01T00:00:00Z&createdBefore=2024-12-31T23:59:59Z&sortBy=createdAt&sortDirection=desc
```

### Response Format

```json
{
  "properties": [
    {
      "id": "string",
      "name": "string",
      "address": "string",
      "price": 0,
      "codeInternal": "string",
      "year": 0,
      "ownerId": "string",
      "ownerName": "string",
      "createdAt": "2024-01-01T00:00:00Z",
      "updatedAt": "2024-01-01T00:00:00Z"
    }
  ],
  "totalCount": 0,
  "page": 1,
  "pageSize": 10,
  "totalPages": 0,
  "hasNextPage": false,
  "hasPreviousPage": false
}
```

## Docker Setup

### Prerequisites

- Docker
- Docker Compose

### Running with Docker Compose

1. **Start the application**:
   ```bash
   docker-compose up -d
   ```

2. **View logs**:
   ```bash
   docker-compose logs -f api
   ```

3. **Stop the application**:
   ```bash
   docker-compose down
   ```

4. **Rebuild after changes**:
   ```bash
   docker-compose up --build -d
   ```

### Services

- **API**: Available at `http://localhost:5125`
- **MongoDB**: Available at `localhost:27017`
  - Username: `admin`
  - Password: `password123`
  - Database: `MillionPropertiesDB`

### Swagger Documentation

Once running, access the Swagger UI at: `http://localhost:5125/swagger`

## Development

### Local Development (without Docker)

1. **Install MongoDB** locally or use MongoDB Atlas
2. **Update connection string** in `appsettings.Development.json`
3. **Run the application**:
   ```bash
   dotnet run
   ```

### Project Structure

```
├── Controllers/          # API Controllers
├── DTOs/                # Data Transfer Objects
├── Models/              # Entity Models
├── Properties/Data/     # Database Context
├── Repositories/        # Repository Pattern Implementation
├── Services/           # Business Logic Services
├── wwwroot/            # Static Files
├── Dockerfile          # Docker configuration
├── docker-compose.yml  # Docker Compose configuration
└── MillionProperties.http # HTTP test requests
```

## API Endpoints

### Properties
- `GET /api/properties` - Get all properties with images and owner info
- `GET /api/properties/{id}` - Get property by ID
- `GET /api/properties/owner/{ownerId}` - Get properties by owner ID
- `GET /api/properties/filter` - **Advanced filter endpoint**
- `POST /api/properties` - Create new property
- `PUT /api/properties/{id}` - Update property
- `DELETE /api/properties/{id}` - Delete property

### Owners
- `GET /api/owners` - Get all owners
- `GET /api/owners/{id}` - Get owner by ID
- `POST /api/owners` - Create new owner
- `PUT /api/owners/{id}` - Update owner
- `DELETE /api/owners/{id}` - Delete owner

### Property Images
- `GET /api/propertyimages/property/{propertyId}` - Get images by property ID
- `POST /api/propertyimages` - Upload property image
- `PUT /api/propertyimages/{id}` - Update property image
- `DELETE /api/propertyimages/{id}` - Delete property image

## Performance Optimizations

The filter endpoint includes several optimizations:

1. **MongoDB Indexing**: Optimized queries with proper filtering
2. **Pagination**: Efficient data loading with skip/limit
3. **Projection**: Only required fields are retrieved
4. **Caching**: Owner lookups are optimized
5. **Validation**: Input validation prevents invalid queries

## Testing

Use the included `MillionProperties.http` file with your HTTP client (VS Code REST Client, Postman, etc.) to test all endpoints including the advanced filter functionality.
