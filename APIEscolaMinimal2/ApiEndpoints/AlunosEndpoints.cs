using APIEscolaMinimal2.Context;
using APIEscolaMinimal2.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaMinimal2.ApiEndpoints;

public static class AlunosEndpoints
{
    public static void MapAlunosEnpoints(this WebApplication app)
    {
        app.MapGet("/alunos", async (AppDbContext db) => await db.Alunos.ToListAsync()).WithTags("Alunos");

        app.MapGet("/alunos/{id:int}", async (AppDbContext db, int id) => await db.Alunos.FindAsync(id)).WithTags("Alunos");

        app.MapPost("/alunos", async (AppDbContext db, Aluno aluno) =>
        {
            db.Alunos.Add(aluno);
            await db.SaveChangesAsync();

            return Results.Created($"Aluno de id = {aluno.AlunoId} criado", aluno);
        }).WithTags("Alunos").RequireAuthorization();

        app.MapPut("/alunos/{id:int}", async (AppDbContext db, Aluno aluno, int id) =>
        {
            if (aluno.AlunoId != id) return Results.BadRequest();

            var alunoDB = await db.Alunos.FindAsync(id);

            if (alunoDB is null) return Results.NotFound();

            alunoDB = new Aluno
            {
                AlunoId = aluno.AlunoId,
                Nome = aluno.Nome,
                Identidade = aluno.Identidade,
                Nascimento = aluno.Nascimento,
                TurmaId = aluno.TurmaId,
                Turma = aluno.Turma
            };

            await db.SaveChangesAsync();

            return Results.Ok(alunoDB);
        }).WithTags("Alunos").RequireAuthorization();

        app.MapDelete("/alunos/{id:int}", async (AppDbContext db, int id) =>
        {
            var aluno = await db.Alunos.FindAsync(id);

            if (aluno is null) return Results.NotFound();

            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Alunos").RequireAuthorization();
    }

}
