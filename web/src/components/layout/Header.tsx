export function Header() {
  return (
    <header className="bg-white border-b border-slate-200 px-6 py-4">
      <div className="flex items-center justify-between">
        <h2 className="text-lg font-semibold text-slate-800">
          Sistema de Gestión
        </h2>
        <div className="flex items-center gap-4">
          {/* TODO: Add user menu and notifications */}
          <span className="text-sm text-slate-600">Usuario</span>
        </div>
      </div>
    </header>
  );
}
