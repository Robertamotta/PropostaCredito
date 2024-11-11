using PropostaCredito.Api.MassTransitConfig;
using PropostaCredito.Dominio.Interfaces;
using PropostaCredito.Dominio.Servico;
using PropostaCredito.Infraestrutura.Servico;

var builder = WebApplication.CreateBuilder(args);

// Dependency Resolver 
builder.Services.AddScoped<IMensageria, Mensageria>();
builder.Services.AddScoped<IProcessadorClienteServico, ProcessadorClienteServico>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddControllers();
builder.Services.ConfigureMasTransit();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
