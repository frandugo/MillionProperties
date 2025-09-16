# Million Properties Frontend

A modern Next.js application built with TypeScript, Tailwind CSS, and **Docker-first architecture**.

## 🐳 Docker-First Approach

This project is designed to run in Docker containers for consistent development and production environments. All configurations are optimized for containerized deployment.

## Tech Stack

- **Next.js 15.5.2** - React framework with App Router
- **TypeScript** - Type-safe JavaScript
- **Tailwind CSS 4** - Utility-first CSS framework
- **shadcn/ui** - Modern component library
- **Docker** - Primary deployment method
- **ESLint** - Code linting and formatting

## 🚀 Quick Start with Docker

### Prerequisites

- **Docker** and **Docker Compose** (required)
- Node.js 18+ (optional, for local development only)

### Run with Docker Compose (Recommended)

1. **Production mode:**
```bash
docker-compose up -d
```

2. **Development mode with hot reload:**
```bash
docker-compose --profile dev up -d
```

The application will be available at:
- Production: [http://localhost:3000](http://localhost:3000)
- Development: [http://localhost:3001](http://localhost:3001)

## Docker Setup

### Production Build

1. Build the Docker image:
```bash
docker build -t million-properties-frontend .
```

2. Run the container:
```bash
docker run -p 3000:3000 million-properties-frontend
```

### Using Docker Compose (Recommended)

1. **Production mode:**
```bash
docker-compose up -d
```

2. **Development mode:**
```bash
docker-compose --profile dev up -d
```

The application will be available at:
- Production: [http://localhost:3000](http://localhost:3000)
- Development: [http://localhost:3001](http://localhost:3001)

### Docker Commands

```bash
# Build and run with docker-compose
docker-compose up --build

# Run in background
docker-compose up -d

# Stop containers
docker-compose down

# View logs
docker-compose logs -f

# Rebuild containers
docker-compose up --build --force-recreate
```

## Project Structure

```
├── src/
│   ├── app/
│   │   ├── globals.css
│   │   ├── layout.tsx
│   │   └── page.tsx
├── public/
├── Dockerfile              # Production Docker image
├── Dockerfile.dev          # Development Docker image
├── docker-compose.yml      # Docker Compose configuration
├── .dockerignore          # Docker ignore patterns
├── next.config.ts         # Next.js configuration
├── tailwind.config.ts     # Tailwind CSS configuration
└── package.json
```

## Available Scripts

- `npm run dev` - Start development server with Turbopack
- `npm run build` - Build production application
- `npm start` - Start production server
- `npm run lint` - Run ESLint

## Docker Configuration

The project includes optimized Docker configurations:

- **Multi-stage build** for smaller production images
- **Standalone output** for efficient containerization
- **Non-root user** for security
- **Development and production** Docker configurations
- **Docker Compose** for easy orchestration

## Environment Variables

Create a `.env.local` file for local environment variables:

```env
# Add your environment variables here
NEXT_PUBLIC_API_URL=http://localhost:8080
```

## Learn More

- [Next.js Documentation](https://nextjs.org/docs)
- [Docker Documentation](https://docs.docker.com/)
- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
