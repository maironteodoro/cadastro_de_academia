using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class ExercicioTreinoController:Controller
    {
        public Context context;

        public ExercicioTreinoController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.ExercicioTreinos);
        }
        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(f => f.Nome), "PersonalID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int treinoId,int exercicioId)
        {
            var exerciciotreino = new ExercicioTreino();
            exerciciotreino.TreinoID = treinoId;
            exerciciotreino.ExercicioID = exercicioId;
            context.Add(exerciciotreino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
