using Microsoft.AspNetCore.Mvc;
using Udemy.Models;
using Udemy.Services;

namespace Udemy.Controllers;
public class RegistroDeVendasController : Controller {
    private readonly RegistroDeVendaService _serviceRegistro;

    public RegistroDeVendasController(RegistroDeVendaService serviceRegistro) {
        _serviceRegistro = serviceRegistro;
    }

    public IActionResult Index() {
        return View();
    }
    public async Task<IActionResult> BuscaSimples(DateTime? dataMin, DateTime? dataMax) {
        // valores padrão caso não seja passada data
        // são passados para a view para aparecerem sempre no cabeçalho
        if (!dataMin.HasValue) dataMin = new DateTime(DateTime.Now.Year, 1, 1);
        if (!dataMax.HasValue) dataMax = DateTime.Now;

        ViewData["dataMin"] = dataMin.Value.ToString("dd-MM-yyyy");
        ViewData["dataMax"] = dataMax.Value.ToString("dd-MM-yyyy");

        return View(await _serviceRegistro.BuscarPorData(dataMin, dataMax));
    }

    public async Task<IActionResult> BuscaEmGrupo(DateTime? dataMin, DateTime? dataMax) {
        if (!dataMin.HasValue) dataMin = new DateTime(DateTime.Now.Year, 1, 1);
        if (!dataMax.HasValue) dataMax = DateTime.Now;

        ViewData["dataMin"] = dataMin.Value.ToString("dd-MM-yyyy");
        ViewData["dataMax"] = dataMax.Value.ToString("dd-MM-yyyy");

        return View(await _serviceRegistro.BuscarPorDataEmGrupo(dataMin, dataMax));
    }
}
