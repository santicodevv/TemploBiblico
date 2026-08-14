# Iglesia System

Sistema de gestión para iglesia con backend .NET y frontend React.

## Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/sql-server) (local o remoto)

## Estructura del Proyecto

```
├── backend/                     # API .NET (Clean Architecture)
│   ├── src/
│   │   ├── Iglesia.Domain/      # Entidades y lógica de dominio
│   │   ├── Iglesia.Application/ # Casos de uso, CQRS con MediatR
│   │   ├── Iglesia.Infrastructure/ # EF Core, repositorios
│   │   └── Iglesia.Api/         # Controllers, configuración
│   └── Iglesia.sln
├── web/                         # Frontend React + Vite + TypeScript
│   └── src/
│       ├── components/          # Componentes reutilizables
│       ├── pages/               # Páginas/rutas
│       ├── lib/                 # Utilidades (api client)
│       └── types/               # TypeScript types
├── docs/                        # Documentación
│   ├── ARQUITECTURA.md
│   └── CONVENCIONES.md
└── .github/workflows/           # CI/CD
```

## Configuración Inicial

### 1. Base de Datos

Configura la cadena de conexión en `backend/src/Iglesia.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=IglesiaDb_Dev;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 2. Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet run --project src/Iglesia.Api
```

La API estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- OpenAPI: `http://localhost:5000/openapi/v1.json`

### 3. Frontend

```bash
cd web
npm install
cp .env.example .env  # Configurar variables de entorno
npm run dev
```

La aplicación estará disponible en `http://localhost:5173`.

## Scripts Disponibles

### Backend
| Comando | Descripción |
|---------|-------------|
| `dotnet build` | Compilar solución |
| `dotnet test` | Ejecutar tests |
| `dotnet run --project src/Iglesia.Api` | Ejecutar API |
| `dotnet ef migrations add <nombre> -p src/Iglesia.Infrastructure -s src/Iglesia.Api` | Crear migración |
| `dotnet ef database update -p src/Iglesia.Infrastructure -s src/Iglesia.Api` | Aplicar migraciones |

### Frontend
| Comando | Descripción |
|---------|-------------|
| `npm run dev` | Servidor de desarrollo |
| `npm run build` | Build de producción |
| `npm run preview` | Preview del build |

## Módulos del Sistema

- **Dashboard**: Vista general y métricas
- **Miembros**: CRUD de miembros de la iglesia
- **Asistencia**: Control de asistencia a eventos
- **Calendario**: Eventos y actividades
- **Seguimiento Pastoral**: Visitas y seguimiento
- **Reportes**: Estadísticas y reportes
- **Administración**: Gestión de usuarios

## Documentación Adicional

- [Arquitectura del Sistema](docs/ARQUITECTURA.md)
- [Convenciones de Código](docs/CONVENCIONES.md)
