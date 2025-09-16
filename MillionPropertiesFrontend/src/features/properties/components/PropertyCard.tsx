'use client';

import { Property } from '@/types/property';
import { Card, CardHeader, CardFooter } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Separator } from '@/components/ui/separator';
import { 
  MapPin, 
  Calendar,
  Heart,
  Share2,
  Eye
} from 'lucide-react';
import Image from 'next/image';
import Link from 'next/link';
import { formatCurrency } from '@/lib/utils';

interface PropertyCardProps {
  property: Property;
  onView?: (property: Property) => void;
  onFavorite?: (property: Property) => void;
  onShare?: (property: Property) => void;
  className?: string;
}

export function PropertyCard({ 
  property, 
  onView, 
  onFavorite, 
  onShare,
  className 
}: PropertyCardProps) {
  const primaryImage = property.images?.[0];
  const imageUrl = primaryImage?.file ? `http://host.docker.internal:5125${primaryImage.file}` : '/placeholder-property.svg';

  return (
    <Link href={`/properties/${property.id}`} className="block">
      <Card className={`group overflow-hidden transition-all duration-300 hover:shadow-lg ${className}`}>
        <div className="relative aspect-[4/3] overflow-hidden rounded-lg">
          <Image
            src={imageUrl}
            alt={`${property.name} - Property Image`}
            fill
            className="object-cover transition-transform duration-300 hover:scale-105"
            sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 33vw"
          />
          
          <div className="absolute top-3 right-3 flex gap-2">
            <Button
              size="sm"
              variant="secondary"
              className="h-8 w-8 p-0 bg-white/90 hover:bg-white"
              onClick={(e) => {
                e.preventDefault();
                onFavorite?.(property);
              }}
            >
              <Heart className="h-4 w-4" />
            </Button>
            <Button
              size="sm"
              variant="secondary"
              className="h-8 w-8 p-0 bg-white/90 hover:bg-white"
              onClick={(e) => {
                e.preventDefault();
                onShare?.(property);
              }}
            >
              <Share2 className="h-4 w-4" />
            </Button>
          </div>
          
          <div className="absolute bottom-3 left-3">
            <Badge className="bg-black/80 text-white font-bold text-lg px-3 py-1">
              {formatCurrency(property.price, 'USD')}
            </Badge>
          </div>
        </div>

      <CardHeader className="pb-3">
        <div className="space-y-3">
          <div>
            <h3 className="font-semibold text-lg line-clamp-2">
              {property.name}
            </h3>
            <div className="flex items-center text-muted-foreground text-sm mt-1">
              <MapPin className="h-4 w-4 mr-1" />
              <span className="line-clamp-1">
                {property.address}
              </span>
            </div>
          </div>

          <div className="flex items-center gap-4 text-sm text-muted-foreground">
            <div className="flex items-center gap-1">
              <Calendar className="h-4 w-4" />
              <span>Built {property.year}</span>
            </div>
            <Badge variant="outline" className="text-xs">
              Code: {property.codeInternal}
            </Badge>
          </div>

          {property.propertyTraces && property.propertyTraces.length > 0 && (
            <div className="text-sm text-muted-foreground">
              <span>Latest: {property.propertyTraces[0]?.name}</span>
            </div>
          )}
        </div>
      </CardHeader>

      <Separator />
      <CardFooter className="p-4 pt-0">
        <div className="flex items-center justify-between w-full">
          {property.owner && (
            <div className="flex items-center gap-2">
              <Avatar className="h-8 w-8">
                <AvatarImage 
                  src={property.owner.photo ? `http://localhost:5125${property.owner.photo}` : undefined} 
                  alt={property.owner.name}
                />
                <AvatarFallback>
                  {property.owner.name.split(' ').map((n: string) => n[0]).join('')}
                </AvatarFallback>
              </Avatar>
              <div className="text-xs">
                <p className="font-medium">{property.owner.name}</p>
                <p className="text-muted-foreground">{property.owner.address}</p>
              </div>
            </div>
          )}
          
          <Button
            size="sm"
            onClick={() => onView?.(property)}
            className="ml-auto"
          >
            <Eye className="h-4 w-4 mr-1" />
            View Details
          </Button>
        </div>
      </CardFooter>
      </Card>
    </Link>
  );
}
