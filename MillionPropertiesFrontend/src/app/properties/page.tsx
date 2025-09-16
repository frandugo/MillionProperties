'use client';

import { useState, useCallback, useMemo, useEffect, Suspense } from 'react';
import { Property, PropertyFilters } from '@/types/property';
import { useProperties } from '@/features/properties/hooks/useProperties';
import { usePropertyFilters } from '@/features/properties/hooks/usePropertyFilters';
import { PropertyList } from '@/features/properties/components/PropertyList';
import { PropertyFiltersSimple } from '@/features/properties/components/PropertyFiltersSimple';
import { useUrlParams } from '@/lib/hooks/useUrlParams';
import { debounce } from '@/lib/utils';

export const dynamic = 'force-dynamic';

function PropertiesPageContent() {
  const { getFiltersFromUrl, getSortByFromUrl, getPageFromUrl, updateUrlParams } = useUrlParams();
  const { filters, updateFilter, resetFilters, setFilters } = usePropertyFilters();
  const [currentPage, setCurrentPage] = useState(1);
  const [initialized, setInitialized] = useState(false);
  const limit = 12;

  const { properties, loading, error, total, totalPages, fetchProperties } = useProperties(
    filters,
    currentPage,
    limit
  );

  useEffect(() => {
    if (!initialized) {
      const urlFilters = getFiltersFromUrl();
      const urlSortBy = getSortByFromUrl();
      const urlPage = getPageFromUrl();
      
      if (urlSortBy) {
        urlFilters.sortBy = urlSortBy;
      }
      
      setFilters(urlFilters);
      setCurrentPage(urlPage);
      setInitialized(true);
    }
  }, [getFiltersFromUrl, getSortByFromUrl, getPageFromUrl, setFilters, initialized]);

  const debouncedFetchProperties = useMemo(
    () => debounce((newFilters: PropertyFilters) => {
      setCurrentPage(1);
      fetchProperties(newFilters, 1, limit, newFilters.sortBy);
    }, 300),
    [fetchProperties, limit]
  );

  const handleFilterChange = useCallback((newFilters: PropertyFilters) => {
    setFilters(newFilters);
    updateUrlParams(newFilters, newFilters.sortBy, 1);
    debouncedFetchProperties(newFilters);
  }, [setFilters, updateUrlParams, debouncedFetchProperties]);

  const handleResetFilters = useCallback(() => {
    resetFilters();
    setCurrentPage(1);
    updateUrlParams({}, undefined, 1);
    fetchProperties({}, 1, limit);
  }, [resetFilters, fetchProperties, limit, updateUrlParams]);

  const handlePageChange = useCallback((newPage: number) => {
    setCurrentPage(newPage);
    updateUrlParams(filters, filters.sortBy, newPage);
    fetchProperties(filters, newPage, limit, filters.sortBy);
  }, [filters, fetchProperties, limit, updateUrlParams]);

  if (!initialized) {
    return (
      <div className="min-h-screen bg-background flex items-center justify-center">
        <div className="text-center">
          <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-primary mx-auto"></div>
          <p className="mt-4 text-muted-foreground">Loading properties...</p>
        </div>
      </div>
    );
  }


  return (
    <div className="container mx-auto px-4 py-8">
      <div className="flex flex-col lg:flex-row gap-8">
        <div className="lg:w-1/4">
          <div className="sticky top-4">
            <PropertyFiltersSimple
              filters={filters}
              onFiltersChange={handleFilterChange}
              onReset={handleResetFilters}
            />
          </div>
        </div>

        <div className="lg:w-3/4">
          <div className="mb-6">
            <h1 className="text-3xl font-bold text-gray-900 mb-2">
              Properties
            </h1>
            <p className="text-gray-600">
              {total > 0 ? `${total} properties found` : 'No properties found'}
            </p>
          </div>

          {error && (
            <div className="mb-6 p-4 bg-red-50 border border-red-200 rounded-lg">
              <p className="text-red-600">{error}</p>
            </div>
          )}

          <PropertyList
            properties={properties}
            loading={loading}
            pagination={{
              page: currentPage,
              limit: limit,
              total: total,
              totalPages: totalPages
            }}
            onPageChange={handlePageChange}
          />
        </div>
      </div>
    </div>
  );
}

export default function PropertiesPage() {
  return (
    <Suspense fallback={<div>Loading...</div>}>
      <PropertiesPageContent />
    </Suspense>
  );
}
