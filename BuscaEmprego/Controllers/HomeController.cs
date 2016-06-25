using BuscaEmprego.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaEmprego.Controllers
{
    public class HomeController : Controller
    {
        private BuscaEmpregoEntities db = new BuscaEmpregoEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.Vagas = FindAll();
            ViewBag.VagasPerfil = FindByArea();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Vaga> FindAll()
        {
            var listVagas = new List<Vaga>();
            listVagas = db.Vaga.Where(x => x.Ativa).ToList();

            return listVagas;
        }

        private List<Vaga> FindByArea()
        {
            var listVagas = new List<Vaga>();
            if (Session["tipo_usuario"] != null && int.Parse(Session["tipo_usuario"].ToString()) == 2)
            {
                if (Session["user_id"] != null)
                {
                    int idUsuario = int.Parse(Session["user_id"].ToString());
                    var usuarioLogado = db.Usuario.Where(x => x.Id == idUsuario).FirstOrDefault();
                    if (usuarioLogado != null)
                    {
                        var listaPerfisUsuario = usuarioLogado.Perfil.Select(x => x.Id).ToList();
                        listVagas = db.Vaga.Where(x => listaPerfisUsuario.Contains(x.Id) && x.Ativa).ToList();
                    }
                }
            }

            return listVagas;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
