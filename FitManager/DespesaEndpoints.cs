using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using FitManagerAPI.Requests;
using System.Drawing;
namespace FitManagerAPI;

public static class DespesaEndpoints
{
    public static void MapDespesaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Despesa").WithTags(nameof(Despesa));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            return await db.Despesa.ToListAsync();
        })
        .WithName("GetAllDespesas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Despesa>, NotFound>> (Guid despesaid, FitManagerAPIContext db) =>
        {
            return await db.Despesa.AsNoTracking()
                .FirstOrDefaultAsync(model => model.DespesaId == despesaid)
                is Despesa model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetDespesaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid despesaid, DespesaRequest despesaRequest, FitManagerAPIContext db) =>
        {
            var despesa = new Despesa(despesaRequest.descricao, despesaRequest.valor, despesaRequest.categoria);
            var affected = await db.Despesa
                .Where(model => model.DespesaId == despesaid)
                .ExecuteUpdateAsync(setters => setters
                    
                    .SetProperty(m => m.Descricao, despesa.Descricao)
                    .SetProperty(m => m.Data, despesa.Data)
                    .SetProperty(m => m.Valor, despesa.Valor)
                    .SetProperty(m => m.Categoria, despesa.Categoria)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateDespesa")
        .WithOpenApi();

        group.MapPost("/", async (DespesaRequest despesaRequest, FitManagerAPIContext db) =>
        {
            var despesa = new Despesa(despesaRequest.descricao, despesaRequest.valor, despesaRequest.categoria);
            db.Despesa.Add(despesa);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Despesa/{despesa.DespesaId}",despesa);
        })
        .WithName("CreateDespesa")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid despesaid, FitManagerAPIContext db) =>
        {
            var affected = await db.Despesa
                .Where(model => model.DespesaId == despesaid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteDespesa")
        .WithOpenApi();
    }
}
