using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrbanControl.Backend.Data;
using UrbanControl.Backend.Interfaces;
using UrbanControl.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Asegúrate de que esta línea esté ANTES de builder.Build()
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Esta línea le dice al JSON que si ve una referencia circular, la ignore en lugar de romperse
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

    // Opcional: Esto hace que el JSON se vea más ordenado en Swagger
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ILoteService, LoteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddScoped<IEmpresaService, EmpresaService>();

builder.Services.AddScoped<IPermisosService, PermisosService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UrbanControl API", Version = "v1" });

    // 1. Definir el esquema de seguridad (JWT)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Escribe: 'Bearer' seguido de un espacio y tu token.\n\nEjemplo: Bearer eyJhbGciOiJIUzI1Ni..."
    });

    // 2. Aplicar la seguridad a todos los endpoints
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Configuration de JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Prueba poniéndolo en false
        ValidateAudience = false, // Prueba poniéndolo en false
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllwAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200","https://urban-control-frontend.vercel.app","https://whimsical-youtiao-4502b8.netlify.app") // El puerto de tu Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UrbanControl API v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz "/"
});

app.UseHttpsRedirection();

app.UseCors("AllwAngularApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Llamamos al método estático que creamos
        await DbSeeder.SeedAsync(context);
        Console.WriteLine("--- Semilla de datos ejecutada con éxito ---");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--- Error al ejecutar el Seeding: {ex.Message} ---");
    }
}

app.Run();
