using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using Dapper;

using System.Data;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class MangaDapperRepository : IMangaDapperRepository
{
    private readonly IDbConnection _connection;

    public MangaDapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Manga>> GetMangasAsync()
    {
        var query = "SELECT * FROM Mangas";
        var mangas = await _connection.QueryAsync<Manga>(query);

        return mangas;
    }

    public async Task<Manga> GetMangaByIdAsync(int id)
    {
        var query = "SELECT * FROM Mangas WHERE Id = @Id";
        var manga = await _connection.QueryFirstOrDefaultAsync<Manga>(query, new { Id = id })
            ?? throw new InvalidOperationException("Manga not found");

        return manga;
    }
}
