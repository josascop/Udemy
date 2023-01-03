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
    public IActionResult Index() {
        // exibe todos os vendedores do banco de dados
        return View(_vendedorService.BuscarTodos());
    }

    // página de criação
    public IActionResult Create() {
        var modelo = new VendedorFormViewModel { Departamentos = _departamentoService.BuscarTodos() };
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
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });

        return View(v);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id) {
        _vendedorService.Remover(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int? id) {
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });

        return View(v);
    }

    public IActionResult Edit(int? id) {
        if (id is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Id inválido" });

        Vendedor? v = _vendedorService.BuscarPorId(id.Value);
        if (v is null) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Vendedor não encontrado" });
        List<Departamento> deps = _departamentoService.BuscarTodos();
        var modelo = new VendedorFormViewModel { Vendedor = v, Departamentos = deps };

        return View(modelo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VendedorFormViewModel modelo) {
        if (id != modelo.Vendedor.Id) return RedirectToAction(nameof(Error), new ErrorViewModel { Message = "Os ids não são iguais" });

        try {
            _vendedorService.Atualizar(modelo.Vendedor);
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