using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Infrastructure.Context;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class MangaRepository : IMangaRepository
{
    private readonly AppDbContext _context;

    public MangaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Manga?> GetByIdAsync(int id)
    {
        var manga = await _context.Mangas.FindAsync(id);

        return manga;
    }

    public async Task<Manga> AddAsync(Manga manga)
    {
        if (manga is null)
            throw new ArgumentNullException(nameof(manga));

        await _context.Mangas.AddAsync(manga);
        return manga;
    }

    public async Task<Manga?> DeleteByIdAsync(int id)
    {
        var manga = await GetByIdAsync(id);

        if (manga is null)
            return null;

        _context.Mangas.Remove(manga);

        return manga;
    }

    public void Update(Manga manga)
    {
        if (manga is null)
            throw new ArgumentNullException(nameof(manga));

        _context.Mangas.Update(manga);
    }
}
