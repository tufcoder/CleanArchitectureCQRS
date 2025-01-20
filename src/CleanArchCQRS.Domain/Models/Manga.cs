using CleanArchCQRS.Domain.Models.Enums;
using CleanArchCQRS.Domain.Validations;

namespace CleanArchCQRS.Domain.Models;

public sealed class Manga : Entity
{
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<MangaGenres> Genres { get; set; } = [];
    public DateTime ReleaseDate { get; set; }
    public string? Publisher { get; set; }
    public bool? IsActive { get; set; }

    private Manga()
    {
        
    }

    public Manga(string title, decimal price, IEnumerable<MangaGenres> genres, DateTime releaseDate, string publisher, bool? isActive)
    {
        ValidateDomain(title, price, genres, releaseDate, publisher, isActive);
    }

    public Manga(int id, string title, decimal price, IEnumerable<MangaGenres> genres, DateTime releaseDate, string publisher, bool? isActive)
    {
        DomainValidation.When(id < 0, "Invalid Id value.");
        Id = id;
        ValidateDomain(title, price, genres, releaseDate, publisher, isActive);
    }

    public void Update(string title, decimal price, IEnumerable<MangaGenres> genres, DateTime releaseDate, string publisher, bool? isActive)
    {
        ValidateDomain(title, price, genres, releaseDate, publisher, isActive);
    }

    private void ValidateDomain(string title, decimal price, IEnumerable<MangaGenres> genres, DateTime releaseDate, string publisher, bool? isActive)
    {
        DomainValidation.When(string.IsNullOrEmpty(title), "Invalid title. Title is required.");
        DomainValidation.When(price <= 0, "Invalid price. Price need to be greater than zero.");
        DomainValidation.When(genres.Count() < 1, "Invalid genres. A Manga needs have at least one genre.");
        DomainValidation.When(string.IsNullOrEmpty(releaseDate.ToString()), "Invalid release date. Input a valid date.");
        DomainValidation.When(string.IsNullOrEmpty(publisher), "Invalid publisher. Publisher is required.");
        DomainValidation.When(!isActive.HasValue, "Input a valid active state: true or false.");

        Title = title;
        Price = price;
        Genres = genres;
        ReleaseDate = releaseDate;
        Publisher = publisher;
        IsActive = isActive;
    }
}
