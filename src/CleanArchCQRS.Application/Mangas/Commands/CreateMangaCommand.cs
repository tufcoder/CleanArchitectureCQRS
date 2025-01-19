using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Domain.Models.Enums;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public sealed class CreateMangaCommand : IRequest<Manga>
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<MangaGenres> Genres { get; set; } = [];
    public DateTime ReleaseDate { get; set; }
    public string? Publisher { get; set; }
    public bool? IsActive { get; set; }
}
