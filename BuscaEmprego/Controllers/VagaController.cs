using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaEmprego.Database;
using BuscaEmprego.Helpers;

namespace BuscaEmprego.Controllers
{
    public class VagaController : Controller
    {
        private BuscaEmpregoEntities db = new BuscaEmpregoEntities();

        //
        // GET: /Vaga/Details/5
        public ActionResult Details(int id)
        {
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        //Candidatar-se
        // POST: /Vaga/Details/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Details(Vaga vaga)
        {
            var idUsuario = int.Parse(Session["user_id"].ToString());
            var vagaUsuario = new Vaga_Usuario();
            vagaUsuario.Usuario_Id = idUsuario;
            vagaUsuario.Vaga_Id = vaga.Id;
            vagaUsuario.Data_Candidatura = DateTime.Now;
            db.Vaga_Usuario.Add(vagaUsuario);
            db.SaveChanges();
            //mensagem de sucesso
        }

        //
        // GET: /Vaga/Create
        public ActionResult Create()
        {
            var model = new Vaga();
            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1");
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email");

            model.Perfil = PerfilHelper.PopularPerfil().ToList();

            return View(model);
        }

        //
        // POST: /Vaga/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vaga vaga)
        {
            if (ModelState.IsValid && Session["user_id"] != null)
            {
                var idUsuario = int.Parse(Session["user_id"].ToString());
                vaga.Usuario = db.Usuario.Where(x => x.Id == idUsuario).FirstOrDefault();
                vaga.Data_Cadastro = DateTime.Now;
                db.Vaga.Add(vaga);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
            vaga.Perfil = PerfilHelper.PopularPerfil().ToList();
            return View(vaga);
        }

        //
        // GET: /Vaga/Edit/5
        public ActionResult Edit(int id)
        {
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
            return View(vaga);
        }

        //
        // POST: /Vaga/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
            return View(vaga);
        }

        //
        // GET: /Vaga/Delete/5
        public ActionResult Delete(int id)
        {
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        //
        // POST: /Vaga/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Vaga vaga)
        {
            //Vaga vaga = db.Vaga.Find(id);
            db.Vaga.Remove(vaga);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}