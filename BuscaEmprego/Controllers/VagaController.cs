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
        public ActionResult Details(int id = 0)
        {
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
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
            if (ModelState.IsValid)
            {
                db.Vaga.Add(vaga);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
            vaga.Perfil = PerfilHelper.PopularPerfil().ToList();
            return View(vaga);
        }

        //
        // GET: /Vaga/Edit/5
        public ActionResult Edit(int id = 0)
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
        // POST: /Vaga/Edit/5
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
        public ActionResult Delete(int id = 0)
        {
            Vaga vaga = db.Vaga.Find(id);
            if (vaga == null)
            {
                return HttpNotFound();
            }
            return View(vaga);
        }

        //
        // POST: /Vaga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vaga vaga = db.Vaga.Find(id);
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