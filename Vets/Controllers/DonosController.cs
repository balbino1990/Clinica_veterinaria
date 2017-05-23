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

        // GET: Donos/Detailhes/5
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
        [ValidateAntiForgeryToken]   //Para evitar algum ataque ou falsificação dos dados dos utilizadores
        public ActionResult Criar([Bind(Include = "DonoID,Nome,NIF")] Donos dono)     //Só vai listar ou aceitar os atributos definidos ('DonoID, Nome, NIF')
        {
            //determinar o nº (ID) a atribuir ao novo DONO
            //criar a variavel, que recebe esse valor
            int noovoID = 0;

            try
            {
                //##############################
                //determinar o novo ID
                //noovoID = (from d in db.Donos
                //           orderby d.DonoID descending
                //           select d.DonoID).FirstOrDefault() + 1;    //firsOrDefault ===> limit 1 ()
                //#####################################################

                noovoID = db.Donos.Max(d => d.DonoID) + 1;
                //select max(d.DonoID)" 
                //from donos d

               
            }
            catch (System.Exception)
            {
                // a tabela de 'DONOS' está vazia
                // não sendo possivel devolver o MAX de uma tabela
                // por isso, vou atribuir 'manualmente' o valor do 'noovoID'
                noovoID = 1;
            }

            //atribuir o 'novoID' ao objeto 'dono'
            dono.DonoID = noovoID;
            try
            {
                // nõa gera uma seção porque vai verificar o base de dados
                if (ModelState.IsValid)
                {
                    //Vai adicionar para a tabela 'Donos' do base de dados 'VetsDB'
                    db.Donos.Add(dono);
                    // salvar as alterações na base de dados
                    db.SaveChanges(); 
                    //retornar e redirecionar para o ação ou view 'Index' 
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                // não consigo guardar as alterações 
                //No minimo, preciso de notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Ocorreu um erro na adição do novo Dono.");
                //notificar o 'administrador/programador' que ocorreu este erro
                //fazer: 1º enviar email ao programador a informar da ocorrência do erro
                // 2º ter uma tabela, na BD onde são reportados os erros: 
                //  -data
                //  -método
                //  -controller
                //  -detalhes do erro

            }


            //retornar para o 'View' da tabela 'Donos'
            return View(dono);
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

        // GET: Donos/Apagar/5
        public ActionResult Apagar(int? id)
        {

            //avalia se o parâmetro é nulo
            if (id == null)
            {
                //redireciona para o inicio
                return RedirectToAction("Index");
                    //new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Não existe o dono na lista");
            }
            //procurar o dono na base dados, cuja chave primaria é igual ao parâmetro fornecido
            Donos dono = db.Donos.Find(id);
            //Se o dono não é encontrado
            if (dono == null)
            {
                //redireciona para o inicio
                return RedirectToAction("Index");
            }
            //retorna para o View do 'Donos'
            return View(dono);
        }

        // POST: Donos/Apagar/5
        [HttpPost, ActionName("Apagar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            //procurar o dono na base dados, cuja chave primaria é igual ao parâmetro fornecido
            Donos dono = db.Donos.Find(id);

            try
            {
                //remove do objeto 'db', o dono encontrado na linha anterior
                db.Donos.Remove(dono);
                //torna definitivas as instruções anteriores
                db.SaveChanges();
            }
            catch (System.Exception){
                // gerar uma mensnagem de erro, a ser entregue ao utilizador
                ModelState.AddModelError("",
                    string.Format("Ocorreu um erro na operação de eliminar o 'dono' com ID {0} - {1}", id, dono.Nome)
                );
                //regressar à view 'Delete'
                return View(dono);

            }
           
            //devolve o controlo do programa, apresenta a view 'Index'
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
