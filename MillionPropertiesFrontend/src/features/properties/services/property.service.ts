import { Property, PropertyFilters, PropertySortBy } from '@/types/property';
import { propertiesApi, PropertyApiResponse } from '@/lib/api/properties.api';

export class PropertyService {
  static async getProperties(
    filters: PropertyFilters = {},
    page: number = 1,
    pageSize: number = 12,
    sortBy?: PropertySortBy
  ): Promise<PropertyApiResponse> {
    try {
      return await propertiesApi.getProperties(filters, page, pageSize, sortBy);
    } catch (error) {
      console.error('Error fetching properties:', error);
      return {
        properties: [],
        totalCount: 0,
        page: 1,
        pageSize: 12,
        totalPages: 0,
        hasNextPage: false,
        hasPreviousPage: false
      };
    }
  }

  static async getPropertyById(id: string): Promise<Property> {
    try {
      return await propertiesApi.getProperty(id);
    } catch (error) {
      console.error('Error fetching property:', error);
      throw new Error(`Property with id ${id} not found`);
    }
  }

  static async createProperty(property: Omit<Property, 'id' | 'createdAt' | 'updatedAt'>): Promise<Property> {
    return await propertiesApi.createProperty(property);
  }

  static async updateProperty(id: string, property: Partial<Property>): Promise<Property> {
    return await propertiesApi.updateProperty(id, property);
  }

  static async deleteProperty(id: string): Promise<void> {
    await propertiesApi.deleteProperty(id);
  }

}
