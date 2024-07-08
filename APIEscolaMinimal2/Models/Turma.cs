using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIEscolaMinimal2.Models;

[Table("turma")]
public class Turma
{
    public int TurmaId { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão do nome da turma")]
    [StringLength(12, ErrorMessage = "Nome da turma deve ter no máximo {1} caracteres")]
    public string? Ano { get; set; } //Primário, 1º, 2º, 3º...

    [Required(ErrorMessage = "É obrigatório inclusão de letra sequência da turma")]
    [StringLength(1, ErrorMessage = "Sequência da turma deve ter no máximo {1} caractere")]
    public string? Sequencia { get; set; } //A, B, C...

    //[Required(ErrorMessage = "É obrigatório inclusão de id da sala na turma")]
    public int? SalaId { get; set; }

    public Sala? Sala { get; set; }

    public string? Nome
    {
        get
        {
            return $"{Ano}{Sequencia}";
        }
    }

    public ICollection<Aluno>? Alunos { get; set; }
}
