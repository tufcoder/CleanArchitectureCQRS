namespace CleanArchCQRS.Domain.Abstractions;

public abstract class EntityBase
{
    public int Id { get; protected set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
