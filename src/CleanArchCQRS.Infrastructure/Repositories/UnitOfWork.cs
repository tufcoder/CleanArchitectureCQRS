using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Infrastructure.Context;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IMangaRepository? _repository;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IMangaRepository MangaRepository
    {
        get
        {
            return _repository = _repository ?? new MangaRepository(_context);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
