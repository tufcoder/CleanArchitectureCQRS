using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Infrastructure.Context;
using CleanArchCQRS.Infrastructure.Repositories;
using CleanArchCQRS.Application.Mangas.Commands.Validations;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace CleanArchCQRS.CrossCutting.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["SQL_SERVER_CONNECTION_STRING"];

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
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(myHandlers);
            configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("CleanArchCQRS.Application"));

        return services;
    }
}
