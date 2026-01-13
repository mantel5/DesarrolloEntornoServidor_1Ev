using Microsoft.OpenApi.Models; // <--- Necesario para configurar Swagger
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

// --- 2. CONFIGURACIÓN DE SEGURIDAD JWT ---
// --- CONFIGURACIÓN DE SEGURIDAD JWT ---
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
            // AQUI ESTÁN LOS CAMBIOS PARA COINCIDIR CON EL JSON:
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

// --- 3. INYECCIÓN DE DEPENDENCIAS ---
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
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 4. CONFIGURACIÓN DE SWAGGER CON BOTÓN AUTHORIZE (¡MODIFICADO!) ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuplementosAPI", Version = "v1" });

    // Definimos el esquema de seguridad (Bearer)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Requerimos este esquema en los endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// --- 5. PIPELINE DE PETICIONES ---

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