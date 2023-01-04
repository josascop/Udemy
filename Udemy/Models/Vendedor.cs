using System.ComponentModel.DataAnnotations;

namespace Udemy.Models;

public class Vendedor {
    public int Id { get; set; }

    [Required(ErrorMessage = "informe um {0}")]
    [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "o nome deve ter entre {2} e {1} caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "informe um {0}")]
    [EmailAddress(ErrorMessage = "o email informado é inválido")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "informe um {0}")]
    [Display(Name = "Salário base")]
    [Range(100, 50000, ErrorMessage = "o {0} deve ser entre {1} e {2}")]
    public decimal Salariobase { get; set; }

    [Required(ErrorMessage = "informe uma {0}")]
    [Display(Name = "Data de nascimento"), DataType(DataType.Date)]
    public DateTime DataDeNascimento { get; set; }

    public Departamento Departamento { get; set; }

    [Display(Name = "Departamento")]
    public int DepartamentoId { get; set; }
    public ICollection<RegistroDeVenda> Vendas { get; set; } = new List<RegistroDeVenda>();

    public Vendedor() { }

    public Vendedor(int id, string nome, string email, decimal salariobase, DateTime dataDeNascimento,
        Departamento departamento) {
        Id = id;
        Nome = nome;
        Email = email;
        Salariobase = salariobase;
        DataDeNascimento = dataDeNascimento;
        Departamento = departamento;
    }

    public void AdicionarVenda(RegistroDeVenda rv) {
        Vendas.Add(rv);
    }

    public void RemoverVenda(RegistroDeVenda rv) {
        Vendas.Remove(rv);
    }

    public decimal TotalDeVendas(DateTime inicial, DateTime final) {
        return Vendas.Where(registro => registro.Data >= inicial && registro.Data <= final)
            .Sum(registro => registro.Quantia);
    }
}