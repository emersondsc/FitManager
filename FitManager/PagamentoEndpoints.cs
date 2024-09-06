using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace FitManagerAPI;

public static class PagamentoEndpoints
{
    public static void MapPagamentoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pagamento").WithTags(nameof(Pagamento));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            return await db.Pagamento.ToListAsync();
        })
        .WithName("GetAllPagamentos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Pagamento>, NotFound>> (int pagamentoid, FitManagerAPIContext db) =>
        {
            return await db.Pagamento.AsNoTracking()
                .FirstOrDefaultAsync(model => model.PagamentoId == pagamentoid)
                is Pagamento model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPagamentoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int pagamentoid, Pagamento pagamento, FitManagerAPIContext db) =>
        {
            var affected = await db.Pagamento
                .Where(model => model.PagamentoId == pagamentoid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.PagamentoId, pagamento.PagamentoId)
                    .SetProperty(m => m.DataPagamento, pagamento.DataPagamento)
                    .SetProperty(m => m.ValorPago, pagamento.ValorPago)
                    .SetProperty(m => m.MetodoPagamento, pagamento.MetodoPagamento)
                    .SetProperty(m => m.Confirmado, pagamento.Confirmado)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePagamento")
        .WithOpenApi();

        group.MapPost("/", async (Pagamento pagamento, FitManagerAPIContext db) =>
        {
            db.Pagamento.Add(pagamento);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Pagamento/{pagamento.PagamentoId}",pagamento);
        })
        .WithName("CreatePagamento")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int pagamentoid, FitManagerAPIContext db) =>
        {
            var affected = await db.Pagamento
                .Where(model => model.PagamentoId == pagamentoid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePagamento")
        .WithOpenApi();
    }
}
