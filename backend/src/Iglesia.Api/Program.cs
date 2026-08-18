using System.Text;
using Iglesia.Api.Authorization;
using Iglesia.Api.Services;
using Iglesia.Application;
using Iglesia.Application.Common.Interfaces;
using Iglesia.Application.Interfaces;
using Iglesia.Domain.Entities;
using Iglesia.Infrastructure;
using Iglesia.Infrastructure.Persistence;
using Iglesia.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// ASP.NET Core Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// HTTP Context & Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IDateTime, DateTimeService>();

// Servicio de Autenticación personalizado
builder.Services.AddScoped<IAuthService, AuthService>();

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI con soporte para JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Iglesia API", Version = "v1" });

    // Configuración del botón Authorize 🔒 para colocar el Token JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token JWT en el formato: Bearer {tu_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(builder.Configuration.GetValue<string>("Cors:AllowedOrigins") ?? "http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings.GetValue<string>("SecretKey") 
    ?? "DefaultSecretKeyForDevelopmentOnly_ChangeInProduction!";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer") ?? "IglesiaApi",
        ValidAudience = jwtSettings.GetValue<string>("Audience") ?? "IglesiaApp",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

// Authorization Policies
builder.Services.AddScoped<IAuthorizationHandler, MismoMinisterioHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiereAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequiereLider", policy => policy.RequireRole("Lider", "Admin"));
    options.AddPolicy("MismoMinisterioPolicy", policy =>
        policy.Requirements.Add(new MismoMinisterioRequirement()));
});

var app = builder.Build();

// Pipeline de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seeder Automático: Crea roles y el usuario admin si no existen
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<Usuario>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await roleManager.RoleExistsAsync("Lider"))
            await roleManager.CreateAsync(new IdentityRole("Lider"));

        var adminEmail = "admin@iglesia.com";
        var usuarioExistente = await userManager.FindByEmailAsync(adminEmail);

        if (usuarioExistente == null)
        {
            var nuevoAdmin = new Usuario
            {
                UserName = adminEmail,
                Email = adminEmail,
                NombreCompleto = "Administrador Sistema",
                EmailConfirmed = true,
                MinisterioId = 1
            };

            var resultado = await userManager.CreateAsync(nuevoAdmin, "Admin123!");
            if (resultado.Succeeded)
            {
                await userManager.AddToRoleAsync(nuevoAdmin, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al ejecutar el Seeder inicial.");
    }
}

app.Run();
