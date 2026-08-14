import { NavLink } from 'react-router-dom';

const navigation = [
  { name: 'Dashboard', href: '/' },
  { name: 'Miembros', href: '/miembros' },
  { name: 'Asistencia', href: '/asistencia' },
  { name: 'Calendario', href: '/calendario' },
  { name: 'Seguimiento Pastoral', href: '/seguimiento' },
  { name: 'Reportes', href: '/reportes' },
  { name: 'Administración', href: '/admin' },
];

export function Sidebar() {
  return (
    <aside className="w-64 bg-slate-800 text-white min-h-screen">
      <div className="p-4 border-b border-slate-700">
        <h1 className="text-xl font-bold">Iglesia System</h1>
      </div>
      <nav className="p-4">
        <ul className="space-y-2">
          {navigation.map((item) => (
            <li key={item.name}>
              <NavLink
                to={item.href}
                className={({ isActive }) =>
                  `block px-4 py-2 rounded-lg transition-colors ${
                    isActive
                      ? 'bg-slate-700 text-white'
                      : 'text-slate-300 hover:bg-slate-700 hover:text-white'
                  }`
                }
              >
                {item.name}
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
    </aside>
  );
}
