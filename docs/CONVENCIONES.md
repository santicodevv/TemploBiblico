# Convenciones de Código

## Backend (.NET)

### Nomenclatura
- **Clases/Interfaces**: PascalCase (`MemberService`, `IMemberRepository`)
- **Métodos**: PascalCase (`GetMemberById`)
- **Variables/Parámetros**: camelCase (`memberId`, `isActive`)
- **Constantes**: PascalCase (`MaxRetryCount`)
- **Interfaces**: Prefijo `I` (`IRepository`, `IService`)

### Estructura de Archivos
```
Feature/
├── Commands/
│   ├── CreateMemberCommand.cs
│   └── CreateMemberCommandHandler.cs
├── Queries/
│   ├── GetMemberQuery.cs
│   └── GetMemberQueryHandler.cs
└── Validators/
    └── CreateMemberCommandValidator.cs
```

### Patrones
- CQRS con MediatR para separar Commands y Queries
- Repository Pattern para acceso a datos
- Unit of Work implícito con EF Core
- Validación con FluentValidation en Application layer

## Frontend (React/TypeScript)

### Nomenclatura
- **Componentes**: PascalCase (`MemberCard.tsx`)
- **Hooks**: camelCase con prefijo `use` (`useMember.ts`)
- **Utilidades**: camelCase (`formatDate.ts`)
- **Types/Interfaces**: PascalCase (`Member`, `ApiResponse`)

### Estructura de Componentes
```tsx
// Imports
import { useState } from 'react'

// Types
interface Props {
  id: string
}

// Component
export function MemberCard({ id }: Props) {
  // hooks primero
  // handlers después
  // render al final
}
```

### Estilos
- Tailwind CSS para estilos
- Clases utilitarias inline
- Extraer componentes si se repiten estilos

## Git

### Commits
Formato: `tipo(scope): descripción`

Tipos:
- `feat`: Nueva funcionalidad
- `fix`: Corrección de bug
- `docs`: Documentación
- `refactor`: Refactorización
- `test`: Tests
- `chore`: Mantenimiento

Ejemplos:
```
feat(members): add member registration form
fix(auth): resolve token expiration issue
docs(readme): update setup instructions
```

### Branches
- `master` - Producción (protegido, solo PRs)
- `feature/nombre-modulo` - Nuevas funcionalidades
- `fix/nombre` - Correcciones

**Nombres de branches por módulo:**
| Módulo | Branch |
|--------|--------|
| Miembros | `feature/miembros-crud` |
| Asistencia + Eventos | `feature/asistencia-eventos` |
| Seguimiento Pastoral | `feature/seguimiento-pastoral` |
| Auth + Roles | `feature/auth-roles` |
| Reportes | `feature/reportes` |

### Branch Protection (master)
- No se permiten commits directos a `master`
- Todo cambio requiere Pull Request
- Requiere al menos 1 aprobación
- CI debe pasar (backend-ci, frontend-ci)
- Reviews obsoletos se descartan con nuevos commits

## Flujo de Trabajo

### Herramientas
- **Trello**: Gestión de tareas y asignaciones
- **GitHub**: Código, PRs y CI/CD
- **Issues de GitHub**: Documentación técnica de respaldo

### Proceso
```
1. Tomar card en Trello → Mover a "In Progress"
2. Crear branch: git checkout -b feature/nombre-modulo
3. Desarrollar y hacer commits
4. Abrir PR con título claro (ej: "Miembros: CRUD completo + foto")
5. CI corre automáticamente
6. Lead mueve card a "In Review"
7. Code review y aprobación
8. Merge a master
9. Lead mueve card a "Done"
```

### Título del PR
El título del PR debe coincidir con la card de Trello para fácil identificación:
- `Miembros: CRUD completo + foto`
- `Asistencia + Eventos: calendario y control`
- `Seguimiento Pastoral: registro y pendientes`
- `Auth + Roles: login y permisos`
- `Reportes: dashboard y exportación`

### CI/CD
Los workflows de GitHub Actions corren automáticamente:
- **backend-ci**: Build y validación del backend (.NET)
- **frontend-ci**: Build y validación del frontend (React)

Ambos deben pasar antes de poder hacer merge.
