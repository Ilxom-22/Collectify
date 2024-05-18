namespace Collectify.Domain.Common;

public class IAuditableEntity : IEntity
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
}