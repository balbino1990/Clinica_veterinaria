using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vets.Models;

namespace Vets.Controllers
{
    public class AnimaisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Animais
        public ActionResult Index()
        {
            var animais = db.Animais.Include(a => a.Dono);
            return View(animais.ToList().OrderBy(d=>d.AnimalID));
        }

        // GET: Animais/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animais animais = db.Animais.Find(id);
            if (animais == null)
            {
                return HttpNotFound();
            }
            return View(animais);
        }

        // GET: Animais/Criar
        public ActionResult Criar()
        {
            ViewBag.DonosFK = new SelectList(db.Donos, "DonoID", "Nome");
            return View();
        }

        // POST: Animais/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "AnimalID,Nome,Especie,Raca,Peso,Altura,DonosFK")] Animais animais)
        {
            if (ModelState.IsValid)
            {
                db.Animais.Add(animais);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonosFK = new SelectList(db.Donos, "DonoID", "Nome", animais.DonosFK);
            return View(animais);
        }

        // GET: Animais/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animais animais = db.Animais.Find(id);
            if (animais == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonosFK = new SelectList(db.Donos, "DonoID", "Nome", animais.DonosFK);
            return View(animais);
        }

        // POST: Animais/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "AnimalID,Nome,Especie,Raca,Peso,Altura,DonosFK")] Animais animais)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animais).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonosFK = new SelectList(db.Donos, "DonoID", "Nome", animais.DonosFK);
            return View(animais);
        }

        // GET: Animais/Apagar/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animais animais = db.Animais.Find(id);
            if (animais == null)
            {
                return HttpNotFound();
            }
            return View(animais);
        }

        // POST: Animais/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animais animais = db.Animais.Find(id);
            db.Animais.Remove(animais);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
