using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    public class PersonalController : Controller
    {
        public Context context;

        public PersonalController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Personals);
        }
        public IActionResult Create()
        {
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(f => f.Nome), "PersonalID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Personal personal)
        {
            context.Add(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var produto = context.Personals.FirstOrDefault(p => p.PersonalID == id); ;
            return View(produto);
        }

        public IActionResult Edit(int id)
        {
            var personal = context.Personals.Find(id);
            ViewBag.PersonalID = new SelectList(context.Personals.OrderBy(f => f.Nome), "PersonalID", "Nome");
            return View(personal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Personal personal)
        {
            context.Entry(personal).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var personal = context.Personals.FirstOrDefault(p => p.PersonalID == id);
            return View(personal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Personal personal)
        {
            context.Remove(personal);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
