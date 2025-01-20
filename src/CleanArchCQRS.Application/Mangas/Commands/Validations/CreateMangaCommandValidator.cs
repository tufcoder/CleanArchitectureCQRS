using FluentValidation;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands.Validations;

public class CreateMangaCommandValidator : AbstractValidator<CreateMangaCommand>
{
    public CreateMangaCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Please ensure you have entered the Title")
            .MaximumLength(100).WithMessage("The Title must have at maximum 100 characteres");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .NotEmpty().WithMessage("Please ensure you have entered the Price");

        //RuleFor(p => p.Genres)
        //    .NotEmpty().WithMessage("Please ensure you have entered the Genres");

        RuleFor(p => p.ReleaseDate)
            .NotEmpty().WithMessage("Please ensure you have entered the ReleaseDate");

        RuleFor(p => p.Publisher)
            .NotEmpty().WithMessage("Please ensure you have entered the Publisher");

        RuleFor(p => p.IsActive)
            .NotNull()
            .NotEmpty().WithMessage("Please ensure you have inform the active status");
    }
}
