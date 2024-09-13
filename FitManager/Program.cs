using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FitManagerAPI.Data;
using FitManagerAPI;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FitManagerAPIContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FitManagerAPIContext") ?? throw new InvalidOperationException("Connection string 'FitManagerAPIContext' not found.")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapClienteEndpoints();

app.MapPlanoEndpoints();

app.MapPagamentoEndpoints();

app.MapDespesaEndpoints();

app.MapFuncionarioEndpoints();

app.Run();