namespace Udemy.Models;

public class Vendedor {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public decimal Salariobase { get; set; }
    public DateTime DataDeNascimento { get; set; }
    public Departamento Departamento { get; set; }
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