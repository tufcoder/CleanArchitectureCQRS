using CleanArchCQRS.Domain.Models;
using CleanArchCQRS.Infrastructure.Configurations;

using Microsoft.EntityFrameworkCore;

namespace CleanArchCQRS.Infrastructure.Context;

public sealed class AppDbContext : DbContext
{
    public DbSet<Manga> Mangas { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MangaConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
