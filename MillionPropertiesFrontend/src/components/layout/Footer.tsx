'use client';

import { Home, Mail, Phone, MapPin } from "lucide-react";
import Link from "next/link";

export function Footer() {
  return (
    <footer className="bg-muted/50 border-t">
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div className="space-y-4">
            <div className="flex items-center gap-2">
              <Home className="h-6 w-6 text-primary" />
              <span className="font-bold text-xl">Million Properties</span>
            </div>
            <p className="text-sm text-muted-foreground">
              Your trusted partner in finding the perfect property. 
              We connect dreams with reality through exceptional real estate services.
            </p>
            <div className="flex items-center gap-2 text-sm text-muted-foreground">
              <Phone className="h-4 w-4" />
              <span>+1 (555) 123-4567</span>
            </div>
            <div className="flex items-center gap-2 text-sm text-muted-foreground">
              <Mail className="h-4 w-4" />
              <span>info@millionproperties.com</span>
            </div>
            <div className="flex items-center gap-2 text-sm text-muted-foreground">
              <MapPin className="h-4 w-4" />
              <span>123 Real Estate Ave, City, State 12345</span>
            </div>
          </div>

          <div className="space-y-4">
            <h3 className="font-semibold">Quick Links</h3>
            <nav className="flex flex-col gap-2">
              <Link href="/properties" className="text-sm text-muted-foreground hover:text-primary">
                Browse Properties
              </Link>
              <Link href="/agents" className="text-sm text-muted-foreground hover:text-primary">
                Our Agents
              </Link>
              <Link href="/about" className="text-sm text-muted-foreground hover:text-primary">
                About Us
              </Link>
              <Link href="/contact" className="text-sm text-muted-foreground hover:text-primary">
                Contact
              </Link>
            </nav>
          </div>

          <div className="space-y-4">
            <h3 className="font-semibold">Services</h3>
            <nav className="flex flex-col gap-2">
              <Link href="/buy" className="text-sm text-muted-foreground hover:text-primary">
                Buy Property
              </Link>
              <Link href="/sell" className="text-sm text-muted-foreground hover:text-primary">
                Sell Property
              </Link>
              <Link href="/rent" className="text-sm text-muted-foreground hover:text-primary">
                Rent Property
              </Link>
              <Link href="/valuation" className="text-sm text-muted-foreground hover:text-primary">
                Property Valuation
              </Link>
            </nav>
          </div>

          <div className="space-y-4">
            <h3 className="font-semibold">Legal</h3>
            <nav className="flex flex-col gap-2">
              <Link href="/privacy" className="text-sm text-muted-foreground hover:text-primary">
                Privacy Policy
              </Link>
              <Link href="/terms" className="text-sm text-muted-foreground hover:text-primary">
                Terms of Service
              </Link>
              <Link href="/cookies" className="text-sm text-muted-foreground hover:text-primary">
                Cookie Policy
              </Link>
              <Link href="/disclaimer" className="text-sm text-muted-foreground hover:text-primary">
                Disclaimer
              </Link>
            </nav>
          </div>
        </div>

        <div className="border-t mt-8 pt-8 text-center text-sm text-muted-foreground">
          <p>&copy; 2024 Million Properties. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}
