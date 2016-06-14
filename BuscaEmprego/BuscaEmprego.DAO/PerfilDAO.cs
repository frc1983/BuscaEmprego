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
        public Perfil GetById(int id)
        {
            using (var db = new BuscaEmprego())
            {
                return db.Perfil.Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public List<Perfil> BuscarPerfis()
        {
            try
            {
                using (var db = new BuscaEmprego())
                {
                    return db.Perfil.ToList();
                }
            }
            catch (Exception e)
            {
                throw new DAOException("Erro ao buscar perfis.");
            }
        }
    }
}
