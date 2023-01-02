using Udemy.Data;
using Udemy.Models;

namespace Udemy.Services;

public class VendedorService {
    private readonly UdemyContext _ctx;

    public VendedorService(UdemyContext ctx) {
        _ctx = ctx;
    }

    public List<Vendedor> BuscarTodos() {
        return _ctx.Vendedor.ToList();
    }

    public void Inserir(Vendedor vnd) {
        _ctx.Add(vnd);
        _ctx.SaveChanges();
    }

    public Vendedor? BuscarPorId(int id) {
        return _ctx.Vendedor.FirstOrDefault(vnd => vnd.Id == id);
    }

    public void Remover(int id) {
        var v = _ctx.Vendedor.Find(id);
        if (v is null) return;
        _ctx.Vendedor.Remove(v);
        _ctx.SaveChanges();
    }
}