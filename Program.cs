using SuplementosAPI.Repositories;
using SuplementosAPI.Services;

var builder = WebApplication.CreateBuilder(args);


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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
