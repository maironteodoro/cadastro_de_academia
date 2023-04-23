using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;
using System.Linq;

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
            //usada para verificar se os dados recebidos de uma solicitação HTTP são válidos
            if (ModelState.IsValid)
            {
                context.Treinos.Add(treino);
                context.SaveChanges();
                return View(treino);
            }
            ViewBag.AlunoID = new SelectList(context.Alunos.OrderBy(a => a.Nome), "AlunoID", "Nome", treino.AlunoID);
            return View("Index");
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
        public IActionResult Edit(int id)
        {
            var treino = context.Treinos.Find(id);
            ViewBag.Exercicios = context.Exercicios.ToList();
            ViewBag.AlunoID = new SelectList(context.Alunos.OrderBy(a => a.Nome), "AlunoID", "Nome");
            ViewBag.TreinoID = new SelectList(context.Treinos.OrderBy(f => f.TreinoID), "TreinoID");
            ViewBag.ExercicioTreino = context.ExercicioTreinos
                                                                .Where(et => et.TreinoID == id)
                                                                .Select(et => new {
                                                                    ExercicioTreinoID = et.ExercicioTreinoID,
                                                                    NomeExercicio = et.Exercicio.Nome
                                                                })
                                                                .ToList();


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
            var exerciciosTreino = context.ExercicioTreinos.Where(et => et.TreinoID == treino.TreinoID).ToList();
            foreach (var item in exerciciosTreino)
            {
                context.ExercicioTreinos.Remove(item);
            }
            context.Remove(treino);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AdicionarExercicio()//add uma nova relação de exerciciotreino
        {
            ViewBag.Exercicios = context.Exercicios.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarExercicio(ExercicioTreino exercicioTreino)//Adicionar um exerciciotreino novo (uso no edit)
        {
            //indica que houve um erro na solicitação do cliente.
            if (exercicioTreino == null)
            {
                return BadRequest();
            }

            context.ExercicioTreinos.Add(exercicioTreino);
            context.SaveChanges();

            return RedirectToAction("Edit", new { id = exercicioTreino.TreinoID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverExercicio(int exercicioTreinoId)
        {
            var exercicioTreino = context.ExercicioTreinos.Find(exercicioTreinoId);

            if (exercicioTreino == null)
            {
                return NotFound();
            }

            context.ExercicioTreinos.Remove(exercicioTreino);
            context.SaveChanges();

            return RedirectToAction("Edit", new { id = exercicioTreino.TreinoID });
        }

    }
}
