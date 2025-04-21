using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Queries;

public sealed class GetMangasByIdQuery : IRequest<Manga>
{
    public int Id { get; set; }

    public class GetMangasByIdQueryHandler : IRequestHandler<GetMangasByIdQuery, Manga>
    {
        private readonly IMangaDapperRepository _repository;

        public GetMangasByIdQueryHandler(IMangaDapperRepository repository)
        {
            _repository = repository;
        }

        public async Task<Manga> Handle(GetMangasByIdQuery request, CancellationToken cancellationToken)
        {
            var manga = await _repository.GetMangaByIdAsync(request.Id);
            
            return manga;
        }
    }
}
