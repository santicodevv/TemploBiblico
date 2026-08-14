using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Evento
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    public DateOnly Fecha { get; private set; }
    public TimeOnly HoraInicio { get; private set; }
    public TimeOnly HoraFin { get; private set; }
    public string Responsable { get; private set; } = string.Empty;
    public string? Ubicacion { get; private set; }

    // Navigation properties
    private readonly List<Asistencia> _asistencias = [];
    public IReadOnlyCollection<Asistencia> Asistencias => _asistencias.AsReadOnly();

    private Evento() { } // EF Core

    public Evento(
        string titulo,
        DateOnly fecha,
        TimeOnly horaInicio,
        TimeOnly horaFin,
        string responsable,
        string? descripcion = null,
        string? ubicacion = null)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("El título del evento es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del evento es requerido.");

        if (horaFin <= horaInicio)
            throw new DomainException("La hora de fin debe ser posterior a la hora de inicio.");

        Id = Guid.NewGuid();
        Titulo = titulo;
        Fecha = fecha;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        Responsable = responsable;
        Descripcion = descripcion;
        Ubicacion = ubicacion;
    }

    public void Actualizar(
        string titulo,
        DateOnly fecha,
        TimeOnly horaInicio,
        TimeOnly horaFin,
        string responsable,
        string? descripcion,
        string? ubicacion)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("El título del evento es requerido.");

        if (string.IsNullOrWhiteSpace(responsable))
            throw new DomainException("El responsable del evento es requerido.");

        if (horaFin <= horaInicio)
            throw new DomainException("La hora de fin debe ser posterior a la hora de inicio.");

        Titulo = titulo;
        Fecha = fecha;
        HoraInicio = horaInicio;
        HoraFin = horaFin;
        Responsable = responsable;
        Descripcion = descripcion;
        Ubicacion = ubicacion;
    }
}
