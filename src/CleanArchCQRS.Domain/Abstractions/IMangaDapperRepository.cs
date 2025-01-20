using CleanArchCQRS.Domain.Models;

namespace CleanArchCQRS.Domain.Abstractions;

public interface IMangaDapperRepository
{
    Task<IEnumerable<Manga>> GetMangas();
    Task<Manga?> GetMangaById(int id);
}
