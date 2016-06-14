using BuscaEmprego.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaEmprego.Helpers
{
    public class DropDownHelper
    {
        public static IEnumerable<SelectListItem> GetDropdownTipoUsuario()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Candidato", Value = "Usuario" });
            list.Add(new SelectListItem() { Text = "Empresa", Value = "Empresa" });

            return list;
        }

        public static IEnumerable<SelectListItem> GetDropdownTipoVaga()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Emprego", Value = "1" });
            list.Add(new SelectListItem() { Text = "Estágio", Value = "2" });

            return list;
        }

        public static IEnumerable<SelectListItem> GetDropdownPerfil()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var daoPerfil = new PerfilBusiness().BuscarPerfis();
            foreach (var perfil in daoPerfil)
            {
                list.Add(new SelectListItem() { Text = perfil.Nome, Value = perfil.Id.ToString() });
            }

            return list;
        }
    }
}