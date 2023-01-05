using Microsoft.EntityFrameworkCore;
using Udemy.Data;
using Udemy.Models;

namespace Udemy.Services;
public class RegistroDeVendaService {

    private readonly UdemyContext _ctx;

    public RegistroDeVendaService(UdemyContext ctx) {
        _ctx = ctx;
    }

    public async Task<List<RegistroDeVenda>> BuscarPorData(DateTime? inicial, DateTime? final) {
        var resultado = from reg in _ctx.RegistroDeVenda select reg;

        if (inicial.HasValue) resultado = resultado.Where(r => r.Data >= inicial.Value);

        if (final.HasValue) resultado = resultado.Where(r => r.Data <= final.Value);

        return await resultado
            .Include(r => r.Atendente)
            .Include(r => r.Atendente.Departamento)
            .OrderByDescending(r => r.Data)
            .ToListAsync();
    }

    public async Task<List<IGrouping<Departamento, RegistroDeVenda>>> BuscarPorDataEmGrupo(DateTime? inicial, DateTime? final) {
        var resultado = from reg in _ctx.RegistroDeVenda select reg;

        if (inicial.HasValue) resultado = resultado.Where(r => r.Data >= inicial.Value);

        if (final.HasValue) resultado = resultado.Where(r => r.Data <= final.Value);

        return await resultado
            .Include(r => r.Atendente)
            .Include(r => r.Atendente.Departamento)
            .OrderByDescending(r => r.Data)
            .GroupBy(r => r.Atendente.Departamento)
            .ToListAsync();
    }
}
