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
        public Perfil GetById(int idv)
        {
            return new PerfilDAO().GetById(idv);
        }

        public List<Perfil> BuscarPerfis()
        {
            try
            {
                var perfis = new PerfilDAO().BuscarPerfis();

                return perfis;
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
