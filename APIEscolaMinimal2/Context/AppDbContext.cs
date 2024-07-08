using APIEscolaMinimal2.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEscolaMinimal2.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Sala> Salas { get; set; }
}
