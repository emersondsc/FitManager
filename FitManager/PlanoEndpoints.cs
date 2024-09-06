using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace FitManagerAPI;

public static class PlanoEndpoints
{
    public static void MapPlanoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Plano").WithTags(nameof(Plano));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            return await db.Plano.ToListAsync();
        })
        .WithName("GetAllPlanos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Plano>, NotFound>> (int planoid, FitManagerAPIContext db) =>
        {
            return await db.Plano.AsNoTracking()
                .FirstOrDefaultAsync(model => model.PlanoId == planoid)
                is Plano model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPlanoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int planoid, Plano plano, FitManagerAPIContext db) =>
        {
            var affected = await db.Plano
                .Where(model => model.PlanoId == planoid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.PlanoId, plano.PlanoId)
                    .SetProperty(m => m.Nome, plano.Nome)
                    .SetProperty(m => m.Preco, plano.Preco)
                    .SetProperty(m => m.Descricao, plano.Descricao)
                    .SetProperty(m => m.Duracao, plano.Duracao)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePlano")
        .WithOpenApi();

        group.MapPost("/", async (Plano plano, FitManagerAPIContext db) =>
        {
            db.Plano.Add(plano);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Plano/{plano.PlanoId}",plano);
        })
        .WithName("CreatePlano")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int planoid, FitManagerAPIContext db) =>
        {
            var affected = await db.Plano
                .Where(model => model.PlanoId == planoid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePlano")
        .WithOpenApi();
    }
}
