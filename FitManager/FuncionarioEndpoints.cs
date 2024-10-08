﻿using Microsoft.EntityFrameworkCore;
using FitManagerAPI.Data;
using FitManagerAPI.Modelos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using FitManagerAPI.Requests;
namespace FitManagerAPI;

public static class FuncionarioEndpoints
{
    public static void MapFuncionarioEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Funcionario").WithTags(nameof(Funcionario));

        group.MapGet("/", async (FitManagerAPIContext db) =>
        {
            return await db.Funcionario.ToListAsync();
        })
        .WithName("GetAllFuncionarios")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Funcionario>, NotFound>> (Guid funcionarioid, FitManagerAPIContext db) =>
        {
            return await db.Funcionario.AsNoTracking()
                .FirstOrDefaultAsync(model => model.FuncionarioId == funcionarioid)
                is Funcionario model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetFuncionarioById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid funcionarioid, FuncionarioRequest funcionarioRequest, FitManagerAPIContext db) =>
        {
            var funcionario = new Funcionario(funcionarioRequest.nome, funcionarioRequest.cargo, funcionarioRequest.salario, funcionarioRequest.horarioTrabalho);
            var affected = await db.Funcionario
                .Where(model => model.FuncionarioId == funcionarioid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Nome, funcionario.Nome)
                    .SetProperty(m => m.Cargo, funcionario.Cargo)
                    .SetProperty(m => m.Salario, funcionario.Salario)
                    .SetProperty(m => m.HorarioTrabalho, funcionario.HorarioTrabalho)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateFuncionario")
        .WithOpenApi();

        group.MapPost("/", async (FuncionarioRequest funcionarioRequest, FitManagerAPIContext db) =>
        {
            var funcionario = new Funcionario(funcionarioRequest.nome, funcionarioRequest.cargo, funcionarioRequest.salario, funcionarioRequest.horarioTrabalho);
            db.Funcionario.Add(funcionario);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Funcionario/{funcionario.FuncionarioId}",funcionario);
        })
        .WithName("CreateFuncionario")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid funcionarioid, FitManagerAPIContext db) =>
        {
            var affected = await db.Funcionario
                .Where(model => model.FuncionarioId == funcionarioid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteFuncionario")
        .WithOpenApi();
    }
}
