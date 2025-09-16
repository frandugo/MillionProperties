# Docker Deployment Guide

## 🐳 Million Properties Frontend - Docker Setup

This application is built with a Docker-first approach for consistent development and production environments.

## Quick Commands

### Development
```bash
# Start development environment with hot reload
docker-compose --profile dev up -d

# View logs
docker-compose logs -f nextjs-dev

# Stop development environment
docker-compose --profile dev down
```

### Production
```bash
# Start production environment
docker-compose up -d

# View logs
docker-compose logs -f nextjs-app

# Stop production environment
docker-compose down
```

### Manual Docker Commands
```bash
# Build production image
docker build -t million-properties-frontend .

# Run production container
docker run -d -p 3000:3000 --name million-properties million-properties-frontend

# Build development image
docker build -f Dockerfile.dev -t million-properties-frontend:dev .

# Run development container with volume mounting
docker run -d -p 3001:3000 -v $(pwd):/app -v /app/node_modules --name million-properties-dev million-properties-frontend:dev
```

## Environment Configuration

### Production Environment Variables
Create a `.env.production` file:
```env
NODE_ENV=production
NEXT_PUBLIC_API_URL=https://api.millionproperties.com
NEXT_TELEMETRY_DISABLED=1
```

### Development Environment Variables
Create a `.env.local` file:
```env
NODE_ENV=development
NEXT_PUBLIC_API_URL=http://localhost:8080
```

## Docker Architecture

### Multi-Stage Build Process
1. **Base Stage**: Node.js 18 Alpine base image
2. **Dependencies Stage**: Install production dependencies
3. **Builder Stage**: Build the Next.js application
4. **Runner Stage**: Production runtime with minimal footprint

### Security Features
- Non-root user execution
- Minimal attack surface with Alpine Linux
- Standalone Next.js output for reduced image size
- Health checks and proper signal handling

## Troubleshooting

### Port Conflicts
If port 3000 is already in use:
```bash
# Check what's using the port
lsof -i :3000

# Use different port
docker run -p 3002:3000 million-properties-frontend
```

### Volume Issues (Development)
```bash
# Reset volumes
docker-compose down -v
docker-compose --profile dev up -d --build
```

### Container Logs
```bash
# View real-time logs
docker logs -f <container-name>

# View last 100 lines
docker logs --tail 100 <container-name>
```

## Performance Optimization

### Image Size Optimization
- Multi-stage builds reduce final image size by ~70%
- `.dockerignore` excludes unnecessary files
- Standalone output eliminates unused dependencies

### Runtime Optimization
- Production builds use optimized bundles
- Static assets are properly cached
- Health checks ensure container reliability

## Deployment Platforms

This Docker setup is compatible with:
- **Docker Compose** (local development)
- **Kubernetes** (production orchestration)
- **AWS ECS/Fargate** (managed containers)
- **Google Cloud Run** (serverless containers)
- **Azure Container Instances** (cloud containers)
- **Vercel** (with Docker builds)
- **Railway/Render** (container platforms)
