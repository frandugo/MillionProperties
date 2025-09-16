'use client';

export const dynamic = 'force-dynamic';

import { useEffect, useState } from 'react';
import { Property } from '@/types/property';
import { PropertyService } from '@/features/properties/services/property.service';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Separator } from '@/components/ui/separator';
import { 
  MapPin, 
  Calendar,
  Heart,
  Share2,
  ArrowLeft,
  Phone,
  Mail
} from 'lucide-react';
import Image from 'next/image';
import Link from 'next/link';
import { formatCurrency, formatDate } from '@/lib/utils';
import { notFound } from 'next/navigation';

interface PropertyDetailPageProps {
  params: {
    id: string;
  };
}

export default function PropertyDetailPage({ params }: PropertyDetailPageProps) {
  const [property, setProperty] = useState<Property | null>(null);
  const [loading, setLoading] = useState(true);
  const [_currentImageIndex, _setCurrentImageIndex] = useState(0);

  useEffect(() => {
    const fetchProperty = async () => {
      try {
        const foundProperty = await PropertyService.getPropertyById(params.id);
        setProperty(foundProperty);
      } catch (error) {
        console.error('Error fetching property:', error);
        notFound();
      } finally {
        setLoading(false);
      }
    };

    fetchProperty();
  }, [params.id]);

  const handleShare = () => {
    if (navigator.share && property) {
      navigator.share({
        title: property.name,
        text: `Property at ${property.address}`,
        url: window.location.href,
      });
    } else if (property) {
      navigator.clipboard.writeText(window.location.href);
    }
  };

  const handleFavorite = () => {
    console.log('Add to favorites:', property?.id);
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-background">
        <div className="container mx-auto px-4 py-6">
          <div className="animate-pulse space-y-6">
            <div className="h-8 bg-muted rounded w-1/4"></div>
            <div className="h-96 bg-muted rounded"></div>
            <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
              <div className="lg:col-span-2 space-y-4">
                <div className="h-6 bg-muted rounded w-3/4"></div>
                <div className="h-4 bg-muted rounded w-1/2"></div>
                <div className="h-20 bg-muted rounded"></div>
              </div>
              <div className="space-y-4">
                <div className="h-32 bg-muted rounded"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }

  if (!property) {
    return notFound();
  }

  const primaryImage = property.images?.[0];
  const src = property.images?.[0]?.file ? `http://host.docker.internal:5125${property.images[0].file}` : '/placeholder-property.svg';

  return (
    <div className="min-h-screen bg-background">
      {/* Header */}
      <div className="border-b bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
        <div className="container mx-auto px-4 py-4">
          <div className="flex items-center gap-4">
            <Button variant="ghost" size="sm" asChild>
              <Link href="/properties">
                <ArrowLeft className="h-4 w-4 mr-2" />
                Back to Properties
              </Link>
            </Button>
            <div className="flex items-center gap-2 ml-auto">
              <Button variant="outline" size="sm" onClick={handleFavorite}>
                <Heart className="h-4 w-4 mr-2" />
                Save
              </Button>
              <Button variant="outline" size="sm" onClick={handleShare}>
                <Share2 className="h-4 w-4 mr-2" />
                Share
              </Button>
            </div>
          </div>
        </div>
      </div>

      <div className="container mx-auto px-4 py-6">
        {/* Image Gallery */}
        <div className="relative aspect-[16/9] mb-6 rounded-lg overflow-hidden">
          <Image
            src={src}
            alt={`${property.name} - Property Image`}
            fill
            className="object-cover"
            priority
          />
          
          {/* Property Code Badge */}
          <Badge className="absolute top-4 left-4 bg-black/80 text-white">
            Code: {property.codeInternal}
          </Badge>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Main Content */}
          <div className="lg:col-span-2 space-y-6">
            {/* Property Header */}
            <div className="space-y-4">
              <div className="flex items-start justify-between">
                <div>
                  <h1 className="text-3xl font-bold mb-2">{property.name}</h1>
                  <div className="flex items-center text-muted-foreground mb-4">
                    <MapPin className="h-4 w-4 mr-1" />
                    <span>{property.address}</span>
                  </div>
                </div>
                <div className="text-right">
                  <div className="text-3xl font-bold text-primary">
                    {formatCurrency(property.price, 'USD')}
                  </div>
                </div>
              </div>

              {/* Property Info */}
              <div className="flex items-center gap-6 text-lg">
                <div className="flex items-center gap-2">
                  <Calendar className="h-5 w-5 text-muted-foreground" />
                  <span>Built {property.year}</span>
                </div>
                <Badge variant="outline">
                  Code: {property.codeInternal}
                </Badge>
              </div>
            </div>

            <Separator />

            {/* Property Traces */}
            {property.propertyTraces && property.propertyTraces.length > 0 && (
              <>
                <Separator />
                <div className="space-y-4">
                  <h2 className="text-xl font-semibold">Property History</h2>
                  <div className="space-y-3">
                    {property.propertyTraces.map((trace) => (
                      <div key={trace.id} className="border rounded-lg p-4">
                        <div className="flex justify-between items-start">
                          <div>
                            <h3 className="font-medium">{trace.name}</h3>
                            <p className="text-sm text-muted-foreground">
                              Sale Date: {formatDate(trace.dateSale)}
                            </p>
                          </div>
                          <div className="text-right">
                            <div className="font-bold">{formatCurrency(trace.value, 'USD')}</div>
                            <div className="text-sm text-muted-foreground">Tax: {formatCurrency(trace.tax, 'USD')}</div>
                          </div>
                        </div>
                      </div>
                    ))}
                  </div>
                </div>
              </>
            )}

            <Separator />

            {/* Property Details */}
            <div className="space-y-4">
              <h2 className="text-xl font-semibold">Property Details</h2>
              <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
                <div>
                  <div className="text-sm text-muted-foreground">Year Built</div>
                  <div className="font-medium">{property.year}</div>
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Internal Code</div>
                  <div className="font-medium">{property.codeInternal}</div>
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Listed Date</div>
                  <div className="font-medium">{formatDate(property.createdAt)}</div>
                </div>
                <div>
                  <div className="text-sm text-muted-foreground">Last Updated</div>
                  <div className="font-medium">{formatDate(property.updatedAt)}</div>
                </div>
              </div>
            </div>
          </div>

          {/* Sidebar */}
          <div className="space-y-6">
            {/* Owner Card */}
            {property.owner && (
              <Card>
                <CardHeader>
                  <CardTitle className="text-lg">Property Owner</CardTitle>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="flex items-center gap-3">
                    <Avatar className="h-12 w-12">
                      <AvatarImage 
                        src={property.owner.photo ? `http://localhost:5125${property.owner.photo}` : undefined} 
                        alt={property.owner.name} 
                      />
                      <AvatarFallback>
                        {property.owner.name.split(' ').map((n: string) => n[0]).join('')}
                      </AvatarFallback>
                    </Avatar>
                    <div>
                      <div className="font-semibold">{property.owner.name}</div>
                      <div className="text-sm text-muted-foreground">{property.owner.address}</div>
                    </div>
                  </div>
                  
                  <div className="space-y-2">
                    <Button className="w-full" size="sm">
                      <Phone className="h-4 w-4 mr-2" />
                      Contact Owner
                    </Button>
                    <Button variant="outline" className="w-full" size="sm">
                      <Mail className="h-4 w-4 mr-2" />
                      Send Message
                    </Button>
                  </div>
                </CardContent>
              </Card>
            )}

            {/* Quick Actions */}
            <Card>
              <CardHeader>
                <CardTitle className="text-lg">Interested?</CardTitle>
              </CardHeader>
              <CardContent className="space-y-3">
                <Button className="w-full">Schedule Viewing</Button>
                <Button variant="outline" className="w-full">Request Info</Button>
                <Button variant="outline" className="w-full">Make Offer</Button>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </div>
  );
}
