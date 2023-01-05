using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Udemy.Models;
using Udemy.Models.ViewModels;
using Udemy.Services;
using Udemy.Services.Exceptions;

namespace Udemy.Controllers;

public class VendedoresController : Controller {
    private readonly VendedorService _vendedorService;
    private readonly DepartamentoService _departamentoService;

    public VendedoresController(VendedorService vs, DepartamentoService ds) {
        _vendedorService = vs;
        _departamentoService = ds;
    }

    // GET
    async public Task<IActionResult> Index() {
        // exibe todos os vendedores do banco de dados
        return View(await _vendedorService.BuscarTodos());
    }

    // página de criação
    async public Task<IActionResult> Create() {
        var modelo = new VendedorFormViewModel { Departamentos = await _departamentoService.BuscarTodos() };
        return View(modelo);
    }

    // ação de criação
    [HttpPost]
    [ValidateAntiForgeryToken]
    async public Task<IActionResult> Create(VendedorFormViewModel modelo) {
        if (!ModelState.IsValid) return View(modelo);
        await _vendedorService.Inserir(modelo.Vendedor);
        return RedirectToAction(nameof(Index));
    }

    async public Task<IActionResult> Delete(int? id) {
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = await _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });

        return View(v);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    async public Task<IActionResult> Delete(int id) {
        try {
            await _vendedorService.Remover(id);
            return RedirectToAction(nameof(Index));
        }
        catch (IntegrityException e) { return RedirectToAction(nameof(Error), new { e.Message }); }
    }

    async public Task<IActionResult> Details(int? id) {
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = await _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });

        return View(v);
    }

    async public Task<IActionResult> Edit(int? id) {
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = await _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });
        List<Departamento> deps = await _departamentoService.BuscarTodos();
        var modelo = new VendedorFormViewModel { Vendedor = v, Departamentos = deps };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    async public Task<IActionResult> Edit(int id, VendedorFormViewModel modelo) {
        if (!ModelState.IsValid) return View(modelo);
        if (id != modelo.Vendedor.Id) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Os ids não são iguais" });

        try {
            await _vendedorService.Atualizar(modelo.Vendedor);
            return RedirectToAction(nameof(Index));
        }
        catch (ApplicationException e) { return RedirectToAction(nameof(Error), new ErrorViewModel { Message = e.Message }); }
    }

    public IActionResult Error(string msg) {
        return View(new ErrorViewModel {
            Message = msg,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}