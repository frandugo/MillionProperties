import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  output: 'standalone',
  images: {
    remotePatterns: [
      {
        protocol: 'http',
        hostname: 'localhost',
        port: '5125',
        pathname: '/**',
      },
      {
        protocol: 'http',
        hostname: 'host.docker.internal',
        port: '5125',
        pathname: '/**',
      },
    ],
  },
};

export default nextConfig;
