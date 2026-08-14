# Arquitectura del Sistema

## Backend - Clean Architecture

El backend sigue los principios de Clean Architecture con las siguientes capas:

```
┌─────────────────────────────────────────┐
│              Iglesia.Api                │  ← Controllers, Middleware, DI
├─────────────────────────────────────────┤
│          Iglesia.Infrastructure         │  ← EF Core, Repositorios, Servicios externos
├─────────────────────────────────────────┤
│          Iglesia.Application            │  ← Casos de uso, DTOs, Validaciones
├─────────────────────────────────────────┤
│            Iglesia.Domain               │  ← Entidades, Interfaces, Value Objects
└─────────────────────────────────────────┘
```

### Iglesia.Domain
- Entidades de negocio
- Interfaces de repositorios
- Value Objects
- Excepciones de dominio
- Sin dependencias externas

### Iglesia.Application
- Casos de uso (Commands/Queries con MediatR)
- DTOs y mapeos (AutoMapper)
- Validaciones (FluentValidation)
- Interfaces de servicios
- Depende solo de Domain

### Iglesia.Infrastructure
- Implementación de repositorios (EF Core)
- DbContext y configuraciones
- Servicios externos (email, storage)
- Depende de Application

### Iglesia.Api
- Controllers REST
- Configuración de DI
- Middleware (auth, error handling)
- Depende de Application e Infrastructure

## Frontend - React + Vite

```
web/
├── src/
│   ├── components/     # Componentes reutilizables
│   ├── pages/          # Páginas/rutas
│   ├── hooks/          # Custom hooks
│   ├── services/       # Llamadas API (axios)
│   ├── stores/         # Estado global
│   └── types/          # TypeScript types
```

### Stack
- **React 18** - UI
- **TypeScript** - Tipado
- **Tailwind CSS v4** - Estilos
- **React Router** - Navegación
- **TanStack Query** - Estado servidor
- **Axios** - HTTP client

## Flujo de Datos

```
Frontend → API Controller → MediatR Handler → Repository → Database
                ↓
           Validación
           (FluentValidation)
```
