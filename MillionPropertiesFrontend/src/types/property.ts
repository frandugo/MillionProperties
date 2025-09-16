export interface Property {
  id: string;
  name: string;
  address: string;
  price: number;
  codeInternal: string;
  year: number;
  ownerId: string;
  createdAt: string;
  updatedAt: string;
  ownerName?: string;
  images?: PropertyImage[];
  propertyTraces?: PropertyTrace[];
  owner?: PropertyOwner;
}

export interface PropertyImage {
  id: string;
  propertyId: string;
  file: string;
  enabled: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface PropertyTrace {
  id: string;
  propertyId: string;
  dateSale: string;
  name: string;
  value: number;
  tax: number;
  createdAt: string;
  updatedAt: string;
}

export interface PropertyOwner {
  id: string;
  name: string;
  address: string;
  photo: string;
  birthday: string;
  createdAt: string;
  updatedAt: string;
}

export enum PropertyType {
  HOUSE = 'house',
  APARTMENT = 'apartment',
  CONDO = 'condo',
  TOWNHOUSE = 'townhouse',
  VILLA = 'villa',
  LAND = 'land',
  COMMERCIAL = 'commercial',
}

export enum PropertyStatus {
  FOR_SALE = 'for_sale',
  FOR_RENT = 'for_rent',
  SOLD = 'sold',
  RENTED = 'rented',
  OFF_MARKET = 'off_market',
}

export interface PropertyFilters {
  search?: string;
  priceMin?: number;
  priceMax?: number;
  yearBuiltMin?: number;
  yearBuiltMax?: number;
  propertyType?: PropertyType[];
  status?: PropertyStatus[];
  bedrooms?: number;
  bathrooms?: number;
  location?: string;
  sortBy?: PropertySortBy;
  sortOrder?: 'asc' | 'desc';
}

export enum PropertySortBy {
  PRICE = 'price',
  DATE_CREATED = 'createdAt',
  DATE_UPDATED = 'updatedAt',
  AREA = 'area',
  BEDROOMS = 'bedrooms',
}

export interface PropertyListResponse {
  properties: Property[];
  pagination: {
    page: number;
    limit: number;
    total: number;
    totalPages: number;
  };
}
