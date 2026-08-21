using Iglesia.Application.Common.Interfaces;

namespace Iglesia.Api.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
    public DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);
}
