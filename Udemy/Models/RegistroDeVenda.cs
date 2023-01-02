namespace Udemy.Models;
public class RegistroDeVenda {
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public decimal Quantia { get; set; }
    public StatusDeVenda Status { get; set; }
    public Vendedor Atendente { get; set; }

    public RegistroDeVenda() { }

    public RegistroDeVenda(int id, DateTime data, decimal quantia, StatusDeVenda status, Vendedor atendente) {
        Id = id;
        Data = data;
        Quantia = quantia;
        Status = status;
        Atendente = atendente;
    }
}
