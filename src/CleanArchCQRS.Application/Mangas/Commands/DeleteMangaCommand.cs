using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public class DeleteMangaCommand : IRequest<Manga>
{
    public int Id { get; set; }

    public class DeleteMangaCommandHandler : IRequestHandler<DeleteMangaCommand, Manga>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMangaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Manga> Handle(DeleteMangaCommand request, CancellationToken cancellationToken)
        {
            var manga = await _unitOfWork.MangaRepository.DeleteById(request.Id);

            await _unitOfWork.CommitAsync();

            return manga;
        }
    }
}
