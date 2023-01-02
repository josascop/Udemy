using Microsoft.AspNetCore.Mvc;
using Udemy.Models;
using Udemy.Models.ViewModels;
using Udemy.Services;

namespace Udemy.Controllers;

public class VendedoresController : Controller {
    private readonly VendedorService _vendedorService;
    private readonly DepartamentoService _departamentoService;

    public VendedoresController(VendedorService vs, DepartamentoService ds) {
        _vendedorService = vs;
        _departamentoService = ds;
    }

    // GET
    public IActionResult Index() {
        // exibe todos os vendedores do banco de dados
        return View(_vendedorService.BuscarTodos());
    }

    // página de criação
    public IActionResult Create() {
        VendedorFormViewModel modelo = new VendedorFormViewModel { Departamentos = _departamentoService.BuscarTodos() };
        return View(modelo);
    }

    // ação de criação
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VendedorFormViewModel modelo) {
        _vendedorService.Inserir(modelo.Vendedor);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int? id) {
        if (id is null) return NotFound();

        Vendedor? v = _vendedorService.BuscarPorId(id.Value);
        if (v is null) return NotFound();

        return View(v);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id) {
        _vendedorService.Remover(id);
        return RedirectToAction(nameof(Index));
    }
}