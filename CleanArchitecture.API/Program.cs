
using CleanArchitecture.API.Middleware;
using CleanArchitecture.Application;
using CleanArchitecture.Identity;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {

        var context = service.GetRequiredService<StreamerDbContext>();
        await context.Database.MigrateAsync();
        await StreamerDbContextSeed.SeedAsync(context, loggerFactory);
        await StreamerDbContextSeedData.LoadDataAsync(context, loggerFactory);


        var contextIdentity = service.GetRequiredService<CleanArchitectureIdentityDbContext>();
        await contextIdentity.Database.MigrateAsync();

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "error en migración");
        throw;
    }
}

app.Run();
