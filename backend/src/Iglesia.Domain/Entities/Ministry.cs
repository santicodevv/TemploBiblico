using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Ministry
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Leader { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<Member> _members = [];
    public IReadOnlyCollection<Member> Members => _members.AsReadOnly();

    private Ministry() { }

    public Ministry(string name, string leader, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Ministry name is required.");

        if (string.IsNullOrWhiteSpace(leader))
            throw new DomainException("Ministry leader is required.");

        Id = Guid.NewGuid();
        Name = name;
        Leader = leader;
        Description = description;
    }

    public void Update(string name, string leader, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Ministry name is required.");

        if (string.IsNullOrWhiteSpace(leader))
            throw new DomainException("Ministry leader is required.");

        Name = name;
        Leader = leader;
        Description = description;
    }
}
