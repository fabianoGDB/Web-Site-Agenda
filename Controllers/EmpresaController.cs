using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Site.Agenda.Database;
using Web.Site.Agenda.Models;
using X.PagedList;

namespace Web.Site.Agenda.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmpresaController(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IList<Empresa> empresas = await PagedListExtensions.ToListAsync(_context.Empresas);
            return View(empresas);
        }

        [HttpGet]
        public IActionResult CriarEmpresa()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarEmpresa(Empresa empresa, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string dir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nome = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(dir, nome), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        empresa.Foto = "~/img/" + nome;
                    }
                }
                else
                {
                    empresa.Foto = "~/img/default.png";
                }

                _context.Add(empresa);
                await _context.SaveChangesAsync();
                TempData["EmpresaNova"] = $"Empresa adicionada com sucesso {empresa.Nome}";
                
                return RedirectToAction(nameof(CriarEmpresa));
            }

            return View(empresa);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarEmpresa(int id)
        {
            Empresa empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            TempData["Foto"] = empresa.Foto;
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEmpresa(Empresa empresa, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string dir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nome = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(dir, nome), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        empresa.Foto = "~/img/" + nome;
                    }
                }
                else
                {
                    empresa.Foto = TempData["Foto"].ToString();
                }

                _context.Update(empresa);
                await _context.SaveChangesAsync();
                TempData["EmpresaAtualizada"] = $"A empresa {empresa.Nome} foi alterada com sucesso";
                return RedirectToAction(nameof(Index));
            }

            return View(empresa);
        }

        [HttpPost]
        public async Task<JsonResult> ExcluirEmpresa(int id)
        {
            Empresa empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            TempData["EmpresaExcluida"] = $"Empresa ${empresa.Nome} foi excluida";
            return Json(true);
        }
        
    }
}