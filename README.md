# Pendaftran Mahasiswa

Pendaftran Mahasiswa is a full-stack web application for managing student registration. It consists of a Vue 3 frontend powered by Vite and an ASP.NET Core Web API backend with Entity Framework Core and SQL Server.

## Tech Stack

- Frontend: Vue 3, Vite, Tailwind CSS
- Backend: ASP.NET Core 8.0, Entity Framework Core, SQL Server
- API Documentation: Swagger (Swashbuckle)
- Development Proxy: ASP.NET Core SpaProxy to integrate Vue dev server

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js and npm](https://nodejs.org/)
- SQL Server instance for database

## Setup and Installation

### Backend

1. Configure your SQL Server connection string in `pendaftran-mahasiswa.Server/appsettings.json` under `DefaultConnection`.
2. Apply database migrations:

```sh
cd pendaftran-mahasiswa.Server
dotnet ef database update
```

### Frontend

1. Install npm dependencies:

```sh
cd pendaftran-mahasiswa.client
npm install
```

## Running the Application

### Development Mode

Run the backend with SPA proxy enabled, which will automatically start the Vue dev server:

```sh
cd pendaftran-mahasiswa.Server
dotnet run
```

The backend will be available at `https://localhost:5001` (or configured port), and the Vue frontend will be proxied.

### Production Build

1. Build the frontend:

```sh
cd pendaftran-mahasiswa.client
npm run build
```

2. Publish the backend, which will serve the built frontend from `wwwroot`:

```sh
cd pendaftran-mahasiswa.Server
dotnet publish -c Release
```

3. Deploy the published backend to your hosting environment.

## API Documentation

Swagger UI is available in development mode at:

```
https://localhost:5001/swagger
```

Use this to explore and test the backend API endpoints.

## Project Structure

```
/pendaftran-mahasiswa.client    # Vue 3 frontend source code
/pendaftran-mahasiswa.Server    # ASP.NET Core backend source code
README.md                      # This documentation file
```

