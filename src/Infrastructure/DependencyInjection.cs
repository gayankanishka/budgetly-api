using Budgetly.Application.Common.Interfaces;
using Budgetly.Infrastructure.Identity.Options;
using Budgetly.Infrastructure.Persistence;
using Budgetly.Infrastructure.Persistence.Factories;
using Budgetly.Infrastructure.Persistence.Imports;
using Budgetly.Infrastructure.Persistence.Options;
using Budgetly.Infrastructure.Persistence.Providers;
using Budgetly.Infrastructure.Persistence.Repositories;
using Budgetly.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Budgetly.Infrastructure;

/// <summary>
///     Dependency injection extension to configure Infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Configure Infrastructure layer services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseOptions = configuration.GetSection(PersistenceOptions.Persistence)
            .Get<PersistenceOptions>();

        services.AddDbContext<ApplicationDbContext>(options => 
           AbstractPersistenceFactory<IDatabaseProvider>
               .CreateFactory()
               .GetProvider(databaseOptions)
               .Build(options, databaseOptions));
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()
                                                              ?? throw new InvalidOperationException());
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddScoped<ITransactionCategoryRepository, TransactionCategoryRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IBudgetItemRepository, BudgetItemItemRepository>();
        services.AddScoped<IBudgetHistoryRepository, BudgetHistoryRepository>();

        services.AddTransient<IDateTimeService, DateTimeService>();

        services.SeedDatabase();
        
        var auth0Options = configuration.GetSection(Auth0Options.Auth0)
            .Get<Auth0Options>();
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
            {
                c.Authority = auth0Options.Domain;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = auth0Options.Audience,
                    ValidIssuer = auth0Options.Domain
                };
            });

        services.AddAuthorization(o =>
        {
            o.AddPolicy("read:transactions", p =>
                p.RequireAuthenticatedUser().RequireClaim("scope", "read:transactions"));
        });

        return services;
    }
}