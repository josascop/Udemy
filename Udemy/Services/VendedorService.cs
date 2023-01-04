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

    async public Task<List<Vendedor>> BuscarTodos() {
        return await _ctx.Vendedor.ToListAsync();
    }

    async public Task Inserir(Vendedor vnd) {
        _ctx.Add(vnd);
        await _ctx.SaveChangesAsync();
    }

    async public Task<Vendedor?> BuscarPorId(int id) {
        return await _ctx.Vendedor.Include(vnd => vnd.Departamento).FirstOrDefaultAsync(vnd => vnd.Id == id);
    }

    async public Task Remover(int id) {
        var v = await _ctx.Vendedor.FindAsync(id);
        if (v is null) return;
        _ctx.Vendedor.Remove(v);
        await _ctx.SaveChangesAsync();
    }

    async public Task Atualizar(Vendedor vnd) {
        if (!await _ctx.Vendedor.AnyAsync(v => v.Id == vnd.Id))
            throw new NotFoundException("Id não encontrado");

        try {
            _ctx.Update(vnd);
            await _ctx.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e) {
            throw new DBConcurrencyException(e.Message);
        }
    }
}