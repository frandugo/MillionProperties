import { useRouter, useSearchParams } from 'next/navigation';
import { useCallback } from 'react';
import { PropertyFilters, PropertySortBy } from '@/types/property';

export function useUrlParams() {
  const router = useRouter();
  const searchParams = useSearchParams();

  const getFiltersFromUrl = useCallback((): PropertyFilters => {
    const filters: PropertyFilters = {};
    
    const search = searchParams.get('search');
    if (search) filters.search = search;
    
    const minPrice = searchParams.get('minPrice');
    if (minPrice) filters.priceMin = parseInt(minPrice, 10);
    
    const maxPrice = searchParams.get('maxPrice');
    if (maxPrice) filters.priceMax = parseInt(maxPrice, 10);
    
    const sortOrder = searchParams.get('sortOrder');
    if (sortOrder === 'asc' || sortOrder === 'desc') {
      filters.sortOrder = sortOrder;
    }
    
    return filters;
  }, [searchParams]);

  const getSortByFromUrl = useCallback((): PropertySortBy | undefined => {
    const sortBy = searchParams.get('sortBy');
    if (!sortBy) return undefined;
    
    switch (sortBy.toLowerCase()) {
      case 'price':
        return PropertySortBy.PRICE;
      case 'createdat':
      case 'date_created':
        return PropertySortBy.DATE_CREATED;
      case 'updatedat':
      case 'date_updated':
        return PropertySortBy.DATE_UPDATED;
      case 'area':
        return PropertySortBy.AREA;
      case 'bedrooms':
        return PropertySortBy.BEDROOMS;
      default:
        return undefined;
    }
  }, [searchParams]);

  const getPageFromUrl = useCallback((): number => {
    const page = searchParams.get('page');
    return page ? Math.max(1, parseInt(page, 10)) : 1;
  }, [searchParams]);

  const updateUrlParams = useCallback((
    filters: PropertyFilters,
    sortBy?: PropertySortBy,
    page: number = 1
  ) => {
    const params = new URLSearchParams();
    
    if (filters.search) params.set('search', filters.search);
    if (filters.priceMin) params.set('minPrice', filters.priceMin.toString());
    if (filters.priceMax) params.set('maxPrice', filters.priceMax.toString());
    if (filters.sortOrder) params.set('sortOrder', filters.sortOrder);
    
    if (sortBy) {
      switch (sortBy) {
        case PropertySortBy.PRICE:
          params.set('sortBy', 'price');
          break;
        case PropertySortBy.DATE_CREATED:
          params.set('sortBy', 'createdAt');
          break;
        case PropertySortBy.DATE_UPDATED:
          params.set('sortBy', 'updatedAt');
          break;
        case PropertySortBy.AREA:
          params.set('sortBy', 'area');
          break;
        case PropertySortBy.BEDROOMS:
          params.set('sortBy', 'bedrooms');
          break;
      }
    }
    
    if (page > 1) params.set('page', page.toString());
    
    const newUrl = params.toString() ? `?${params.toString()}` : '/properties';
    router.push(newUrl, { scroll: false });
  }, [router]);

  return {
    getFiltersFromUrl,
    getSortByFromUrl,
    getPageFromUrl,
    updateUrlParams,
  };
}
