using PropostaCredito.Api.Consumers;
using PropostaCredito.Dominio.Interfaces;
using PropostaCredito.Dominio.Servico;
using PropostaCredito.Infraestrutura.Servico;

var builder = WebApplication.CreateBuilder(args);

// Dependency Resolver 
builder.Services.AddScoped<IMensageria, Mensageria>();
builder.Services.AddScoped<IProcessadorPropostaCreditoClienteServico, ProcessadorPropostaCreditoClienteServico>();

builder.Services.AddHostedService<ClienteConsumer>();

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
