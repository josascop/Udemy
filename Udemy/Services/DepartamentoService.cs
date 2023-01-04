using Microsoft.EntityFrameworkCore;
using Udemy.Data;
using Udemy.Models;

namespace Udemy.Services;
public class DepartamentoService {
    private readonly UdemyContext _ctx;

    public DepartamentoService(UdemyContext ctx) {
        _ctx = ctx;
    }

    async public Task<List<Departamento>> BuscarTodos() {
        return await _ctx.Departamento.OrderBy(d => d.Nome).ToListAsync();
    }
}
