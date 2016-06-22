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

        //
        // POST: /Vaga/Details/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Vaga vaga)
        {
            var tipoUsuario = int.Parse(Session["tipo_usuario"].ToString());
            if (tipoUsuario == 1)
            {
                Approve(vaga);
            }
            else
            {
                Apply(vaga);
            }

            return View(db.Vaga.Find(vaga.Id));
        }

        private void Apply(Vaga vaga)
        {
            var idUsuario = int.Parse(Session["user_id"].ToString());
            var vagaUsuario = db.Vaga_Usuario.Where(x => x.Usuario_Id == idUsuario).FirstOrDefault();
            if (vagaUsuario != null)
            {
                ModelState.AddModelError(String.Empty, "Usuário já é candidato dessa Vaga.");
                return;
            }

            vagaUsuario = new Vaga_Usuario();
            vagaUsuario.Usuario_Id = idUsuario;
            vagaUsuario.Vaga_Id = vaga.Id;
            vagaUsuario.Data_Candidatura = DateTime.Now;
            db.Vaga_Usuario.Add(vagaUsuario);
            db.SaveChanges();

            ViewBag.Sucesso = "Candidatura realizada com sucesso.";
        }

        private void Approve(Vaga vaga)
        {
            var ids = Request["check_candidato"].Split(',');

            if (ids == null || ids.Length < 1)
            {
                ModelState.AddModelError(String.Empty, "Nenhum candidato foi selecionado.");
                return;
            }

            List<Vaga_Usuario> vagaUsuario = db.Vaga_Usuario.Where(x => x.Vaga_Id == vaga.Id).ToList();

            foreach(Vaga_Usuario vu in vagaUsuario)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    int idUsuario = int.Parse(ids[i]);

                    if (vu.Usuario_Id == idUsuario)
                    {
                        vu.Aprovado = true;
                        vu.Data_Aprovacao = DateTime.Now;
                    }
                    else
                    {
                        vu.Aprovado = false;
                        vu.Data_Aprovacao = DateTime.MinValue;
                    }
                }

                
                db.Entry(vu).State = EntityState.Modified;
                db.SaveChanges();
            }

            ViewBag.Sucesso = "Aprovação dos candidatos realizada com sucesso.";
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
                if (vaga.Ativa && vaga.Data_Ativacao == null)
                    vaga.Data_Ativacao = DateTime.Now;

                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Find");
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
            vaga.Data_Cancelamento = DateTime.Now;
            vaga.Ativa = false;
            db.Entry(vaga).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Find()
        {
            var vagas = db.Vaga;
            var listVagas = new List<Vaga>();
            if (Session["tipo_usuario"] != null && int.Parse(Session["tipo_usuario"].ToString()) == 1)
            {
                if (Session["user_id"] != null)
                {
                    int idUsuario = int.Parse(Session["user_id"].ToString());
                    listVagas = vagas.Where(x => x.Empresa_Id == idUsuario).ToList();
                }
            }
            else
            {
                listVagas = vagas.ToList();
            }

            if (listVagas == null || listVagas.Count < 1)
            {
                ModelState.AddModelError(String.Empty, "Não existe Vagas.");
                listVagas = new List<Vaga>();
            }

            return View(listVagas);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}