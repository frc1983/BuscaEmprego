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

        public static List<Perfil> BuscarPerfis()
        {
            try
            {
                var perfis = new PerfilDAO().BuscarPerfis();
                if (perfis ==  null || perfis.Count() == 0)
                    throw new BusinessException("Nenhum perfil encontrado.");

                return perfis;
            }
            catch (DAOException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
