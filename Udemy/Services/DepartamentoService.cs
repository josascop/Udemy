using Udemy.Data;
using Udemy.Models;

namespace Udemy.Services;
public class DepartamentoService {
    private readonly UdemyContext _ctx;

    public DepartamentoService(UdemyContext ctx) {
        _ctx = ctx;
    }

    public List<Departamento> BuscarTodos() {
        return _ctx.Departamento.OrderBy(d => d.Nome).ToList();
    }
}
