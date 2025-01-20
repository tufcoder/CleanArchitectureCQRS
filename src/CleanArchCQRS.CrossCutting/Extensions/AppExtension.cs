using System.Data;

using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Infrastructure.Context;
using CleanArchCQRS.Infrastructure.Repositories;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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

        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        });

        services.AddScoped<IMangaRepository, MangaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMangaDapperRepository, MangaDapperRepository>();

        var myHandlers = AppDomain.CurrentDomain.Load("CleanArchCQRS.Application");
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(myHandlers));

        return services;
    }
}
