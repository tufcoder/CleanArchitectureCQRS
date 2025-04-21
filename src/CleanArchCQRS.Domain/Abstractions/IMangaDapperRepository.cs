using CleanArchCQRS.Domain.Models;

namespace CleanArchCQRS.Domain.Abstractions;

public interface IMangaDapperRepository
{
    Task<IEnumerable<Manga>> GetMangasAsync();
    Task<Manga> GetMangaByIdAsync(int id);
}
