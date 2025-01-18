using CleanArchCQRS.Domain.Models;

namespace CleanArchCQRS.Domain.Abstractions;

public interface IMangaRepository
{
    Task<IEnumerable<Manga>> GetAll();
    Task<Manga> GetById(int id);
    Task<Manga> Add(Manga manga);
    void Update(Manga manga);
    Task<Manga> DeleteById(int id);
}
