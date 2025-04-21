using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Microsoft.OpenApi.Models;
using Serilog;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Diagnostics;
using NZWalks.API.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

var logger =new LoggerConfiguration()
      .WriteTo.Console()
      //To save the Log in .txt file....
      .WriteTo.File("Logs/NZWalks_Log.txt",rollingInterval:RollingInterval.Day)
      .MinimumLevel.Warning()
      .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Wakls Api", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme="Outh2",
                Name=JwtBearerDefaults.AuthenticationScheme,
                In= ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

// ✅ Configure DbContext for main application data
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

// ✅ Fixed: Configure DbContext for authentication (Identity)
builder.Services.AddDbContext<NZWalkAuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"))); // 🔥 Fixed the key

// Dependency Injection for Repositories
builder.Services.AddScoped<IRegionRepository, ImpeRegionRepository>();
builder.Services.AddScoped<IWalksRepository, ImpWalksRepository>();
builder.Services.AddScoped<IDifficulty, ImpDifficulty>();
builder.Services.AddAutoMapper(typeof(AutomapperProfile));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IimageRepository, ImageRepository>();

// Register IHttpContextAccessor service
builder.Services.AddHttpContextAccessor();



builder.Services.AddIdentityCore<IdentityUser>()
     .AddRoles<IdentityRole>()
     .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks") 
     .AddEntityFrameworkStores<NZWalkAuthDbContext>()
     .AddDefaultTokenProviders(); // 🔥 Added default token providers for password reset, email confirmation, etc.


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    //// Lockout settings
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //options.Lockout.MaxFailedAccessAttempts = 5;
    //options.Lockout.AllowedForNewUsers = true;
    //// User settings
    //options.User.RequireUniqueEmail = true;
});

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExeptionHandlerMiddleware>();


app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication comes before authorization
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Image",
});

app.MapControllers();

app.Run();
