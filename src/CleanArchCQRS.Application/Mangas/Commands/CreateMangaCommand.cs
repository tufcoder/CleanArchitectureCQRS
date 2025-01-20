using CleanArchCQRS.Application.Mangas.Commands.Notifications;
using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public sealed class CreateMangaCommand : MangaCommandBase
{
    public class CreateMangaCommandHandler : IRequestHandler<CreateMangaCommand, Manga>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CreateMangaCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
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

            await _mediator.Publish(new MangaCreatedNotification(manga), cancellationToken);

            return manga;
        }
    }
}
