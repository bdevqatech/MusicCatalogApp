using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.API.Data;
using MusicCatalog.API.Helpers;
using MusicCatalog.API.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MusicCatalogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfiles>(); // add all your profiles here
});

mapperConfig.AssertConfigurationIsValid();

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MusicCatalogContext>();
    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("Connection to the database succeeded.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection to the database failed: {ex.Message}");
    }
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
