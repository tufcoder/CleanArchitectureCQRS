using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Infrastructure.Context;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private IMangaRepository? _repository;
    
    public IMangaRepository MangaRepository
    {
        get
        {
            return _repository = _repository ?? new MangaRepository(_context);
        }
    }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
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
