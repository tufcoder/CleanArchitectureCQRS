using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class MangaRepository : IMangaRepository
{
    protected readonly AppDbContext _context;

    public MangaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Manga> Add(Manga manga)
    {
        if (manga is null)
            throw new ArgumentNullException(nameof(manga));

        await _context.Mangas.AddAsync(manga);
        return manga;
    }

    public async Task<IEnumerable<Manga>> GetAll()
    {
        var mangas = await _context.Mangas.ToListAsync();
        return mangas ?? Enumerable.Empty<Manga>();
    }

    public async Task<Manga> GetById(int id)
    {
        var manga = await _context.Mangas.FindAsync(id);

        if (manga is null)
            throw new InvalidOperationException("Manga not found");

        return manga;
    }

    public async Task<Manga> DeleteById(int id)
    {
        var manga = await GetById(id);

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
