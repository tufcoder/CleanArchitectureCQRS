using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Domain.Models.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchCQRS.Infrastructure.Configurations;

public class MangaConfiguration : IEntityTypeConfiguration<Manga>
{
    public void Configure(EntityTypeBuilder<Manga> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Price).HasPrecision(18, 2);
        builder.Property(p => p.Genres)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(p => p.ReleaseDate).IsRequired();
        builder.Property(p => p.IsActive).IsRequired();
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(p => p.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Ignore(p => p.Genres);

        builder.HasData(
            new Manga(
                1,
                "One Piece",
                9.99m,
                new List<MangaGenres>()
                {
                    MangaGenres.Shounen
                },
                new DateTime(1997, 12, 24),
                "Shueisha",
                true
            ),
            new Manga(
                2,
                "Naruto",
                7.99m,
                new List<MangaGenres>()
                {
                    MangaGenres.Shounen,
                    MangaGenres.Ninja,
                },
                new DateTime(2000, 3, 3),
                "Shueisha",
                false
            ),
            new Manga(
                3,
                "Hajime no Ippo",
                5.99m,
                new List<MangaGenres>()
                {
                    MangaGenres.Shounen,
                    MangaGenres.Sports,
                    MangaGenres.Boxe,
                },
                new DateTime(1990, 2, 17),
                "Kodansha",
                true
            )
        );
    }
}
