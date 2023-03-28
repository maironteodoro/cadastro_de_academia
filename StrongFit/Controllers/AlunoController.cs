using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class AlunoController : Controller
    {
        public Context context;

        public AlunoController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Alunos.Include(p => p.personal));
        }
        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(f => f.Nome), "PersonalID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Aluno aluno)
        {
            context.Add(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var aluno = context.Alunos.Include(p => p.personal).First(a => a.AlunoID == id);
            return View(aluno);
        }

        public IActionResult Edit(int id)
        {
            var aluno = context.Alunos.Find(id);
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(f => f.Nome), "PersonalID", "Nome");
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Aluno aluno)
        {
            context.Entry(aluno).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var aluno = context.Alunos.Include(f => f.personal).FirstOrDefault(p => p.AlunoID == id);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Aluno aluno)
        {
            context.Remove(aluno);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
