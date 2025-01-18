namespace CleanArchCQRS.Domain.Models;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
