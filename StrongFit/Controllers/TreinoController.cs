using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class TreinoController : Controller
    {
        public Context context;

        public TreinoController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Treinos.Include(a => a.aluno));
        }
        public IActionResult Create()
        {
            ViewBag.AlunoID = new SelectList(context.Alunos.OrderBy(a => a.Nome), "AlunoID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Treino treino)
        {
            context.Add(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var exerciciosTreino = context.ExercicioTreinos.Where(et => et.TreinoID == id).ToList();
            var treino = context.Treinos.Include(t => t.aluno).FirstOrDefault(t => t.TreinoID == id);

            if (treino == null)
            {
                return NotFound();
            }

            var exercicioIds = exerciciosTreino.Select(et => et.ExercicioID).ToList();
            var exercicios = context.Exercicios.Include(e => e.Categoria)
                .Where(e => exercicioIds.Contains(e.ExercicioID)).ToList();

            ViewBag.Exercicios = exercicios;

            return View(treino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Treino treino)
        {
            context.Entry(treino).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var treino = context.Treinos.Include(a => a.aluno).First(t => t.TreinoID == id);
            return View(treino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Treino treino)
        {
            context.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
