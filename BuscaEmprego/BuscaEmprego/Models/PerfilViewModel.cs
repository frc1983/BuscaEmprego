using BuscaEmprego.Business;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaEmprego.Models
{
    public class PerfilViewModel
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public static ICollection<Perfil> ParseToEntity(IList<PerfilViewModel> perfis)
        {
            var perfilBusiness = new PerfilBusiness();
            List<Perfil> perfisEntity = new List<Perfil>();
            foreach (var item in perfis.Where(x => x.Checked))
                perfisEntity.Add(perfilBusiness.GetById(int.Parse(item.Value)));

            return perfisEntity;
        }
    }
}