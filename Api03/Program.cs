using System.Text.Json.Serialization;
using Api03.infra;
using Api03.models;
using Api03.repositories;
using Api03.services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
 
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IFuncionarioioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
 
builder.Services.AddScoped<ISetorService, SetorService>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
 

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();
 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}
 
app.UseSwagger();
app.UseSwaggerUI();
 
app.MapControllers();
 
app.Run();