using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.DAO
{
    public class PerfilDAO
    {
        public List<Perfil> Listar()
        {
            using (var db = new BuscaEmprego())
            {
                return db.Perfil.ToList();
            }
        }

        public Perfil GetById(int id)
        {
            using (var db = new BuscaEmprego())
            {
                return db.Perfil.Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}
