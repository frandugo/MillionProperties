'use client';

import { PropertyFilters, PropertySortBy } from '@/types/property';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Separator } from '@/components/ui/separator';
import { Slider } from '@/components/ui/slider';
import { Search, Filter, X, SlidersHorizontal } from 'lucide-react';
import { useState } from 'react';
import { formatCurrency } from '@/lib/utils';

interface PropertyFiltersProps {
  filters: PropertyFilters;
  onFiltersChange: (filters: PropertyFilters) => void;
  onReset: () => void;
  className?: string;
}

export function PropertyFiltersComponent({ 
  filters, 
  onFiltersChange, 
  onReset,
  className 
}: PropertyFiltersProps) {
  const [isExpanded, setIsExpanded] = useState(true);
  const [localLocation, setLocalLocation] = useState(filters.location || '');
  const [priceRange, setPriceRange] = useState<[number, number]>([
    filters.priceMin || 0, 
    filters.priceMax || 2000000
  ]);
  const [yearRange, setYearRange] = useState<[number, number]>([
    filters.yearBuiltMin || 1950, 
    filters.yearBuiltMax || 2024
  ]);

  const updateFilter = <K extends keyof PropertyFilters>(key: K, value: PropertyFilters[K]) => {
    onFiltersChange({ ...filters, [key]: value });
  };



  const handleLocationSearch = () => {
    updateFilter('location', localLocation);
  };

  const handlePriceRangeChange = (value: number[]) => {
    const [min, max] = value;
    setPriceRange([min, max]);
    updateFilter('priceMin', min > 0 ? min : undefined);
    updateFilter('priceMax', max < 2000000 ? max : undefined);
  };

  const handleYearRangeChange = (value: number[]) => {
    const [min, max] = value;
    setYearRange([min, max]);
    updateFilter('yearBuiltMin', min > 1950 ? min : undefined);
    updateFilter('yearBuiltMax', max < 2024 ? max : undefined);
  };

  const getActiveFiltersCount = () => {
    const activeFiltersCount = [
      filters.search,
      filters.priceMin,
      filters.priceMax,
      filters.yearBuiltMin,
      filters.yearBuiltMax,
      filters.bedrooms,
      filters.bathrooms,
      filters.location,
    ].filter(Boolean).length;
    return activeFiltersCount;
  };

  const activeFiltersCount = getActiveFiltersCount();

  return (
    <Card className={className}>
      <CardHeader className="pb-3">
        <div className="flex items-center justify-between">
          <CardTitle className="text-lg flex items-center gap-2">
            <Filter className="h-5 w-5" />
            Filters
            {activeFiltersCount > 0 && (
              <Badge variant="secondary" className="ml-2">
                {activeFiltersCount}
              </Badge>
            )}
          </CardTitle>
          <div className="flex items-center gap-2">
            <Button
              variant="ghost"
              size="sm"
              onClick={() => setIsExpanded(!isExpanded)}
            >
              <SlidersHorizontal className="h-4 w-4" />
            </Button>
            {activeFiltersCount > 0 && (
              <Button variant="outline" size="sm" onClick={onReset}>
                <X className="h-4 w-4 mr-1" />
                Clear
              </Button>
            )}
          </div>
        </div>
      </CardHeader>

      <CardContent className="space-y-4">
        {/* Global Search */}
        <div className="space-y-2">
          <label className="text-sm font-medium">Search Properties</label>
          <Input
            placeholder="Search by title, description, or location..."
            value={filters.search || ''}
            onChange={(e) => updateFilter('search', e.target.value || undefined)}
          />
        </div>

        {/* Search Location */}
        <div className="space-y-2">
          <label className="text-sm font-medium">Location</label>
          <div className="flex gap-2">
            <Input
              placeholder="Search by city, state, or address..."
              value={localLocation}
              onChange={(e) => setLocalLocation(e.target.value)}
              onKeyDown={(e) => e.key === 'Enter' && handleLocationSearch()}
            />
            <Button size="sm" onClick={handleLocationSearch}>
              <Search className="h-4 w-4" />
            </Button>
          </div>
        </div>

        {/* Sort */}
        <div className="grid grid-cols-2 gap-3">
          <div className="space-y-2">
            <label className="text-sm font-medium">Sort by</label>
            <Select
              value={filters.sortBy || PropertySortBy.DATE_CREATED}
              onValueChange={(value) => updateFilter('sortBy', value as PropertySortBy)}
            >
              <SelectTrigger>
                <SelectValue />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value={PropertySortBy.PRICE}>Price</SelectItem>
                <SelectItem value={PropertySortBy.DATE_CREATED}>Date Listed</SelectItem>
                <SelectItem value={PropertySortBy.DATE_UPDATED}>Recently Updated</SelectItem>
                <SelectItem value={PropertySortBy.AREA}>Area</SelectItem>
                <SelectItem value={PropertySortBy.BEDROOMS}>Bedrooms</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <div className="space-y-2">
            <label className="text-sm font-medium">Order</label>
            <Select
              value={filters.sortOrder || 'desc'}
              onValueChange={(value) => updateFilter('sortOrder', value as 'asc' | 'desc')}
            >
              <SelectTrigger>
                <SelectValue />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="desc">High to Low</SelectItem>
                <SelectItem value="asc">Low to High</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>

        {isExpanded && (
          <>
            <Separator />


            {/* Price Range */}
            <div className="space-y-4">
              <label className="text-sm font-medium">Price Range</label>
              <div className="space-y-3">
                <div className="px-2">
                  <Slider
                    value={priceRange}
                    onValueChange={handlePriceRangeChange}
                    max={2000000}
                    min={0}
                    step={10000}
                    className="w-full"
                  />
                </div>
                <div className="flex items-center justify-between text-sm text-muted-foreground">
                  <span>{formatCurrency(priceRange[0])}</span>
                  <span>{formatCurrency(priceRange[1])}</span>
                </div>
                <div className="grid grid-cols-2 gap-3">
                  <Input
                    type="number"
                    placeholder="Min price"
                    value={priceRange[0] || ''}
                    onChange={(e) => {
                      const value = Number(e.target.value) || 0;
                      setPriceRange([value, priceRange[1]]);
                      updateFilter('priceMin', value > 0 ? value : undefined);
                    }}
                  />
                  <Input
                    type="number"
                    placeholder="Max price"
                    value={priceRange[1] || ''}
                    onChange={(e) => {
                      const value = Number(e.target.value) || 2000000;
                      setPriceRange([priceRange[0], value]);
                      updateFilter('priceMax', value < 2000000 ? value : undefined);
                    }}
                  />
                </div>
              </div>
            </div>

            {/* Year Built Range */}
            <div className="space-y-4">
              <label className="text-sm font-medium">Year Built</label>
              <div className="space-y-3">
                <div className="px-2">
                  <Slider
                    value={yearRange}
                    onValueChange={handleYearRangeChange}
                    max={2024}
                    min={1950}
                    step={1}
                    className="w-full"
                  />
                </div>
                <div className="flex items-center justify-between text-sm text-muted-foreground">
                  <span>{yearRange[0]}</span>
                  <span>{yearRange[1]}</span>
                </div>
                <div className="grid grid-cols-2 gap-3">
                  <Input
                    type="number"
                    placeholder="Min year"
                    value={yearRange[0] || ''}
                    onChange={(e) => {
                      const value = Number(e.target.value) || 1950;
                      setYearRange([value, yearRange[1]]);
                      updateFilter('yearBuiltMin', value > 1950 ? value : undefined);
                    }}
                  />
                  <Input
                    type="number"
                    placeholder="Max year"
                    value={yearRange[1] || ''}
                    onChange={(e) => {
                      const value = Number(e.target.value) || 2024;
                      setYearRange([yearRange[0], value]);
                      updateFilter('yearBuiltMax', value < 2024 ? value : undefined);
                    }}
                  />
                </div>
              </div>
            </div>

            {/* Bedrooms & Bathrooms */}
            <div className="grid grid-cols-2 gap-3">
              <div className="space-y-2">
                <label className="text-sm font-medium">Min Bedrooms</label>
                <Select
                  value={filters.bedrooms?.toString() || 'any'}
                  onValueChange={(value) => updateFilter('bedrooms', value === 'any' ? undefined : Number(value))}
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Any" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="any">Any</SelectItem>
                    <SelectItem value="1">1+</SelectItem>
                    <SelectItem value="2">2+</SelectItem>
                    <SelectItem value="3">3+</SelectItem>
                    <SelectItem value="4">4+</SelectItem>
                    <SelectItem value="5">5+</SelectItem>
                  </SelectContent>
                </Select>
              </div>

              <div className="space-y-2">
                <label className="text-sm font-medium">Min Bathrooms</label>
                <Select
                  value={filters.bathrooms?.toString() || 'any'}
                  onValueChange={(value) => updateFilter('bathrooms', value === 'any' ? undefined : Number(value))}
                >
                  <SelectTrigger>
                    <SelectValue placeholder="Any" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="any">Any</SelectItem>
                    <SelectItem value="1">1+</SelectItem>
                    <SelectItem value="2">2+</SelectItem>
                    <SelectItem value="3">3+</SelectItem>
                    <SelectItem value="4">4+</SelectItem>
                  </SelectContent>
                </Select>
              </div>
            </div>
          </>
        )}
      </CardContent>
    </Card>
  );
}
