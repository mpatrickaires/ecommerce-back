using AutoMapper;
using ECommerceBack.Application.Authentication;
using ECommerceBack.Common.Exceptions;
using ECommerceBack.Common.Extensions;
using ECommerceBack.Common.Notification;
using ECommerceBack.Common.Options;
using ECommerceBack.Infra.Database;
using ECommerceBack.Infra.Options;
using ECommerceBack.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Reflection;
using System.Text;

namespace ECommerceBack;

/// <summary>
/// Define os métodos que realizam todas as configurações necessárias para a execução da aplicação
/// </summary>
public static class Bootstrapper
{
    public static WebApplicationBuilder ConfigurarAplicacao(this WebApplicationBuilder builder) => builder
        .RegistrarServicos()
        .RegistrarOptions()
        .RegistrarAutoMapper()
        .RegistrarDbContext()
        .ConfigurarAutenticacao();

    private static WebApplicationBuilder RegistrarServicos(this WebApplicationBuilder builder)
    {
        builder.Services
            .Scan(scan => scan
                .FromAssemblyOf<Program>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWithAny("Repository", "Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime())
            .AddHttpContextAccessor()
            .AddScoped<NotificationContext>()
            .AddScoped<InicializadorBanco>()
            .AddScoped<IUsuarioLogado, UsuarioLogado>();

        return builder;
    }

    private static WebApplicationBuilder RegistrarOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<ECommerceContextOptions>(builder.Configuration.GetSection(ECommerceContextOptions.Position))
            .Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Position));

        return builder;
    }

    private static WebApplicationBuilder RegistrarAutoMapper(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return builder;
    }

    private static WebApplicationBuilder RegistrarDbContext(this WebApplicationBuilder builder)
    {
        ECommerceContextOptions contextOptions = builder.ObterOptions<ECommerceContextOptions>(ECommerceContextOptions.Position);

        builder.Services.AddDbContext<ECommerceDbContext>(options => options
            .UseNpgsql($"Host={contextOptions.Host};Database={contextOptions.Database};Username={contextOptions.Username};Password={contextOptions.Password}")
            .UseSnakeCaseNamingConvention());

        return builder;
    }

    private static WebApplicationBuilder ConfigurarAutenticacao(this WebApplicationBuilder builder)
    {
        JwtOptions jwtOptions = builder.ObterOptions<JwtOptions>(JwtOptions.Position);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            });

        return builder;
    }

    private static TOptions ObterOptions<TOptions>(this WebApplicationBuilder builder, string position)
        => builder.Configuration.GetSection(position).Get<TOptions>()
            ?? throw new OptionNaoRegistradaException(ECommerceContextOptions.Position);

    public static WebApplication ConfigurarAplicacao(this WebApplication app)
    {
        using (IServiceScope scope = app.Services.CreateScope())
        {
            scope.ServiceProvider
                .ValidarAutoMapper()
                .AplicarMigrations()
                .PopularBanco();
        }

        return app;
    }

    private static IServiceProvider ValidarAutoMapper(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

        return serviceProvider;
    }

    private static IServiceProvider AplicarMigrations(this IServiceProvider serviceProvider)
    {
        int retries = 0;
        void Migrate()
        {
            try
            {
                serviceProvider.GetRequiredService<ECommerceDbContext>().Database.Migrate();
            }
            catch (NpgsqlException e)
            {
                retries++;
                if (retries == 4)
                {
                    throw;
                }

                Thread.Sleep(TimeSpan.FromSeconds(2));
                Migrate();
            }
        }
        Migrate();

        return serviceProvider;
    }

    private static IServiceProvider PopularBanco(this IServiceProvider serviceProvider)
    {
        serviceProvider.GetRequiredService<InicializadorBanco>().PopularBanco();

        return serviceProvider;
    }
}
