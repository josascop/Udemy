using Microsoft.EntityFrameworkCore;
using Udemy.Data;
using Udemy.Models;
using Udemy.Services.Exceptions;

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
        return _ctx.Vendedor.Include(vnd => vnd.Departamento).FirstOrDefault(vnd => vnd.Id == id);
    }

    public void Remover(int id) {
        var v = _ctx.Vendedor.Find(id);
        if (v is null) return;
        _ctx.Vendedor.Remove(v);
        _ctx.SaveChanges();
    }

    public void Atualizar(Vendedor vnd) {
        if (!_ctx.Vendedor.Any(v => v.Id == vnd.Id))
            throw new NotFoundException("Id não encontrado");

        try {
            _ctx.Update(vnd);
            _ctx.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e) {
            throw new DBConcurrencyException(e.Message);
        }
    }
}