
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SuplementosAPI.Repositories;
using SuplementosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DEL SERVICIO CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

// --- 2. CONFIGURACIÓN DE SEGURIDAD JWT (¡NUEVO!) ---
// Esto lee la configuración de tu appsettings.json y prepara la API para validar tokens
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

// --- 3. INYECCIÓN DE DEPENDENCIAS (Tus Repositorios y Servicios) ---
builder.Services.AddScoped<ICreatinaRepository, CreatinaRepository>();
builder.Services.AddScoped<ICreatinaService, CreatinaService>();
builder.Services.AddScoped<IProteinaRepository, ProteinaRepository>();
builder.Services.AddScoped<IProteinaService, ProteinaService>();
builder.Services.AddScoped<ISalsaRepository, SalsaRepository>();
builder.Services.AddScoped<ISalsaService, SalsaService>();
builder.Services.AddScoped<ITortitasRepository, TortitasRepository>();
builder.Services.AddScoped<ITortitasService, TortitasService>();
builder.Services.AddScoped<IBebidaRepository, BebidaRepository>();
builder.Services.AddScoped<IBebidaService, BebidaService>();
builder.Services.AddScoped<IPreEntrenoRepository, PreEntrenoRepository>();
builder.Services.AddScoped<IPreEntrenoService, PreEntrenoService>();
builder.Services.AddScoped<IOmega3Repository, Omega3Repository>();
builder.Services.AddScoped<IOmega3Service, Omega3Service>();
builder.Services.AddScoped<IPacksRepository, PacksRepository>();
builder.Services.AddScoped<IPacksService, PacksService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IOpinionRepository, OpinionRepository>();
builder.Services.AddScoped<IOpinionService, OpinionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 4. PIPELINE DE PETICIONES ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

// ¡IMPORTANTE! El orden aquí es sagrado:
app.UseAuthentication(); // 1. ¿Quién eres? (Verifica el Token JWT)
app.UseAuthorization();  // 2. ¿Tienes permiso? (Verifica el Rol)

app.MapControllers();

app.Run();