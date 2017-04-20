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
    public class DonosController : Controller
    {
        private VetsDB db = new VetsDB();

        // GET: Donos
        public ActionResult Index()
        {
            //retornar para o view a lista da tabela Donos de BD 'VetsDB', que vai ordenar pelo o 'DonoID' 
            //de forma ascedente
            return View(db.Donos.ToList().OrderBy(d=>d.DonoID));
        }

        // GET: Donos/Details/5
        public ActionResult Detalhes(int? id)
        {
            //se o id do Dono igual nulo ou não existe
            if (id == null)
            {
                //retornar o novo erro de Http na pagina Web
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //se não, a tabela Dono igual um 'id' que encontrar na tabela Dono (Base de dados 'VetsDB')
            Donos donos = db.Donos.Find(id);
            //se não existia a tabela Donos ou se for igual a nulo
            if (donos == null)
            {
                //vai retornar um erro de Http, que disse a tabela procurada não existe na base de dados
                return HttpNotFound();
            }
            //se não, vai retornar para o views da tabela 'Donos'
            return View(donos);
        }

        // GET: Donos/criar
        public ActionResult Criar()
        {
            //retornar para o VIEW
            return View();
        }

        // POST: Donos/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            // Se o codigo do controller não ocorreu algum erro
            if (ModelState.IsValid)
            {
                //Vai adicionar para a tabela 'Donos' do base de dados 'VetsDB'
                db.Donos.Add(donos);
                // Guarda as alterações ou o novo dono se não existe nenhum erro
                db.SaveChanges();
                //retornar e redirecionar para o ação ou view 'Index' 
                return RedirectToAction("Index");
            }
            //retornar para o 'View' da tabela 'Donos'
            return View(donos);
        }

        // GET: Donos/Editar/5
        public ActionResult Editar(int? id)
        {
            //Se não encontra o 'id' do dono
            if (id == null)
            {
                // retornar o erro de estado de Http
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //se não, a tabela Dono igual um 'id' que encontrar na tabela Dono (Base de dados 'VetsDB')
            Donos donos = db.Donos.Find(id);
            //se não existia a tabela Donos ou se for igual a nulo 
            if (donos == null)
            {
                // vai retornar um erro do Http que se disse 'não existe'
                return HttpNotFound();
            }
            //retornar para o View da tabela 'Donos'
            return View(donos);
        }

        // POST: Donos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "DonoID,Nome,NIF")] Donos donos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donos);
        }

        // GET: Donos/Delete/5
        public ActionResult Apagar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donos donos = db.Donos.Find(id);
            if (donos == null)
            {
                return HttpNotFound();
            }
            return View(donos);
        }

        // POST: Donos/Delete/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donos donos = db.Donos.Find(id);
            db.Donos.Remove(donos);
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
