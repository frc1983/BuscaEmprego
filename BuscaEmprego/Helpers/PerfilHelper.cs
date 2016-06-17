using BuscaEmprego.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Helpers
{
    public class PerfilHelper
    {
        public static ICollection<Perfil> PopularPerfil()
        {
            ICollection<Perfil> _perfil = new List<Perfil>();
            using (var db = new BuscaEmprego.Database.BuscaEmpregoEntities())
            {
                foreach (var item in db.Perfil.ToList())
                    _perfil.Add(item);
            }

            return _perfil;
        }
    }
}