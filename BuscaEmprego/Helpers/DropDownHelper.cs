using BuscaEmprego.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaEmprego.Helpers
{
    public class DropDownHelper
    {
        public static IEnumerable<SelectListItem> GetDropdownTipoUsuario(int selected)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            using (var db = new BuscaEmpregoEntities())
            {
                foreach (var tipo in db.Tipo_Usuario.ToList())
                    itens.Add(new SelectListItem() { Text = tipo.Tipo, Value = tipo.Id.ToString(), Selected = tipo.Id == selected ? true : false });
            }

            return itens;
        }
    }
}