using System.Data;

using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using Dapper;

namespace CleanArchCQRS.Infrastructure.Repositories;

public class MangaDapperRepository : IMangaDapperRepository
{
    private readonly IDbConnection _connection;

    public MangaDapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<Manga>> GetMangas()
    {
        var sql = "SELECT * FROM Mangas";
        var mangas = await _connection.QueryAsync<Manga>(sql);

        return mangas;
    }

    public async Task<Manga?> GetMangaById(int id)
    {
        var sql = "SELECT * FROM Mangas WHERE Id = @Id";
        var manga = await _connection.QueryFirstOrDefaultAsync<Manga>(sql, new { Id = id });
        
        return manga;
    }
}
