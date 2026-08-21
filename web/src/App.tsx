import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { MainLayout } from './components/layout';
import {
  Dashboard,
  Miembros,
  Asistencia,
  Calendario,
  Seguimiento,
  Reportes,
  Admin,
} from './pages';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // 5 minutes
      retry: 1,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<MainLayout />}>
            <Route index element={<Dashboard />} />
            <Route path="miembros" element={<Miembros />} />
            <Route path="asistencia" element={<Asistencia />} />
            <Route path="calendario" element={<Calendario />} />
            <Route path="seguimiento" element={<Seguimiento />} />
            <Route path="reportes" element={<Reportes />} />
            <Route path="admin" element={<Admin />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
