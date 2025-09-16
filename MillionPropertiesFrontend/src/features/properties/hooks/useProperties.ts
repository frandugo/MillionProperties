'use client';

import { useState, useEffect, useCallback } from 'react';
import { Property, PropertyFilters, PropertySortBy } from '@/types/property';
import { PropertyService } from '../services/property.service';
import { PropertyApiResponse } from '@/lib/api/properties.api';

interface UsePropertiesState {
  properties: Property[];
  loading: boolean;
  error: string | null;
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

interface UsePropertiesReturn extends UsePropertiesState {
  fetchProperties: (filters?: PropertyFilters, page?: number, pageSize?: number, sortBy?: PropertySortBy) => Promise<void>;
  refetch: () => Promise<void>;
  clearError: () => void;
}

export function useProperties(
  initialFilters: PropertyFilters = {},
  initialPage: number = 1,
  initialPageSize: number = 12
): UsePropertiesReturn {
  const [state, setState] = useState<UsePropertiesState>({
    properties: [],
    loading: false,
    error: null,
    total: 0,
    page: initialPage,
    pageSize: initialPageSize,
    totalPages: 0,
  });

  const [currentFilters, setCurrentFilters] = useState(initialFilters);
  const [currentPage, setCurrentPage] = useState(initialPage);
  const [currentPageSize, setCurrentPageSize] = useState(initialPageSize);
  const [currentSortBy, setCurrentSortBy] = useState<PropertySortBy | undefined>();

  const fetchProperties = useCallback(async (
    filters: PropertyFilters = {},
    page: number = 1,
    pageSize: number = 12,
    sortBy?: PropertySortBy
  ) => {
    setState(prev => ({ ...prev, loading: true, error: null }));
    
    try {
      const response: PropertyApiResponse = await PropertyService.getProperties(
        filters,
        page,
        pageSize,
        sortBy
      );

      setCurrentFilters(filters);
      setCurrentPage(page);
      setCurrentPageSize(pageSize);
      setCurrentSortBy(sortBy);
      
      setState(prev => ({
        ...prev,
        properties: response.properties,
        total: response.totalCount,
        page: response.page,
        pageSize: response.pageSize,
        totalPages: response.totalPages,
        loading: false,
      }));
    } catch (error) {
      setState(prev => ({
        ...prev,
        error: error instanceof Error ? error.message : 'An error occurred',
        loading: false,
      }));
    }
  }, []);

  const refetch = useCallback(() => {
    return fetchProperties(currentFilters, currentPage, currentPageSize, currentSortBy);
  }, [fetchProperties, currentFilters, currentPage, currentPageSize, currentSortBy]);

  const clearError = useCallback(() => {
    setState(prev => ({ ...prev, error: null }));
  }, []);

  useEffect(() => {
    if (typeof window !== 'undefined') {
      fetchProperties(initialFilters, initialPage, initialPageSize);
    }
  }, [fetchProperties, initialFilters, initialPage, initialPageSize]);

  return {
    ...state,
    fetchProperties,
    refetch,
    clearError,
  };
}
