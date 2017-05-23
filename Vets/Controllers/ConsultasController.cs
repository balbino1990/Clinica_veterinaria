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
    public class ConsultasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consultas
        public ActionResult Index()
        {
            var consultas = db.Consultas.Include(c => c.Animal).Include(c => c.Veterinario);
            return View(consultas.ToList().OrderBy(d=>d.ConsultaID));
        }

        // GET: Consultas/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = db.Consultas.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // GET: Consultas/Criar
        public ActionResult Criar()
        {
            ViewBag.AnimalFK = new SelectList(db.Animais, "AnimalID", "Nome");
            ViewBag.VeterinarioFK = new SelectList(db.Veterinarios, "VeterinarioID", "Nome");
            return View();
        }

        // POST: Consultas/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "ConsultaID,DataConsulta,VeterinarioFK,AnimalFK")] Consultas consultas)
        {
            if (ModelState.IsValid)
            {
                db.Consultas.Add(consultas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnimalFK = new SelectList(db.Animais, "AnimalID", "Nome", consultas.AnimalFK);
            ViewBag.VeterinarioFK = new SelectList(db.Veterinarios, "VeterinarioID", "Nome", consultas.VeterinarioFK);
            return View(consultas);
        }

        // GET: Consultas/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = db.Consultas.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnimalFK = new SelectList(db.Animais, "AnimalID", "Nome", consultas.AnimalFK);
            ViewBag.VeterinarioFK = new SelectList(db.Veterinarios, "VeterinarioID", "Nome", consultas.VeterinarioFK);
            return View(consultas);
        }

        // POST: Consultas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "ConsultaID,DataConsulta,VeterinarioFK,AnimalFK")] Consultas consultas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnimalFK = new SelectList(db.Animais, "AnimalID", "Nome", consultas.AnimalFK);
            ViewBag.VeterinarioFK = new SelectList(db.Veterinarios, "VeterinarioID", "Nome", consultas.VeterinarioFK);
            return View(consultas);
        }

        // GET: Consultas/Apagar/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultas consultas = db.Consultas.Find(id);
            if (consultas == null)
            {
                return HttpNotFound();
            }
            return View(consultas);
        }

        // POST: Consultas/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultas consultas = db.Consultas.Find(id);
            db.Consultas.Remove(consultas);
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
