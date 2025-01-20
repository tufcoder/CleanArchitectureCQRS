using FluentValidation;

namespace CleanArchCQRS.Application.Mangas.Commands.Validations;

public class DeleteMangaCommandValidator : AbstractValidator<DeleteMangaCommand>
{
    public DeleteMangaCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotNull();
    }
}
