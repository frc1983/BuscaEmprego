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
                Approve(vaga);
            else
                Apply(vaga);

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

            TempData["sucesso"] = "Candidatura realizada com sucesso.";
        }

        private void Approve(Vaga vaga)
        {
            var ids = Request["check_candidato"] == null ? null : Request["check_candidato"].Split(',');

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

                vaga = db.Vaga.Find(vaga.Id);
                vaga.Data_Preenchimento_Vaga = DateTime.Now;
                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();
            }

            TempData["sucesso"] = "Aprovação dos candidatos realizada com sucesso.";
        }

        //
        // GET: /Vaga/Create
        public ActionResult Create()
        {
            var model = new Vaga();
            ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1");
            ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email");
            
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
                try
                {
                    var ids = Request["check_perfil"].Split(',');
                    vaga.Perfil = new List<Perfil>();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = int.Parse(ids[i]);
                        vaga.Perfil.Add(db.Perfil.Where(x => x.Id == id).First());
                    }

                    var idUsuario = int.Parse(Session["user_id"].ToString());
                    vaga.Usuario = db.Usuario.Where(x => x.Id == idUsuario).FirstOrDefault();
                    vaga.Data_Cadastro = DateTime.Now;
                    db.Vaga.Add(vaga);
                    db.SaveChanges();

                    TempData["sucesso"] = "Vaga criada com sucesso.";

                    return RedirectToAction("Create");
                } catch (Exception)
                {
                    ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
                    ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
                    vaga.Perfil = PerfilHelper.PopularPerfil().ToList();
                    TempData["erro"] = "Erro ao criar a vaga.";
                }
            }

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
                try
                {
                    var ids = Request["check_perfil"].Split(',');
                    vaga.Perfil = new List<Perfil>();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = int.Parse(ids[i]);
                        vaga.Perfil.Add(db.Perfil.Where(x => x.Id == id).First());
                    }

                    if (vaga.Ativa && vaga.Data_Ativacao == null)
                    {
                        vaga.Data_Ativacao = DateTime.Now;
                        vaga.Data_Cancelamento = null;
                    }

                    if (!vaga.Ativa)
                        vaga.Data_Cancelamento = DateTime.Now;

                    db.Entry(vaga).State = EntityState.Modified;
                    db.SaveChanges();

                    TempData["sucesso"] = "Vaga modificada com sucesso.";

                    return RedirectToAction("Find");
                }
                catch (Exception)
                {
                    ViewBag.Tipo_Id = new SelectList(db.Tipo, "Id", "Tipo1", vaga.Tipo_Id);
                    ViewBag.Empresa_Id = new SelectList(db.Usuario, "Id", "Email", vaga.Empresa_Id);
                    TempData["erro"] = "Erro ao modificar a vaga.";
                }
            }
            
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
            try
            {
                vaga.Data_Cancelamento = DateTime.Now;
                vaga.Ativa = false;
                db.Entry(vaga).State = EntityState.Modified;
                db.SaveChanges();
                TempData["sucesso"] = "Vaga removida com sucesso.";
            }
            catch
            {
                TempData["erro"] = "Erro ao remover a vaga.";
            }
            
            return RedirectToAction("Find");
        }

        public ActionResult Find()
        {
            var vagas = db.Vaga;
            var listVagas = new List<Vaga>();

            CancelByDate();

            if (Session["tipo_usuario"] != null && int.Parse(Session["tipo_usuario"].ToString()) == 1)
            {
                if (Session["user_id"] != null)
                {
                    int idUsuario = int.Parse(Session["user_id"].ToString());
                    listVagas = vagas.Where(x => x.Empresa_Id == idUsuario && x.Data_Cancelamento == null).ToList();
                }
            }
            else
                listVagas = vagas.Where(x => x.Ativa && x.Data_Cancelamento == null).ToList();

            if (listVagas == null || listVagas.Count < 1)
            {
                ModelState.AddModelError(String.Empty, "Não existem vagas cadastradas.");
                listVagas = new List<Vaga>();
            }

            return View(listVagas);
        }

        private void CancelByDate()
        {
            var listVagas = db.Vaga.Where(x => x.Data_Cancelamento == null).ToList();

            foreach (Vaga vaga in listVagas)
            {
                if ((vaga.Data_Cadastro.AddDays(30)) < DateTime.Now)
                {
                    vaga.Data_Cancelamento = DateTime.Now;
                    vaga.Ativa = false;
                    db.Entry(vaga).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}