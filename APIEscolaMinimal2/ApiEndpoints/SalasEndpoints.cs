using APIEscolaMinimal2.Context;
using APIEscolaMinimal2.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaMinimal2.ApiEndpoints;

public static class SalasEndpoints
{
    public static void MapSalasEndpoints(this WebApplication app)
    {
        app.MapGet("/salas", async (AppDbContext db) => await db.Salas.ToListAsync()).WithTags("Salas");

        app.MapGet("/salas/{id:int}", async (AppDbContext db, int id) => await db.Salas.FindAsync(id)).WithTags("Salas");

        app.MapPost("/salas", async (AppDbContext db, Sala sala) =>
        {
            db.Salas.Add(sala);
            await db.SaveChangesAsync();

            return Results.Created($"Sala de id = {sala.SalaId} criada", sala);
        }).WithTags("Salas").RequireAuthorization();

        app.MapPut("/salas/{id:int}", async (AppDbContext db, Sala sala, int id) =>
        {
            if (sala.SalaId != id) return Results.BadRequest();

            var salaDB = await db.Salas.FindAsync(id);

            if (salaDB is null) return Results.NotFound();

            salaDB = new Sala
            {
                SalaId = sala.SalaId,
                Numero = sala.Numero
            };

            await db.SaveChangesAsync();

            return Results.Ok(salaDB);
        }).WithTags("Salas").RequireAuthorization();

        app.MapDelete("/salas/{id:int}", async (AppDbContext db, int id) =>
        {
            var sala = await db.Salas.FindAsync(id);

            if (sala is null) return Results.NotFound();

            db.Salas.Remove(sala);
            await db.SaveChangesAsync();

            return Results.NoContent();
        }).WithTags("Salas").RequireAuthorization();
    }
}
