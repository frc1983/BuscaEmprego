using BuscaEmprego.DAO;
using BuscaEmprego.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuscaEmprego.Business
{
    public class PerfilBusiness
    {
        public List<Perfil> Listar()
        {
            return new PerfilDAO().Listar();
        }

        public Perfil GetById(int idv)
        {
            return new PerfilDAO().GetById(idv);
        }
    }
}
