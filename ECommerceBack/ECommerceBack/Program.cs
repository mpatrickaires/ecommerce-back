using AutoMapper;
using ECommerceBack.Common;
using ECommerceBack.Common.Extensions;
using ECommerceBack.Infra.Context;
using ECommerceBack.WebApi.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => options.Filters.Add<NotificationFilter>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ECommerceDbContext>(options => options
    .UseNpgsql("Host=localhost;Database=ecommerce;Username=postgres;Password=@qN9v7@3^68#!%x")
    .UseSnakeCaseNamingConvention());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<NotificationContext>();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.Where(type => type.Name.EndsWithAny("Repository", "Service")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    serviceProvider.GetRequiredService<IMapper>()
        .ConfigurationProvider
        .AssertConfigurationIsValid();

    serviceProvider.GetRequiredService<ECommerceDbContext>()
        .Database
        .Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
