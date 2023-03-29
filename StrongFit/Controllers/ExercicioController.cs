using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class ExercicioController : Controller
    {
        public Context context;

        public ExercicioController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Exercicios.Include(c => c.Categoria));
        }
        public IActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(context.Categorias.OrderBy(c => c.Nome), "CategoriaID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Exercicio exercicio)
        {
            context.Add(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var exercicio = context.Exercicios.Include(c => c.Categoria).First(e => e.ExercicioID == id);
            return View(exercicio);
        }
        public IActionResult Edit(int id)
        {
            var exercicio = context.Exercicios.Find(id);
            ViewBag.CategoriaID = new SelectList(context.Categorias.OrderBy(c => c.Nome), "CategoriaID", "Nome");
            return View(exercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Exercicio exercicio)
        {
            context.Entry(exercicio).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var exercicio = context.Exercicios.Include(c => c.Categoria).First(e => e.ExercicioID == id);
            return View(exercicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Exercicio exercicio)
        {
            context.Remove(exercicio);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
