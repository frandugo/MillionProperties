import { apiClient } from './client';
import { Property, PropertyFilters, PropertySortBy } from '@/types/property';

export interface PropertyApiResponse {
  properties: Property[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface PropertyApiFilters extends Record<string, unknown> {
  Page?: number;
  PageSize?: number;
  Name?: string;
  MinPrice?: number;
  MaxPrice?: number;
  MinYear?: number;
  MaxYear?: number;
  SortBy?: string;
  SortDirection?: 'asc' | 'desc';
}

class PropertiesApi {
  private readonly endpoint = '/api/properties';

  async getProperties(filters: PropertyFilters = {}, page = 1, pageSize = 12, sortBy?: PropertySortBy): Promise<PropertyApiResponse> {
    const params: PropertyApiFilters = {
      Page: page,
      PageSize: pageSize,
    };

    if (filters.search) params.Name = filters.search;
    if (filters.priceMin && filters.priceMin > 0) params.MinPrice = filters.priceMin;
    if (filters.priceMax && filters.priceMax < 2000000) params.MaxPrice = filters.priceMax;
    if (filters.yearBuiltMin && filters.yearBuiltMin > 1800) params.MinYear = filters.yearBuiltMin;
    if (filters.yearBuiltMax && filters.yearBuiltMax < 2030) params.MaxYear = filters.yearBuiltMax;

    if (sortBy) {
      switch (sortBy) {
        case PropertySortBy.PRICE:
          params.SortBy = 'price';
          params.SortDirection = filters.sortOrder || 'asc';
          break;
        case PropertySortBy.DATE_CREATED:
          params.SortBy = 'createdAt';
          params.SortDirection = filters.sortOrder || 'desc';
          break;
        case PropertySortBy.DATE_UPDATED:
          params.SortBy = 'updatedAt';
          params.SortDirection = filters.sortOrder || 'desc';
          break;
        case PropertySortBy.AREA:
          params.SortBy = 'area';
          params.SortDirection = filters.sortOrder || 'desc';
          break;
        case PropertySortBy.BEDROOMS:
          params.SortBy = 'bedrooms';
          params.SortDirection = filters.sortOrder || 'desc';
          break;
      }
    } else {
      params.SortBy = 'name';
      params.SortDirection = 'asc';
    }

    const endpoint = `${this.endpoint}/filter`;
    
    const response = await apiClient.get<PropertyApiResponse>(endpoint, params);
    return response.data;
  }

  async getProperty(id: string): Promise<Property> {
    const response = await apiClient.get<Property>(`${this.endpoint}/${id}`);
    return response.data;
  }

  async createProperty(property: Omit<Property, 'id' | 'createdAt' | 'updatedAt'>): Promise<Property> {
    const response = await apiClient.post<Property>(this.endpoint, property);
    return response.data;
  }

  async updateProperty(id: string, property: Partial<Property>): Promise<Property> {
    const response = await apiClient.put<Property>(`${this.endpoint}/${id}`, property);
    return response.data;
  }

  async deleteProperty(id: string): Promise<void> {
    await apiClient.delete(`${this.endpoint}/${id}`);
  }

  async getPropertyImages(propertyId: string): Promise<PropertyImage[]> {
    const response = await apiClient.get<PropertyImage[]>(`/api/propertyimages?propertyId=${propertyId}`);
    return response.data;
  }

  async getAllPropertyImages(): Promise<PropertyImage[]> {
    const response = await apiClient.get<PropertyImage[]>('/api/propertyimages');
    return response.data;
  }
}

interface PropertyImage {
  id: string;
  propertyId: string;
  file: string;
  enabled: boolean;
  createdAt: string;
  updatedAt: string;
}

export const propertiesApi = new PropertiesApi();
