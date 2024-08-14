using System.ComponentModel.DataAnnotations;

public class Funcao
{
    [Key]
    public string? Id { get; set; }
    [Required(ErrorMessage = "Campo obrigatório!")]
    public string? Nome { get; set; }
    public int salario { get; set; }

}