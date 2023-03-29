using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class CategoriaController : Controller
    {
        public Context context;

        public CategoriaController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Categorias);
        }
        public IActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(context.Categorias.OrderBy(f => f.Nome), "CategoriaID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            context.Add(categoria);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var categoria = context.Categorias.FirstOrDefault(c => c.CategoriaID == id); ;
            return View(categoria);
        }

        public IActionResult Edit(int id)
        {
            var categoria = context.Categorias.Find(id);
            ViewBag.CategoriaID = new SelectList(context.Categorias.OrderBy(f => f.Nome), "CategoriaID", "Nome");
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            context.Entry(categoria).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var categoria = context.Categorias.FirstOrDefault(c => c.CategoriaID == id); ;
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Categoria categoria)
        {
            context.Remove(categoria);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
