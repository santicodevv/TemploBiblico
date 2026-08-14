using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Ministerio
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Responsable { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }

    // Navigation properties
    private readonly List<Miembro> _miembros = [];
    public IReadOnlyCollection<Miembro> Miembros => _miembros.AsReadOnly();

    private Ministerio() { } // EF Core

    public Ministerio(string nombre, string responsable, string? descripcion = null)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del ministerio es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del ministerio es requerido.");

        Id = Guid.NewGuid();
        Nombre = nombre;
        Responsable = responsable;
        Descripcion = descripcion;
    }

    public void Actualizar(string nombre, string responsable, string? descripcion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del ministerio es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del ministerio es requerido.");

        Nombre = nombre;
        Responsable = responsable;
        Descripcion = descripcion;
    }
}
