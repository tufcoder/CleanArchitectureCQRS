using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Infrastructure.Context;
using CleanArchCQRS.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchCQRS.CrossCutting.Extensions;

public static class AppExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IMangaRepository, MangaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var myHandlers = AppDomain.CurrentDomain.Load("CleanArchCQRS.Application");
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(myHandlers));

        return services;
    }
}
