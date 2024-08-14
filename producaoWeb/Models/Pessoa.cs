using System.ComponentModel.DataAnnotations;

public class Pessoa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório!")]
    public string Nome { get; set; }

    public Funcao? funcao { get; set; }

    public int Idade { get; set; }

    public DateTime CriadoEm { get; set; }
    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
        CriadoEm = DateTime.Now;
    }
}