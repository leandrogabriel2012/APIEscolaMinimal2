using APIEscolaMinimal2.Context;
using APIEscolaMinimal2.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace APIEscolaMinimal2.ApiEndpoints;

public static class TurmasEndpoints
{
    public static void MapTurmasEndpoints(this WebApplication app)
    {
        app.MapGet("/turmas", async (AppDbContext db) => await db.Turmas.ToListAsync()).WithTags("Turmas");

        app.MapGet("/turmas/{id:int}", async (AppDbContext db, int id) => await db.Turmas.FindAsync(id)).WithTags("Turmas");

        app.MapPost("/turmas", async (AppDbContext db, Turma turma) =>
        {
            db.Turmas.Add(turma);
            await db.SaveChangesAsync();

            return Results.Created($"Turma de id = {turma.TurmaId} criada", turma);
        }).WithTags("Turmas").RequireAuthorization();

        app.MapPut("/turmas/{id:int}", async (AppDbContext db, Turma turma, int id) =>
        {
            if (turma.TurmaId != id) return Results.BadRequest();

            var turmaDB = await db.Turmas.FindAsync(id);

            if (turmaDB is null) return Results.NotFound();

            turmaDB = new Turma
            {
                TurmaId = turma.TurmaId,
                Ano = turma.Ano,
                Sequencia = turma.Sequencia,
                SalaId = turma.SalaId
            };

            await db.SaveChangesAsync();

            return Results.Ok(turmaDB);
        }).WithTags("Turmas").RequireAuthorization();

        app.MapDelete("/turmas/{id:int}", async (AppDbContext db, int id) =>
        {
            var turma = await db.Turmas.FindAsync(id);

            if (turma is null) return Results.NotFound();

            db.Turmas.Remove(turma);
            await db.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Turmas").RequireAuthorization();
    }
}
