'use client';

import { PropertyFilters, PropertySortBy } from '@/types/property';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { Separator } from '@/components/ui/separator';
import { Slider } from '@/components/ui/slider';
import { Search, Filter, X } from 'lucide-react';
import { useState } from 'react';
import { formatCurrency } from '@/lib/utils';

interface PropertyFiltersProps {
  filters: PropertyFilters;
  onFiltersChange: (filters: PropertyFilters) => void;
  onReset: () => void;
  className?: string;
}

export function PropertyFiltersSimple({ 
  filters, 
  onFiltersChange, 
  onReset,
  className 
}: PropertyFiltersProps) {
  const [minPrice, setMinPrice] = useState(filters.priceMin?.toString() || '');
  const [maxPrice, setMaxPrice] = useState(filters.priceMax?.toString() || '');

  const updateFilter = <K extends keyof PropertyFilters>(key: K, value: PropertyFilters[K]) => {
    onFiltersChange({ ...filters, [key]: value });
  };

  const handleMinPriceChange = (value: string) => {
    setMinPrice(value);
    const numValue = value ? parseInt(value, 10) : undefined;
    updateFilter('priceMin', numValue);
  };

  const handleMaxPriceChange = (value: string) => {
    setMaxPrice(value);
    const numValue = value ? parseInt(value, 10) : undefined;
    updateFilter('priceMax', numValue);
  };

  const handlePriceRangeChange = (values: number[]) => {
    updateFilter('priceMin', values[0]);
    updateFilter('priceMax', values[1]);
    setMinPrice(values[0].toString());
    setMaxPrice(values[1].toString());
  };

  const handleYearRangeChange = (values: number[]) => {
    updateFilter('yearBuiltMin', values[0]);
    updateFilter('yearBuiltMax', values[1]);
  };

  const getActiveFiltersCount = () => {
    return [
      filters.search,
      filters.priceMin,
      filters.priceMax,
      filters.yearBuiltMin,
      filters.yearBuiltMax,
      filters.sortBy !== PropertySortBy.DATE_CREATED ? filters.sortBy : null,
      filters.sortOrder !== 'desc' ? filters.sortOrder : null,
    ].filter(Boolean).length;
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
          {activeFiltersCount > 0 && (
            <Button variant="outline" size="sm" onClick={onReset}>
              <X className="h-4 w-4 mr-1" />
              Clear
            </Button>
          )}
        </div>
      </CardHeader>

      <CardContent className="space-y-4">
        <div className="space-y-2">
          <label className="text-sm font-medium">Search Properties</label>
          <div className="flex gap-2">
            <Input
              placeholder="Search by name (villa, house, etc.)..."
              value={filters.search || ''}
              onChange={(e) => updateFilter('search', e.target.value || undefined)}
            />
            <Search className="h-4 w-4 mt-3 text-muted-foreground" />
          </div>
        </div>

        <Separator />

        <div className="space-y-3">
          <label className="text-sm font-medium">Price Range</label>
          <div className="space-y-4">
            <Slider
              value={[filters.priceMin || 0, filters.priceMax || 2000000]}
              onValueChange={handlePriceRangeChange}
              min={0}
              max={2000000}
              step={10000}
              className="w-full"
            />
            <div className="flex justify-between text-sm text-muted-foreground">
              <span>{formatCurrency(filters.priceMin || 0)}</span>
              <span>{formatCurrency(filters.priceMax || 2000000)}</span>
            </div>
            <div className="grid grid-cols-2 gap-3">
              <Input
                type="number"
                placeholder="Min price"
                value={minPrice}
                onChange={(e) => handleMinPriceChange(e.target.value)}
              />
              <Input
                type="number"
                placeholder="Max price"
                value={maxPrice}
                onChange={(e) => handleMaxPriceChange(e.target.value)}
              />
            </div>
          </div>
        </div>

        <Separator />

        <div className="space-y-3">
          <label className="text-sm font-medium">Year Built</label>
          <div className="space-y-4">
            <Slider
              value={[filters.yearBuiltMin || 1950, filters.yearBuiltMax || 2024]}
              onValueChange={handleYearRangeChange}
              min={1950}
              max={2024}
              step={1}
              className="w-full"
            />
            <div className="flex justify-between text-sm text-muted-foreground">
              <span>{filters.yearBuiltMin || 1950}</span>
              <span>{filters.yearBuiltMax || 2024}</span>
            </div>
          </div>
        </div>

        <Separator />

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
                <SelectItem value={PropertySortBy.DATE_CREATED}>Date Created</SelectItem>
                <SelectItem value={PropertySortBy.DATE_UPDATED}>Date Updated</SelectItem>
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
                <SelectItem value="asc">Low to High</SelectItem>
                <SelectItem value="desc">High to Low</SelectItem>
              </SelectContent>
            </Select>
          </div>
        </div>

      </CardContent>
    </Card>
  );
}
