using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Domain.Models.Enums;

namespace CleanArchCQRS.Application;

public class CreateMangaHandlerWithoutMediatR : ICreateMangaHandler
{
    private readonly IMangaRepository _repository;

    public CreateMangaHandlerWithoutMediatR(IMangaRepository repository)
    {
        _repository = repository;
    }

    public CreateMangaResponse Handle(CreateMangaRequest command)
    {
        var manga = new Manga(
            command.Title!,
            command.Price,
            command.Genres,
            command.ReleaseDate,
            command.Publisher!,
            command.IsActive
            );

        _repository.AddAsync(manga);

        return new CreateMangaResponse
        {
            Id = manga.Id,
            Title = manga.Title!,
            Genres = manga.Genres,
            ReleaseDate = manga.ReleaseDate,
            Publisher = manga.Publisher!,
            IsActive = manga.IsActive,
        };
    }
}

public interface ICreateMangaHandler
{
    CreateMangaResponse Handle(CreateMangaRequest command);
}

public class CreateMangaRequest
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<MangaGenres> Genres { get; set; } = [];
    public DateTime ReleaseDate { get; set; }
    public string? Publisher { get; set; }
    public bool? IsActive { get; set; }
}

public class CreateMangaResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<MangaGenres> Genres { get; set; } = [];
    public DateTime ReleaseDate { get; set; }
    public string? Publisher { get; set; }
    public bool? IsActive { get; set; }
}
