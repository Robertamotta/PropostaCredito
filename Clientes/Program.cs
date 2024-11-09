using Clientes.Aplicacao.Servicos;
using Clientes.Infraestrutura.Dados;
using Clientes.Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MySQL
builder.Services.AddDbContext<ClientesContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));

// Dependency Resolver 
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<ClienteServico>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
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
