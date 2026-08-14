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
- `main` - Producción
- `develop` - Desarrollo
- `feature/nombre` - Nuevas funcionalidades
- `fix/nombre` - Correcciones
