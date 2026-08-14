using Iglesia.Domain.Enums;
using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

/// <summary>
/// Representa un miembro de la iglesia.
/// </summary>
/// <remarks>
/// TODO: Actualmente un miembro solo puede pertenecer a UN ministerio (relación 1-N).
/// Si el equipo decide que un miembro puede estar en varios ministerios, se debe crear
/// una tabla intermedia MiembroMinisterio (relación N-N). Discutir con el equipo antes
/// de implementar cambios.
/// </remarks>
public class Miembro
{
    public Guid Id { get; private set; }
    public string Nombres { get; private set; } = string.Empty;
    public string Apellidos { get; private set; } = string.Empty;
    public DateOnly? FechaNacimiento { get; private set; }
    public string? Telefono { get; private set; }
    public string? Direccion { get; private set; }
    public string? Email { get; private set; }
    public DateOnly? FechaConversion { get; private set; }
    public DateOnly? FechaBautismo { get; private set; }
    public EstadoMiembro Estado { get; private set; }
    public string? Foto { get; private set; }

    // Relación con Ministerio (opcional, 1-N)
    public Guid? MinisterioId { get; private set; }
    public Ministerio? Ministerio { get; private set; }

    // Navigation properties
    private readonly List<Asistencia> _asistencias = [];
    public IReadOnlyCollection<Asistencia> Asistencias => _asistencias.AsReadOnly();

    private readonly List<Seguimiento> _seguimientos = [];
    public IReadOnlyCollection<Seguimiento> Seguimientos => _seguimientos.AsReadOnly();

    private Miembro() { } // EF Core

    public Miembro(
        string nombres,
        string apellidos,
        DateOnly? fechaNacimiento = null,
        string? telefono = null,
        string? direccion = null,
        string? email = null)
    {
        if (string.IsNullOrWhiteSpace(nombres))
            throw new DomainException("Los nombres del miembro son requeridos.");

        if (string.IsNullOrWhiteSpace(apellidos))
            throw new DomainException("Los apellidos del miembro son requeridos.");

        Id = Guid.NewGuid();
        Nombres = nombres;
        Apellidos = apellidos;
        FechaNacimiento = fechaNacimiento;
        Telefono = telefono;
        Direccion = direccion;
        Email = email;
        Estado = EstadoMiembro.Activo;
    }

    public void ActualizarDatosPersonales(
        string nombres,
        string apellidos,
        DateOnly? fechaNacimiento,
        string? telefono,
        string? direccion,
        string? email)
    {
        if (string.IsNullOrWhiteSpace(nombres))
            throw new DomainException("Los nombres del miembro son requeridos.");

        if (string.IsNullOrWhiteSpace(apellidos))
            throw new DomainException("Los apellidos del miembro son requeridos.");

        Nombres = nombres;
        Apellidos = apellidos;
        FechaNacimiento = fechaNacimiento;
        Telefono = telefono;
        Direccion = direccion;
        Email = email;
    }

    public void RegistrarConversion(DateOnly fecha)
    {
        FechaConversion = fecha;
    }

    public void RegistrarBautismo(DateOnly fecha)
    {
        if (FechaConversion.HasValue && fecha < FechaConversion.Value)
            throw new DomainException("La fecha de bautismo no puede ser anterior a la fecha de conversión.");

        FechaBautismo = fecha;
    }

    public void AsignarMinisterio(Guid? ministerioId)
    {
        MinisterioId = ministerioId;
    }

    public void CambiarEstado(EstadoMiembro estado)
    {
        Estado = estado;
    }

    public void ActualizarFoto(string? urlFoto)
    {
        Foto = urlFoto;
    }
}
