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
    public class VeterinariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veterinarios
        public ActionResult Index()
        {
            return View(db.Veterinarios.ToList().OrderBy(d=>d.VeterinarioID));
        }

        // GET: Veterinarios/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarios veterinarios = db.Veterinarios.Find(id);
            if (veterinarios == null)
            {
                return HttpNotFound();
            }
            return View(veterinarios);
        }

        // GET: Veterinarios/Criar
        public ActionResult Criar()
        {
            return View();
        }

        // POST: Veterinarios/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "VeterinarioID,Nome,Rua,NumPorta,Andar,CodPostal,NIF,DataEntradaClinica,NumCedulaProf,DataInscOrdem,Faculdade")] Veterinarios veterinarios)
        {
            if (ModelState.IsValid)
            {
                db.Veterinarios.Add(veterinarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(veterinarios);
        }

        // GET: Veterinarios/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarios veterinarios = db.Veterinarios.Find(id);
            if (veterinarios == null)
            {
                return HttpNotFound();
            }
            return View(veterinarios);
        }

        // POST: Veterinarios/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "VeterinarioID,Nome,Rua,NumPorta,Andar,CodPostal,NIF,DataEntradaClinica,NumCedulaProf,DataInscOrdem,Faculdade")] Veterinarios veterinarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veterinarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(veterinarios);
        }

        // GET: Veterinarios/Apagar/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarios veterinarios = db.Veterinarios.Find(id);
            if (veterinarios == null)
            {
                return HttpNotFound();
            }
            return View(veterinarios);
        }

        // POST: Veterinarios/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veterinarios veterinarios = db.Veterinarios.Find(id);
            db.Veterinarios.Remove(veterinarios);
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
