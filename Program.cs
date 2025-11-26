using SuplementosAPI.Repositories;
using SuplementosAPI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<ICreatinaRepository, CreatinaRepository>();
builder.Services.AddScoped<ICreatinaService, CreatinaService>();
builder.Services.AddScoped<IProteinaRepository, ProteinaRepository>();
builder.Services.AddScoped<IProteinaService, ProteinaService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
