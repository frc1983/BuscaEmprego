using System;
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

namespace BuscaEmprego.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
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
        public ActionResult Login(Usuario model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Senha, persistCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            var usuarioModel = new Usuario();

            PopularPerfil();
            usuarioModel.Perfil = _perfil;

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
                    using (var db = new BuscaEmprego.Database.BuscaEmpregoEntities())
                    {                        
                        var ids = Request["check_perfil"].Split(',');
                        model.Perfil = new List<Perfil>();
                        for (int i = 0; i < ids.Length; i++){
                            int id = int.Parse(ids[i]);
                            model.Perfil.Add(db.Perfil.Where(x => x.Id == id).First());
                        }

                        db.Usuario.Add(model);
                        db.SaveChanges();
                    }

                    Session["user_name"] = model.Email;
                    Session["user_id"] = model.Id;
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

        #region Helpers

        private void PopularPerfil()
        {
            using (var db = new BuscaEmprego.Database.BuscaEmpregoEntities())
            {
                foreach (var item in db.Perfil.ToList())
                    _perfil.Add(item);
            }
        }

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
