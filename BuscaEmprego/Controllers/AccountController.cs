﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BuscaEmprego.Database;
using BuscaEmprego.Helpers;
using System.Data;

namespace BuscaEmprego.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private BuscaEmpregoEntities db = new BuscaEmpregoEntities();
        private ICollection<Perfil> _perfil = new List<Perfil>();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario model)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("CPF_CNPJ");
            if (ModelState.IsValid)
            {
                var user = db.Usuario.Where(x => x.Email == model.Email.ToLower() && x.Senha == model.Senha).FirstOrDefault();
                if (user != null)
                {
                    SetLoginSession(user);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Nome de usuário ou senha estão incorretos.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Logoff()
        {
            Session["user_name"] = null;
            Session["user_id"] = null;
            Session["tipo_usuario"] = null;

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var usuarioModel = ObterUsuarioLogado();  

            return View(usuarioModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ids = Request["check_perfil"].Split(',');
                    model.Perfil = new List<Perfil>();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        int id = int.Parse(ids[i]);
                        model.Perfil.Add(db.Perfil.Where(x => x.Id == id).First());
                    }

                    if (model.Endereco_Id != 0)
                    {
                        Endereco endereco = db.Endereco.Find(model.Endereco_Id);
                        endereco.CEP = model.Endereco.CEP;
                        endereco.Complemento = model.Endereco.Complemento;
                        endereco.Logradouro = model.Endereco.Logradouro;
                        endereco.Telefone = model.Endereco.Telefone;
                        endereco.Tipo = model.Endereco.Tipo;

                        db.Entry(endereco).State = EntityState.Modified;
                        db.SaveChanges();

                        model.Endereco = db.Endereco.Find(model.Endereco_Id);
                    }

                    if (model.Id != 0)
                        db.Entry(model).State = EntityState.Modified;
                    else
                        db.Usuario.Add(model);

                    db.SaveChanges();

                    SetLoginSession(model);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

        private Usuario ObterUsuarioLogado()
        {
            var usuarioModel = new Usuario();

            if (Session["user_id"] != null && int.Parse(Session["user_id"].ToString()) != 0)
            {
                var id = int.Parse(Session["user_id"].ToString());
                usuarioModel = db.Usuario.Where(x => x.Id == id).FirstOrDefault();
            }

            return usuarioModel;
        }

        private void SetLoginSession(Usuario model)
        {
            Session["user_name"] = model.Email;
            Session["user_id"] = model.Id;
            Session["tipo_usuario"] = model.Tipo_Usuario_Id;
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
