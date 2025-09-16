# MillionProperties

## Docker Setup

### Backend (API)
```bash
cd MillionProperties
docker build -t million-properties-api .
docker run -p 8080:8080 million-properties-api
```

### Frontend
```bash
cd MillionPropertiesFrontend
docker build -t million-properties-frontend .
docker run -p 3000:3000 million-properties-frontend
```

### Run Both (Docker Compose)
```bash
cd MillionPropertiesFrontend
docker-compose up
```
