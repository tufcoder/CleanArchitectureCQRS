using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using FluentValidation;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands;

public sealed class CreateMangaCommand : MangaCommandBase
{
    public class CreateMangaCommandHandler : IRequestHandler<CreateMangaCommand, Manga>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateMangaCommand> _validator;

        public CreateMangaCommandHandler(IUnitOfWork unitOfWork,
            IValidator<CreateMangaCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Manga> Handle(CreateMangaCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            
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
}
