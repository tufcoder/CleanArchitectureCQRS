using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public sealed class UpdateMangaCommand : MangaCommandBase
{
    public int Id { get; set; }

    public class UpdateMangaCommandHandler : IRequestHandler<UpdateMangaCommand, Manga>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMangaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manga> Handle(UpdateMangaCommand request, CancellationToken cancellationToken)
        {
            var manga = await _unitOfWork.MangaRepository.GetById(request.Id);

            manga.Update(
                request.Title!,
                request.Price,
                request.Genres,
                request.ReleaseDate,
                request.Publisher!,
                request.IsActive
                );
            manga.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.MangaRepository.Update(manga);
            await _unitOfWork.CommitAsync();

            return manga;
        }
    }
}
