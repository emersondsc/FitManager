using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace FitManagerAPI;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cliente").WithTags(nameof(Cliente));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            return await db.Cliente.ToListAsync();
        })
        .WithName("GetAllClientes")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Cliente>, NotFound>> (int clienteid, FitManagerAPIContext db) =>
        {
            return await db.Cliente.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ClienteId == clienteid)
                is Cliente model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClienteById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int clienteid, Cliente cliente, FitManagerAPIContext db) =>
        {
            var affected = await db.Cliente
                .Where(model => model.ClienteId == clienteid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.ClienteId, cliente.ClienteId)
                    .SetProperty(m => m.Nome, cliente.Nome)
                    .SetProperty(m => m.Email, cliente.Email)
                    .SetProperty(m => m.Telefone, cliente.Telefone)
                    .SetProperty(m => m.Ativo, cliente.Ativo)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCliente")
        .WithOpenApi();

        group.MapPost("/", async (Cliente cliente, FitManagerAPIContext db) =>
        {
            db.Cliente.Add(cliente);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Cliente/{cliente.ClienteId}",cliente);
        })
        .WithName("CreateCliente")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int clienteid, FitManagerAPIContext db) =>
        {
            var affected = await db.Cliente
                .Where(model => model.ClienteId == clienteid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCliente")
        .WithOpenApi();
    }
}
