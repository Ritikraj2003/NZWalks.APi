using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NZWalksDbContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));
builder.Services.AddScoped<IRegionRepository,ImpeRegionRepository>();
builder.Services.AddScoped<IWalksRepository, ImpWalksRepository>();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
