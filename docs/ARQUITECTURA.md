# System Architecture

## Backend - Clean Architecture

The backend follows Clean Architecture principles with the following layers:

```
┌─────────────────────────────────────────┐
│              Iglesia.Api                │  ← Controllers, Middleware, DI
├─────────────────────────────────────────┤
│          Iglesia.Infrastructure         │  ← EF Core, Repositories, External Services
├─────────────────────────────────────────┤
│          Iglesia.Application            │  ← Use Cases, DTOs, Validations
├─────────────────────────────────────────┤
│            Iglesia.Domain               │  ← Entities, Interfaces, Value Objects
└─────────────────────────────────────────┘
```

### Iglesia.Domain
- Business entities (pure C#, no external dependencies)
- Repository interfaces
- Value Objects
- Domain exceptions
- **Zero NuGet dependencies**

### Iglesia.Application
- Use cases (Commands/Queries with MediatR)
- DTOs and mappings (AutoMapper)
- Validations (FluentValidation)
- Service interfaces
- Depends only on Domain

### Iglesia.Infrastructure
- Repository implementations (EF Core)
- DbContext and configurations
- External services (email, storage)
- Depends on Application

### Iglesia.Api
- REST Controllers
- DI configuration
- Middleware (auth, error handling)
- Depends on Application and Infrastructure

## Domain Model

```
┌─────────────┐       ┌─────────────┐
│   Member    │──────▶│   Ministry  │
└─────────────┘  N:1  └─────────────┘
      │
      │ 1:N
      ▼
┌─────────────┐       ┌─────────────┐
│  Attendance │◀──────│    Event    │
└─────────────┘  N:1  └─────────────┘

┌─────────────┐
│  FollowUp   │──────▶ Member (N:1)
└─────────────┘
```

### Entities
| Entity | Description |
|--------|-------------|
| `Member` | Church member with personal data, conversion/baptism dates |
| `Ministry` | Church ministry (worship, youth, etc.) |
| `Event` | Church event or service |
| `Attendance` | Member attendance record for an event |
| `FollowUp` | Pastoral follow-up visit record |

### Enums
| Enum | Values |
|------|--------|
| `MemberStatus` | Active, Inactive |
| `AttendanceStatus` | Present, Absent, Excused |

## Frontend - React + Vite

```
web/
├── src/
│   ├── components/     # Reusable components
│   │   └── layout/     # Layout components (Sidebar, Header)
│   ├── pages/          # Route pages
│   ├── hooks/          # Custom hooks
│   ├── lib/            # Utilities (api client)
│   └── types/          # TypeScript types
```

### Stack
- **React 19** - UI
- **TypeScript** - Type safety
- **Tailwind CSS v4** - Styling
- **React Router** - Navigation
- **TanStack Query** - Server state
- **Axios** - HTTP client

## Data Flow

```
Frontend → API Controller → MediatR Handler → Repository → Database
                ↓
           Validation
           (FluentValidation)
```

## Authentication Flow (TODO)

```
1. User sends credentials to /api/auth/login
2. API validates and generates JWT token
3. Frontend stores token in localStorage
4. All subsequent requests include Bearer token
5. API validates token on each request
```
