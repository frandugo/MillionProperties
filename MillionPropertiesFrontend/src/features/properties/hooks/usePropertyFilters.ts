'use client';

import { useState, useCallback } from 'react';
import { PropertyFilters, PropertySortBy } from '@/types/property';

interface UsePropertyFiltersReturn {
  filters: PropertyFilters;
  updateFilter: <K extends keyof PropertyFilters>(key: K, value: PropertyFilters[K]) => void;
  resetFilters: () => void;
  clearFilter: (key: keyof PropertyFilters) => void;
  setFilters: (filters: PropertyFilters) => void;
}

const defaultFilters: PropertyFilters = {
  sortBy: PropertySortBy.DATE_CREATED,
  sortOrder: 'desc',
};

export function usePropertyFilters(initialFilters: PropertyFilters = {}): UsePropertyFiltersReturn {
  const [filters, setFilters] = useState<PropertyFilters>({
    ...defaultFilters,
    ...initialFilters,
  });

  const updateFilter = useCallback(<K extends keyof PropertyFilters>(
    key: K,
    value: PropertyFilters[K]
  ) => {
    setFilters(prev => ({
      ...prev,
      [key]: value,
    }));
  }, []);

  const resetFilters = useCallback(() => {
    setFilters(defaultFilters);
  }, []);

  const clearFilter = useCallback((key: keyof PropertyFilters) => {
    setFilters(prev => {
      const newFilters = { ...prev };
      delete newFilters[key];
      return newFilters;
    });
  }, []);

  return {
    filters,
    updateFilter,
    resetFilters,
    clearFilter,
    setFilters,
  };
}
