using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Queries;

public sealed class GetMangasQuery : IRequest<IEnumerable<Manga>>
{
    public class GetMangasQueryHandler : IRequestHandler<GetMangasQuery, IEnumerable<Manga>>
    {
        private readonly IMangaDapperRepository _repository;

        public GetMangasQueryHandler(IMangaDapperRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Manga>> Handle(GetMangasQuery request, CancellationToken cancellationToken)
        {
            var mangas = await _repository.GetMangas();
            return mangas;
        }
    }
}
