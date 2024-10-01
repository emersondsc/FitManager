using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using FitManagerAPI.Requests;
namespace FitManagerAPI;

public static class PagamentoEndpoints
{
    public static void MapPagamentoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pagamento").WithTags(nameof(Pagamento));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            var pagamentos = await db.Pagamento
            .Include(c => c.Cliente) // Carrega o Clinte para cada Pagamento
            .ToListAsync();
            return pagamentos;
        })
        .WithName("GetAllPagamentos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Pagamento>, NotFound>> (Guid pagamentoid, FitManagerAPIContext db) =>
        {
            return await db.Pagamento
                .Include(c => c.Cliente) // Carrega o Clinte para cada Pagamento
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.PagamentoId == pagamentoid)
                is Pagamento model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPagamentoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid pagamentoid, PagamentoRequest pagamentoRequest, FitManagerAPIContext db) =>
        {
            var clientePagante = db.Cliente.Find(pagamentoRequest.clienteId);
            

            var pagamento = new Pagamento(clientePagante, pagamentoRequest.dataPagamento, pagamentoRequest.valorPago,
                pagamentoRequest.metodoPagamento, pagamentoRequest.confirmado);

            var affected = await db.Pagamento
                .Where(model => model.PagamentoId == pagamentoid)
                .ExecuteUpdateAsync(setters => setters
                    
                    .SetProperty(m => m.DataPagamento, pagamento.DataPagamento)
                    .SetProperty(m => m.ValorPago, pagamento.ValorPago)
                    .SetProperty(m => m.MetodoPagamento, pagamento.MetodoPagamento)
                    .SetProperty(m => m.Confirmado, pagamento.Confirmado)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePagamento")
        .WithOpenApi();

        group.MapPost("/", async (PagamentoRequest pagamentoRequest, FitManagerAPIContext db) =>
        {
            var clientePagante = db.Cliente.Find(pagamentoRequest.clienteId);
            var dataUTC = DateTime.Now.ToUniversalTime();

            var pagamento = new Pagamento(clientePagante, dataUTC, pagamentoRequest.valorPago,
                pagamentoRequest.metodoPagamento, pagamentoRequest.confirmado);

            db.Pagamento.Add(pagamento);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Pagamento/{pagamento.PagamentoId}",pagamento);
        })
        .WithName("CreatePagamento")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid pagamentoid, FitManagerAPIContext db) =>
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
