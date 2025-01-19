using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public class CreateMangaCommandHandler : IRequestHandler<CreateMangaCommand, Manga>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMangaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Manga> Handle(CreateMangaCommand request, CancellationToken cancellationToken)
    {
        var manga = new Manga(
            request.Title!,
            request.Price,
            request.Genres,
            request.ReleaseDate,
            request.Publisher!,
            request.IsActive
            );

        await _unitOfWork.MangaRepository.Add(manga);
        await _unitOfWork.CommitAsync();

        return manga;
    }
}
