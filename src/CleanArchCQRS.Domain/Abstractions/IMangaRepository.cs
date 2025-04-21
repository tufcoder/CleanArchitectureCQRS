using CleanArchCQRS.Domain.Models;

namespace CleanArchCQRS.Domain.Abstractions;

public interface IMangaRepository
{
    Task<Manga?> GetByIdAsync(int id);
    Task<Manga> AddAsync(Manga manga);
    void Update(Manga manga);
    Task<Manga?> DeleteByIdAsync(int id);
}
