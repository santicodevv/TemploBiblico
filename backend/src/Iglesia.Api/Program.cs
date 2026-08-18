using System.Text;
using Iglesia.Api.Authorization;
using Iglesia.Api.Services;
using Iglesia.Application;
using Iglesia.Application.Common.Interfaces;
using Iglesia.Domain.Entities;
using Iglesia.Infrastructure;
using Iglesia.Infrastructure.Identity;
using Iglesia.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Identity configuration (Corregido con ApplicationUser y ApplicationRole)
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
    .AddEntityFrameworkStores<IglesiaDbContext>()
    .AddDefaultTokenProviders();

// HTTP Context & Custom Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IDateTime, DateTimeService>();

// Controllers
builder.Services.AddControllers();

// Swagger / Endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline
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

// Seeder Automático: Crea roles y usuario inicial si no existen
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetService<UserManager<ApplicationUser>>();
        var roleManager = services.GetService<RoleManager<ApplicationRole>>();

        if (userManager != null && roleManager != null)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "Admin" });

            if (!await roleManager.RoleExistsAsync("Lider"))
                await roleManager.CreateAsync(new ApplicationRole { Name = "Lider" });

            var adminEmail = "admin@iglesia.com";
            var usuarioExistente = await userManager.FindByEmailAsync(adminEmail);

            if (usuarioExistente == null)
            {
                var nuevoAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var resultado = await userManager.CreateAsync(nuevoAdmin, "Admin123!");
                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(nuevoAdmin, "Admin");
                }
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