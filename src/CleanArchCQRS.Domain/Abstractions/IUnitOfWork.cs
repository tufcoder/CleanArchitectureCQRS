namespace CleanArchCQRS.Domain.Abstractions;

public interface IUnitOfWork
{
    IMangaRepository MangaRepository { get; }
    Task CommitAsync();
}
