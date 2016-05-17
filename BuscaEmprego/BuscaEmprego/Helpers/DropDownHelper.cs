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
    }
}