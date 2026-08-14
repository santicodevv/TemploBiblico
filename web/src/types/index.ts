// TODO: Add TypeScript types as features are implemented

export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

// Placeholder types - implement as needed
export interface Miembro {
  id: string;
  nombres: string;
  apellidos: string;
  fechaNacimiento?: string;
  telefono?: string;
  direccion?: string;
  email?: string;
  fechaConversion?: string;
  fechaBautismo?: string;
  estado: 'Activo' | 'Inactivo';
  foto?: string;
  ministerioId?: string;
}

export interface Evento {
  id: string;
  titulo: string;
  descripcion?: string;
  fecha: string;
  horaInicio: string;
  horaFin: string;
  responsable: string;
  ubicacion?: string;
}

export interface Asistencia {
  id: string;
  miembroId: string;
  eventoId: string;
  fecha: string;
  estado: 'Presente' | 'Ausente' | 'Excusado';
  observacion?: string;
}

export interface Seguimiento {
  id: string;
  miembroId: string;
  fecha: string;
  motivo: string;
  observacion?: string;
  proximaVisita?: string;
  responsable: string;
}

export interface Ministerio {
  id: string;
  nombre: string;
  responsable: string;
  descripcion?: string;
}
