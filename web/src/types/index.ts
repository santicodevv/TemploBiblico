// API Response types
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

// Domain types
export interface Member {
  id: string;
  firstName: string;
  lastName: string;
  birthDate?: string;
  phone?: string;
  address?: string;
  email?: string;
  conversionDate?: string;
  baptismDate?: string;
  status: 'Active' | 'Inactive';
  photoUrl?: string;
  ministryId?: string;
}

export interface Event {
  id: string;
  title: string;
  description?: string;
  date: string;
  startTime: string;
  endTime: string;
  organizer: string;
  location?: string;
}

export interface Attendance {
  id: string;
  memberId: string;
  eventId: string;
  date: string;
  status: 'Present' | 'Absent' | 'Excused';
  notes?: string;
}

export interface FollowUp {
  id: string;
  memberId: string;
  date: string;
  reason: string;
  notes?: string;
  nextVisitDate?: string;
  assignedTo: string;
}

export interface Ministry {
  id: string;
  name: string;
  leader: string;
  description?: string;
}
