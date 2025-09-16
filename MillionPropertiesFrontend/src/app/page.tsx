'use client';

export const dynamic = 'force-dynamic';

import { Button } from "@/components/ui/button";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { ArrowRight, Home, Search, Star, TrendingUp } from "lucide-react";
import Link from "next/link";
import { PropertyService } from "@/features/properties/services/property.service";
import { PropertyCard } from "@/features/properties/components/PropertyCard";
import { useState, useEffect } from "react";
import { Property } from "@/types/property";

export default function HomePage() {
  const [featuredProperties, setFeaturedProperties] = useState<Property[]>([]);

  useEffect(() => {
    const loadFeaturedProperties = async () => {
      try {
        const response = await PropertyService.getProperties({}, 1, 6);
        setFeaturedProperties(response.properties);
      } catch (error) {
        console.error('Failed to load featured properties:', error);
        setFeaturedProperties([]);
      }
    };
    
    if (typeof window !== 'undefined') {
      loadFeaturedProperties();
    }
  }, []);

  return (
    <div className="min-h-screen bg-background">
      <section className="relative bg-gradient-to-br from-primary/10 via-background to-secondary/10">
        <div className="container mx-auto px-4 py-20">
          <div className="max-w-4xl mx-auto text-center space-y-8">
            <div className="space-y-4">
              <Badge variant="secondary" className="mb-4">
                Million Properties Platform
              </Badge>
              <h1 className="text-4xl md:text-6xl font-bold tracking-tight">
                Find Your Perfect
                <span className="text-primary block">Dream Home</span>
              </h1>
              <p className="text-xl text-muted-foreground max-w-2xl mx-auto">
                Discover exceptional properties from our curated collection of homes, 
                apartments, and commercial spaces. Your next chapter starts here.
              </p>
            </div>
            
            <div className="flex flex-col sm:flex-row gap-4 justify-center">
              <Button asChild size="lg" className="gap-2">
                <Link href="/properties">
                  <Search className="h-5 w-5" />
                  Browse Properties
                  <ArrowRight className="h-5 w-5" />
                </Link>
              </Button>
              <Button variant="outline" size="lg" className="gap-2">
                <Home className="h-5 w-5" />
                List Your Property
              </Button>
            </div>
          </div>
        </div>
      </section>

      <section className="py-16 border-b">
        <div className="container mx-auto px-4">
          <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
            {[
              { label: "Properties Listed", value: "10,000+", icon: Home },
              { label: "Happy Customers", value: "5,000+", icon: Star },
              { label: "Cities Covered", value: "50+", icon: TrendingUp },
              { label: "Years Experience", value: "15+", icon: Badge },
            ].map((stat, index) => (
              <div key={index} className="text-center space-y-2">
                <stat.icon className="h-8 w-8 mx-auto text-primary" />
                <div className="text-3xl font-bold">{stat.value}</div>
                <div className="text-muted-foreground">{stat.label}</div>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className="py-16">
        <div className="container mx-auto px-4">
          <div className="text-center space-y-4 mb-12">
            <h2 className="text-3xl font-bold">Featured Properties</h2>
            <p className="text-muted-foreground max-w-2xl mx-auto">
              Explore our handpicked selection of premium properties that offer 
              exceptional value and outstanding features.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
            {featuredProperties.map((property) => (
              <PropertyCard
                key={property.id}
                property={property}
                onView={(_property) => {}}
                onFavorite={(property) => console.log('Favorite:', property.id)}
                onShare={(property) => {
                  if (navigator.share) {
                    navigator.share({
                      title: property.name,
                      text: `Property at ${property.address}`,
                      url: `${window.location.origin}/properties/${property.id}`,
                    });
                  }
                }}
              />
            ))}
          </div>

          <div className="text-center">
            <Button asChild variant="outline" size="lg">
              <Link href="/properties">
                View All Properties
                <ArrowRight className="h-5 w-5 ml-2" />
              </Link>
            </Button>
          </div>
        </div>
      </section>

      <section className="py-16 bg-muted/50">
        <div className="container mx-auto px-4">
          <div className="text-center space-y-4 mb-12">
            <h2 className="text-3xl font-bold">Why Choose Million Properties?</h2>
            <p className="text-muted-foreground max-w-2xl mx-auto">
              We provide comprehensive real estate solutions with cutting-edge technology 
              and personalized service.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {[
              {
                title: "Advanced Search",
                description: "Find properties with our intelligent filtering system and real-time updates.",
                icon: Search,
              },
              {
                title: "Expert Agents",
                description: "Work with certified professionals who understand your local market.",
                icon: Star,
              },
              {
                title: "Secure Transactions",
                description: "Complete peace of mind with our secure and transparent process.",
                icon: Home,
              },
            ].map((feature, index) => (
              <Card key={index}>
                <CardHeader>
                  <feature.icon className="h-12 w-12 text-primary mb-4" />
                  <CardTitle>{feature.title}</CardTitle>
                </CardHeader>
                <CardContent>
                  <CardDescription>{feature.description}</CardDescription>
                </CardContent>
              </Card>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
}
